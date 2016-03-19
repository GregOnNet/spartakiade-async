using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  [TestFixture]
  public class TaskRunVsTaskFactorynew
  {
    [Test]
    public async Task TaskRun()
    {
      Console.Write("Thread: {0}", Thread.CurrentThread.ManagedThreadId);
      await Task.Run(() => DoCpuBoundOperationAsync(200));
    }

    [Test]
    public async Task TaskFactoryStartNew()
    {
      Console.Write("Thread: {0}", Thread.CurrentThread.ManagedThreadId);
      await Task.Factory.StartNew(() => DoCpuBoundOperationAsync(200));
    }

    public async Task DoCpuBoundOperationAsync(int delayInMilliSeconds)
    {
      await Task.Delay(delayInMilliSeconds);
    }
  }

  [TestFixture]
  public class ParallelFor
  {
    [Test]
    public void CalculateFibonacciSecqunce()
    {
      Parallel.For(1, 10, (i,state) => Console.WriteLine(
        Fibonacci(i)
      ));
    }

    public int Fibonacci(int n)
    {
      int a = 0;
      int b = 1;
      
      for (int i = 0; i < n; i++)
      {
        int temp = a;
        a = b;
        b = temp + b;
      }
      return a;
    }
  }
}
