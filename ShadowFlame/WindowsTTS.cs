using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace ShadowFlame
{
    public static class WindowsTTS
    {
        private const int SpeechRate = 3; // Adjust the speech rate as needed (range: -10 to 10)
        private const int Volume = 100; // Adjust the volume as needed (range: 0 to 100)
        private static SpeechSynthesizer _synthesizer;

        static WindowsTTS()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.Rate = SpeechRate;
            _synthesizer.Volume = Volume;
        }

        // Async APIs
        public static Task<string[]> GetAvailableVoicesAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    return _synthesizer.GetInstalledVoices()
                        .Select(v => v.VoiceInfo.Name)
                        .ToArray();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    return Array.Empty<string>();
                }
            });
        }

        public static Task SelectVoiceAsync(string name)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(name))
                        _synthesizer.SelectVoice(name);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            });
        }

        public static Task SetRateAsync(int rate)
        {
            return Task.Run(() =>
            {
                try { _synthesizer.Rate = rate; } catch (Exception ex) { Trace.WriteLine(ex); }
            });
        }

        public static Task SetVolumeAsync(int volume)
        {
            return Task.Run(() =>
            {
                try { _synthesizer.Volume = volume; } catch (Exception ex) { Trace.WriteLine(ex); }
            });
        }

        public static Task SpeakAsync(string text)
        {
            if (_synthesizer == null || string.IsNullOrWhiteSpace(text))
                return Task.CompletedTask;

            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            EventHandler<SpeakCompletedEventArgs>? handler = null;
            handler = (s, e) =>
            {
                try { _synthesizer.SpeakCompleted -= handler; } catch { }
                tcs.TrySetResult(null);
            };

            try
            {
                _synthesizer.SpeakCompleted += handler;
                _synthesizer.SpeakAsync(text);
            }
            catch (Exception ex)
            {
                try { _synthesizer.SpeakCompleted -= handler; } catch { }
                Trace.WriteLine(ex);
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public static void Stop()
        {
            if (_synthesizer != null)
            {
                _synthesizer.SpeakAsyncCancelAll();
            }
        }

        // Backwards-compatible synchronous APIs (wrap async or call underlying directly)
        public static string[] GetAvailableVoices()
        {
            try
            {
                return _synthesizer.GetInstalledVoices()
                    .Select(v => v.VoiceInfo.Name)
                    .ToArray();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return Array.Empty<string>();
            }
        }

        public static void SelectVoice(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                    _synthesizer.SelectVoice(name);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public static void SetRate(int rate)
        {
            try { _synthesizer.Rate = rate; } catch (Exception ex) { Trace.WriteLine(ex); }
        }

        public static void SetVolume(int volume)
        {
            try { _synthesizer.Volume = volume; } catch (Exception ex) { Trace.WriteLine(ex); }
        }

        /// <summary>
        /// Backwards-compatible synchronous wrapper that starts speaking without awaiting.
        /// </summary>
        public static void Speak(string text)
        {
            _ = SpeakAsync(text);
        }

        public static void Dispose()
        {
            if (_synthesizer != null)
            {
                _synthesizer.Dispose();
                _synthesizer = null;
            }
        }
    }
}
