using System;
using System.Threading.Tasks;

namespace Dolls.Extensions
{
  public static class ForTask
  {
    public static void Ignore(this Task task) { }

    public static async Task IgnoreCancellation(this Task task)
    {
      try
      {
        await task.ConfigureAwait(false);
      }
      catch (OperationCanceledException) { }
    }
  }
}
