using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  public static class TaskExtensions
  {
    public static void Ignore(this Task task) { }

    /// <summary>
    /// Deep, deep inside the .NET Framework
    /// This is a custom Await
    /// There for you have to write an Extension-Methd 'GetAwaiter' for the
    /// class that should be able to work with await.
    /// </summary>
    /// <param name="process"></param>
    /// <returns></returns>
    public static TaskAwaiter<int> GetAwaiter(this Process process)
    {
      var tcs = new TaskCompletionSource<int>();
      process.EnableRaisingEvents = true;
      process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);
      if (process.HasExited) tcs.TrySetResult(process.ExitCode);

      return tcs.Task.GetAwaiter();
    }
  }
}
