using NUnit.Framework;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  [TestFixture]
  class AsyncPlayground
  {
    [Test]
    public async Task TaskRun()
    {
      await Task.Run(() => DoCpuBoundOperationAsync(1000));
    }

    public async Task DoCpuBoundOperationAsync(int delayInMilliSeconds)
    {
      await Task.Delay(delayInMilliSeconds);
    }
  }
}
