using System.Threading.Tasks;
using Xunit;

namespace ShadowFlame.Tests
{
    public class WindowsTTSTests
    {
        [Fact]
        public void GetAvailableVoices_ReturnsArray()
        {
            var voices = WindowsTTS.GetAvailableVoices();
            Assert.NotNull(voices);
        }

        [Fact]
        public async Task GetAvailableVoicesAsync_MatchesSync()
        {
            var asyncVoices = await WindowsTTS.GetAvailableVoicesAsync();
            var syncVoices = WindowsTTS.GetAvailableVoices();
            Assert.Equal(syncVoices, asyncVoices);
        }

        [Fact]
        public async Task SelectVoiceAsync_DoesNotThrow_ForInvalidVoice()
        {
            await WindowsTTS.SelectVoiceAsync("__NonExistentVoice__");
        }

        [Fact]
        public async Task SetRateAndVolumeAsync_DoNotThrow()
        {
            await WindowsTTS.SetRateAsync(2);
            await WindowsTTS.SetVolumeAsync(50);
        }

        [Fact]
        public async Task SpeakAsync_EmptyText_CompletesQuickly()
        {
            await WindowsTTS.SpeakAsync(string.Empty);
        }
    }
}
