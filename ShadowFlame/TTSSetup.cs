using ShadowFlame;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowFlame
{
    public partial class TTSSetup : Form
    {
        public TTSSetup()
        {
            InitializeComponent();
            this.Font = GlobalFontService.Instance.CurrentFont;
            // wire up events
            this.Load += TTSSetup_Load;
            VoiceCB.SelectedIndexChanged += VoiceCB_SelectedIndexChanged;
            SpeechTRK.ValueChanged += SpeechTRK_ValueChanged;
            VolumeTRK.ValueChanged += VolumeTRK_ValueChanged;
            this.FormClosing += TTSSetup_FormClosing;
        }

        private void PopulateVoices()
        {
            var voices = WindowsTTS.GetAvailableVoices();
            VoiceCB.Items.Clear();
            foreach (var voice in voices)
                VoiceCB.Items.Add(voice);

            if (VoiceCB.Items.Count > 0)
                VoiceCB.SelectedIndex = 0;
        }

        private void TTSSetup_Load(object? sender, System.EventArgs e)
        {
            PopulateVoices();
            // restore saved settings if present
            try
            {
                LoadSettings();
            }
            catch { /* ignore settings errors */ }
        }

        private void VoiceCB_SelectedIndexChanged(object? sender, System.EventArgs e)
        {
            SaveSettings();
        }

        private void SpeechTRK_ValueChanged(object? sender, System.EventArgs e)
        {
            UpdateRateLabel();
            SaveSettings();
        }

        private void VolumeTRK_ValueChanged(object? sender, System.EventArgs e)
        {
            UpdateVolumeLabel();
            SaveSettings();
        }

        private void TTSSetup_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        // Persist settings to AppData\ShadowFlame\ttssettings.txt
        private readonly string settingsFile = System.IO.Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
            "ShadowFlame", "TTSSettings.txt");

        private void SaveSettings()
        {
            try
            {
                var dir = System.IO.Path.GetDirectoryName(settingsFile);
                if (!string.IsNullOrEmpty(dir))
                    System.IO.Directory.CreateDirectory(dir);

                var voice = VoiceCB.SelectedItem as string ?? string.Empty;
                var rate = SpeechTRK.Value;
                var vol = VolumeTRK.Value;
                System.IO.File.WriteAllLines(settingsFile, new[] {
                    $"Voice={voice}",
                    $"Rate={rate}",
                    $"Volume={vol}",
                    $"ConsoleSpeech={ConsoleSpeechCHK.Checked}"
                });
            }
            catch
            {
                // ignore save errors
            }
        }

        private void LoadSettings()
        {
            if (!System.IO.File.Exists(settingsFile))
                return;

            var lines = System.IO.File.ReadAllLines(settingsFile);
            foreach (var line in lines)
            {
                if (line.StartsWith("Voice=", System.StringComparison.OrdinalIgnoreCase))
                {
                    var v = line.Substring("Voice=".Length);
                    var idx = VoiceCB.Items.IndexOf(v);
                    if (idx >= 0)
                        VoiceCB.SelectedIndex = idx;
                }
                else if (line.StartsWith("Rate=", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(line.Substring("Rate=".Length), out var r))
                    {
                        SpeechTRK.Value = r;
                    }
                }
                else if (line.StartsWith("Volume=", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(line.Substring("Volume=".Length), out var v))
                    {
                        VolumeTRK.Value = v;
                    }
                }
                else if (line.StartsWith("ConsoleSpeech=", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (bool.TryParse(line.Substring("ConsoleSpeech=".Length), out var c))
                    {
                        ConsoleSpeechCHK.Checked = c;
                    }
                }
            }
            UpdateRateLabel();
            UpdateVolumeLabel();
        }

        private void UpdateRateLabel()
        {
            if (SpeedLBL != null && SpeechTRK != null)
                SpeedLBL.Text = $"Speed: {SpeechTRK.Value}";
        }

        private void UpdateVolumeLabel()
        {
            if (VolumeLBL != null && VolumeTRK != null)
                VolumeLBL.Text = $"Volume: {VolumeTRK.Value}";
        }

        private async void PreviewBTN_Click(object sender, EventArgs e)
        {
            var voice = VoiceCB.SelectedItem as string;
            var rate = SpeechTRK.Value;
            var volume = VolumeTRK.Value;

            try
            {
                if (!string.IsNullOrEmpty(voice))
                    await WindowsTTS.SelectVoiceAsync(voice).ConfigureAwait(false);
                await WindowsTTS.SetRateAsync(rate).ConfigureAwait(false);
                await WindowsTTS.SetVolumeAsync(volume).ConfigureAwait(false);
                await WindowsTTS.SpeakAsync(SampleTXT.Text).ConfigureAwait(false);
            }
            catch
            {
                // ignore preview errors
            }
        }

        private void SaveBTN_Click(object sender, EventArgs e)
        {
            SaveSettings();
            DisplayMessage.ShowInfo("Settings saved successfully.", "TTS Settings");
            Close();
        }
    }
}
