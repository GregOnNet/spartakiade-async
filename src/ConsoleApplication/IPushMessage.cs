using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  public interface IPushMessage
  {
    void Start(Func<Task> pump);
    Task Stop();
  }
}