using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ShadowFlame
{
    //<summary>
    //persists a chosen font
    //</summary>
    public sealed class GlobalFontService
    {
        //in a form's constructor, run
        //this.Font = ShadowFlame.GlobalFontService.Instance.CurrentFont;

        private static readonly Lazy<GlobalFontService> _instance =
            new Lazy<GlobalFontService>(() => new GlobalFontService());

        public static GlobalFontService Instance => _instance.Value;

        private readonly string _appDataFolder;
        private readonly string _fontFile;

        private Font _currentFont;
        private readonly Font _defaultFont =
            new Font("Tahoma", 18F, FontStyle.Bold);

        public event EventHandler<Font> FontChanged;

        private GlobalFontService()
        {
            _appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ShadowFlame");

            _fontFile = Path.Combine(_appDataFolder, "Font.txt");

            Directory.CreateDirectory(_appDataFolder);

            _currentFont = LoadFont();
        }

        // Internal constructor for testing to avoid dialogs and file system side-effects
        internal GlobalFontService(string appDataFolder, string fontFile, Font initialFont, bool skipDialog = true)
        {
            _appDataFolder = appDataFolder;
            _fontFile = fontFile;
            Directory.CreateDirectory(_appDataFolder);
            _currentFont = initialFont ?? _defaultFont;
            if (!skipDialog)
            {
                _currentFont = LoadFont();
            }
        }

        public Font CurrentFont => _currentFont;

        public void ChooseFont()
        {
            using (FontDialog dlg = new FontDialog())
            {
                dlg.ShowColor = false;
                dlg.Font = _currentFont;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _currentFont = dlg.Font;
                    SaveFont();
                    FontChanged?.Invoke(this, _currentFont);
                }
            }
        }

        internal void SaveFont()
        {
            using (StreamWriter sw = new StreamWriter(_fontFile, false))
            {
                sw.WriteLine(_currentFont.FontFamily.Name);
                sw.WriteLine(_currentFont.Size);
                sw.WriteLine((int)_currentFont.Style);
                sw.WriteLine((int)_currentFont.Unit);
            }
        }

        internal Font LoadFont()
        {
            if (!File.Exists(_fontFile))
            {
                // First run → ask user
                using (FontDialog dlg = new FontDialog())
                {
                    dlg.ShowColor = false;
                    dlg.Font = _defaultFont;

                    if (dlg.ShowDialog() == DialogResult.OK)
                        _currentFont = dlg.Font;
                    else
                        _currentFont = _defaultFont;
                }

                SaveFont();
                return _currentFont;
            }

            try
            {
                string[] lines = File.ReadAllLines(_fontFile);
                if (lines.Length < 4)
                    throw new InvalidDataException();

                string family = lines[0];
                float size = float.Parse(lines[1]);
                FontStyle style = (FontStyle)int.Parse(lines[2]);
                GraphicsUnit unit = (GraphicsUnit)int.Parse(lines[3]);

                return new Font(family, size, style, unit);
            }
            catch
            {
                return _defaultFont;
            }
        }

    }
}
