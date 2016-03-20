using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  public interface IPushMessages1
  {
    void Start(Func<Task> pump);
    Task Stop();
  }
}