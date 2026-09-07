using System;
using System.Drawing;
using System.IO;
using Xunit;

namespace ShadowFlame.Tests
{
    public class GlobalFontServiceTests : IDisposable
    {
        private readonly string _tempDir;
        private readonly string _fontFile;

        public GlobalFontServiceTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "ShadowFlameTests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempDir);
            _fontFile = Path.Combine(_tempDir, "Font.txt");
        }

        [Fact]
        public void SaveAndLoadFont_WritesAndReadsFontFile()
        {
            var font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);
            var svc = new GlobalFontService(_tempDir, _fontFile, font, skipDialog: true);
            svc.SaveFont();

            Assert.True(File.Exists(_fontFile));
            var lines = File.ReadAllLines(_fontFile);
            Assert.Equal(4, lines.Length);

            var loaded = svc.LoadFont();
            Assert.Equal(font.FontFamily.Name, loaded.FontFamily.Name);
            Assert.Equal(font.Size, loaded.Size);
            Assert.Equal(font.Style, loaded.Style);
        }

        public void Dispose()
        {
            try { Directory.Delete(_tempDir, true); } catch { }
        }
    }
}
