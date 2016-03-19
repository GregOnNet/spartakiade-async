using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  interface IPushMessages
  {
    void Start(Func<Task> pump);
    Task Stop();
  }

  class MessagePump : IPushMessages
  {
    private ConcurrentDictionary<Task, Task> _runningTasks;
    private CancellationTokenSource _cancellationSource;

    public MessagePump()
    {
      _runningTasks = new ConcurrentDictionary<Task, Task>();
    }

    public void Start(Func<Task> pump)
    {
      _cancellationSource = new CancellationTokenSource();

      var pumpTask = Task.Run(() =>
      {
        while (!_cancellationSource.IsCancellationRequested)
        {
          var runningTask = pump();
          _runningTasks.TryAdd(runningTask, runningTask);

          runningTask.ContinueWith(t =>
          {
            Task taskToBeRemoved;
            _runningTasks.TryRemove(t, out taskToBeRemoved);
          }, TaskContinuationOptions.ExecuteSynchronously);
        }
      });

      pumpTask.ConfigureAwait(false);
      Console.WriteLine("Pump finished!");
    }

    public Task Stop()
    {
      return Task.Run(() => 
      {
        _cancellationSource.Cancel();

        Task.WaitAll(_runningTasks.Keys.ToArray());

        Console.WriteLine("All Tasks finished");
        _cancellationSource.Dispose();
      });
    }
  }
}
