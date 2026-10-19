using System;
using System.IO;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ShadowFlame
{
    public sealed class ConsoleTTS : IDisposable
    {
        private const int DefaultRate = 3;
        private const int DefaultVolume = 100;

        private readonly string appDirectory;
        private readonly string ttsSettingsFile;

        private readonly SpeechSynthesizer synth;
        private readonly Func<string, CancellationToken, Task> _speakFunc;
        private bool _speechEnabled;
        public bool SpeechEnabled => _speechEnabled;

        /// <summary>
        /// Initializes a new instance of the ConsoleTTS class for 
        /// speech synthesis in console applications. It sets up the application directory, options file, and initializes the speech synthesizer. If the options file does not exist, it defaults to speech disabled and creates the options file to avoid blocking the UI with prompts.
        /// </summary>
        public ConsoleTTS()
        {
            appDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ShadowFlame");

            ttsSettingsFile = Path.Combine(appDirectory, "TTSSettings.txt");

            synth = new SpeechSynthesizer();
            InitializeSpeech();

            // default speak function uses the SpeechSynthesizer events to complete
            _speakFunc = (message, ct) =>
            {
                if (string.IsNullOrWhiteSpace(message))
                    return Task.CompletedTask;

                var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
                EventHandler<SpeakCompletedEventArgs>? handler = null;
                CancellationTokenRegistration ctr = default;
                handler = (s, e) =>
                {
                    try { synth.SpeakCompleted -= handler; } catch { }
                    try { ctr.Dispose(); } catch { }
                    tcs.TrySetResult(null);
                };

                try
                {
                    synth.SpeakCompleted += handler;
                    synth.SpeakAsync(message);
                    if (ct.CanBeCanceled)
                    {
                        try
                        {
                            ctr = ct.Register(() =>
                            {
                                try { synth.SpeakAsyncCancelAll(); } catch { }
                                try { synth.SpeakCompleted -= handler; } catch { }
                                tcs.TrySetCanceled(ct);
                            });
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    try { synth.SpeakCompleted -= handler; } catch { }
                    try { ctr.Dispose(); } catch { }
                    Trace.WriteLine(ex);
                    tcs.TrySetException(ex);
                }

                return tcs.Task;
            };

            // Start settings initialization asynchronously to avoid blocking callers/UI.
            _ = LoadFromTtsSettingsAsync();
        }

        /// <summary>
        /// Internal constructor used by tests to inject a custom speak function and control enabled state.
        /// </summary>
        internal ConsoleTTS(Func<string, CancellationToken, Task> speakFunc, bool speechEnabled = false)
        {
            appDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ShadowFlame");

            ttsSettingsFile = Path.Combine(appDirectory, "TTSSettings.txt");

            synth = new SpeechSynthesizer();
            InitializeSpeech();
            _speakFunc = speakFunc ?? ((m, ct) => { synth.SpeakAsync(m); return Task.CompletedTask; });
            _speechEnabled = speechEnabled;
        }

        // ---------------------------
        // Public API
        // ---------------------------

        /// <summary>
        /// Prints the current TTS settings to the console, including whether speech is enabled, the selected voice, rate, and volume.
        /// This is mostly used for debugging and diagnostic purposes to verify the TTS configuration.
        /// </summary>
        public void PrintSettings()
        {
            SpeakAndDisplay($"[TTS] SpeechEnabled={_speechEnabled}\n Voice='{synth.Voice?.Name}'\n Rate={synth.Rate} Volume={synth.Volume}");
        }
        
        public void Speak(string message)
        {
            if (_speechEnabled && !string.IsNullOrWhiteSpace(message))
            {
                // fire-and-forget
                _ = _speakFunc(message, CancellationToken.None);
            }
        }

        public Task SpeakAsync(string message, CancellationToken cancellationToken = default)
        {
            if (!_speechEnabled || string.IsNullOrWhiteSpace(message))
                return Task.CompletedTask;

            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            EventHandler<SpeakCompletedEventArgs>? handler = null;
            CancellationTokenRegistration ctr = default;
            handler = (s, e) =>
            {
                try { synth.SpeakCompleted -= handler; }
                catch { }
                try { ctr.Dispose(); } catch { }
                tcs.TrySetResult(null);
            };

            try
            {
                synth.SpeakCompleted += handler;
                synth.SpeakAsync(message);
                if (cancellationToken.CanBeCanceled)
                {
                    try
                    {
                        ctr = cancellationToken.Register(() =>
                        {
                            try { synth.SpeakAsyncCancelAll(); } catch { }
                            try { synth.SpeakCompleted -= handler; } catch { }
                            tcs.TrySetCanceled(cancellationToken);
                        });
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                try { synth.SpeakCompleted -= handler; } catch { }
                try { ctr.Dispose(); } catch { }
                Trace.WriteLine(ex);
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public void SpeakAndDisplay(string message = "", bool SameLine = false)
        {
            if (SameLine)
            {
                Console.Write(message);
            }
            else
            {
                Console.WriteLine(message);
            }
            // Respect the configured console speech setting: only speak when enabled.
            SpeakAsync(message);
        }

        public void Stop()
        {
            synth.SpeakAsyncCancelAll();
        }

        // ---------------------------
        // Initialization
        // ---------------------------

        private void InitializeSpeech()
        {
            synth.SetOutputToDefaultAudioDevice();
            synth.Rate = DefaultRate;
            synth.Volume = DefaultVolume;
        }

        // ---------------------------
        // Options Handling
        // ---------------------------

        private async Task LoadFromTtsSettingsAsync()
        {
            // Read settings produced by the TTSSetup dialog. If missing, launch the
            // TTS setup dialog so the user can create settings. If still missing after
            // the dialog, default to speech disabled.
            if (!File.Exists(ttsSettingsFile))
            {
                await LaunchTtsSetupDialogAsync().ConfigureAwait(false);
                if (!File.Exists(ttsSettingsFile))
                {
                    _speechEnabled = false;
                    return;
                }
            }

            try
            {
                var lines = await File.ReadAllLinesAsync(ttsSettingsFile).ConfigureAwait(false);
                foreach (var line in lines)
                {
                    if (line.StartsWith("ConsoleSpeech=", StringComparison.OrdinalIgnoreCase))
                    {
                        var v = line.Substring("ConsoleSpeech=".Length).Trim();
                        _speechEnabled = v.Equals("true", StringComparison.OrdinalIgnoreCase);
                    }
                    else if (line.StartsWith("Rate=", StringComparison.OrdinalIgnoreCase))
                    {
                        if (int.TryParse(line.Substring("Rate=".Length), out var r))
                        {
                            try { synth.Rate = r; } catch (Exception ex) { Trace.WriteLine(ex); }
                        }
                    }
                    else if (line.StartsWith("Volume=", StringComparison.OrdinalIgnoreCase))
                    {
                        if (int.TryParse(line.Substring("Volume=".Length), out var v2))
                        {
                            try { synth.Volume = v2; } catch (Exception ex) { Trace.WriteLine(ex); }
                        }
                    }
                    else if (line.StartsWith("Voice=", StringComparison.OrdinalIgnoreCase))
                    {
                        var voice = line.Substring("Voice=".Length);
                        try { if (!string.IsNullOrEmpty(voice)) synth.SelectVoice(voice); } catch (Exception ex) { Trace.WriteLine(ex); }
                    }
                }
            }
            catch (Exception ex)
            {
                // On any error parsing settings, default to disabled without prompting.
                Trace.WriteLine(ex);
                _speechEnabled = false;
            }
        }

        /// <summary>
        /// Launches the TTS setup dialog on an STA thread and returns when the dialog closes.
        /// </summary>
        private Task LaunchTtsSetupDialogAsync(CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            try
            {
                var thread = new Thread(() =>
                {
                    try
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        using (var dlg = new TTSSetup())
                        {
                            dlg.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                    finally
                    {
                        tcs.TrySetResult(null);
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
                if (cancellationToken.CanBeCanceled)
                {
                    cancellationToken.Register(() =>
                    {
                        // If cancellation is requested, signal the task as canceled. The STA thread
                        // running the dialog may continue until the dialog closes; aborting threads is unsafe.
                        tcs.TrySetCanceled(cancellationToken);
                    });
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                tcs.TrySetResult(null);
            }

            return tcs.Task;
        }

        public async Task InitializeAsync()
        {
            await LoadFromTtsSettingsAsync().ConfigureAwait(false);

            // Diagnostic information to help determine why speech may not be heard
            try
            {
                var voiceName = string.Empty;
                try { voiceName = synth.Voice?.Name ?? string.Empty; } catch { }
                var installed = 0;
                try { installed = synth.GetInstalledVoices().Count; } catch { }
                Trace.WriteLine($"ConsoleTTS initialized. SpeechEnabled={_speechEnabled}, Voice='{voiceName}', InstalledVoices={installed}, Rate={synth.Rate}, Volume={synth.Volume}");
                
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        // Note: ConsoleTTS no longer prompts or writes legacy options. It fully relies on
        // TTSSettings.txt produced by the TTSSetup dialog.

        // ---------------------------
        // Cleanup
        // ---------------------------

        public void Dispose()
        {
            synth?.Dispose();
        }
    }
}
