using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NuGet.Protocol.Tests
{
    public class SemaphoreSlimThrottleTests
    {
        [Fact]
        public async Task SemaphoreSlimThrottle_RespectsInnerSemaphore()
        {
            // Arrange
            var semaphoreSlim = new SemaphoreSlim(2);
            var target = new SemaphoreSlimThrottle(semaphoreSlim);

            // Act
            await target.WaitAsync();
            var countA = semaphoreSlim.CurrentCount;
            await target.WaitAsync();
            var countB = semaphoreSlim.CurrentCount;
            target.Release();
            var countC = semaphoreSlim.CurrentCount;
            target.Release();
            var countD = semaphoreSlim.CurrentCount;

            // Assert
            Assert.Equal(1, countA);
            Assert.Equal(0, countB);
            Assert.Equal(1, countC);
            Assert.Equal(2, countD);
        }
    }
}
