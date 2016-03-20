using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
      Parallel.For(1, 10, async (i, state) => 
      {
        var fib = await Fibonacci(i);
        Console.WriteLine(fib);
      });
    }

    public Task<int> Fibonacci(int n)
    {
      int a = 0;
      int b = 1;
      
      for (int i = 0; i < n; i++)
      {
        int temp = a;
        a = b;
        b = temp + b;
      }

      return Task.FromResult(a);
    }
  }

  [TestFixture]
  public class TaskWhenAll
  {
    [Test]
    public void TaskWhenAllForFibonacci()
    {
      var tasks = new List<Task>();
      var boundaries = new int[] { 2, 3, 5, 8, 14, 21 };

      foreach(var boundary in boundaries)
      {
        tasks.Add(Fibonacci(boundary));
      }

      var coordinator = Task.WhenAll(tasks);
      Console.WriteLine(coordinator.Status);
    }

    public Task<int> Fibonacci(int n)
    {
      int a = 0;
      int b = 1;

      for (int i = 0; i < n; i++)
      {
        int temp = a;
        a = b;
        b = temp + b;
      }

      return Task.FromResult(a);
    }

    [Test]
    public async Task CompletionSource()
    {
      var tcs = new TaskCompletionSource<int>();
      var process = Process.Start(new ProcessStartInfo(@"ping.exe") { UseShellExecute = false, CreateNoWindow = true });
      process.EnableRaisingEvents = true;
      process.Exited += (s, e) =>
      {
        tcs.TrySetResult(process.ExitCode);
      };

      if(process.HasExited)
        tcs.TrySetResult(process.ExitCode);

      await tcs.Task.ConfigureAwait(false);
    }

    [Test]
    public async Task CompletionSourceCustomAwait()
    {
      var tcs = new TaskCompletionSource<int>();
      var process = await Process.Start(new ProcessStartInfo(@"ping.exe") { UseShellExecute = false, CreateNoWindow = true });
    }
  }
}
