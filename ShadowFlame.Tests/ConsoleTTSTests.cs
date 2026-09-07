using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShadowFlame.Tests
{
    public class ConsoleTTSTests
    {
        [Fact]
        public async Task SpeakAsync_Completes_WhenSpeakFuncCompletes()
        {
            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            bool called = false;
            Func<string, CancellationToken, Task> speakFunc = (msg, ct) =>
            {
                called = true;
                return tcs.Task;
            };

            using var tts = new ConsoleTTS(speakFunc, speechEnabled: true);
            var task = tts.SpeakAsync("hello");
            Assert.False(task.IsCompleted);
            Assert.True(called);
            tcs.SetResult(null);
            await task;
        }

        [Fact]
        public async Task SpeakAsync_Cancels_WhenTokenCanceled()
        {
            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            Func<string, CancellationToken, Task> speakFunc = (msg, ct) =>
            {
                var reg = ct.Register(() => tcs.TrySetCanceled(ct));
                return tcs.Task.ContinueWith(t => { reg.Dispose(); }, TaskScheduler.Default);
            };

            using var tts = new ConsoleTTS(speakFunc, speechEnabled: true);
            var cts = new CancellationTokenSource();
            var task = tts.SpeakAsync("hello", cts.Token);
            cts.Cancel();
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
        }
    }
}
