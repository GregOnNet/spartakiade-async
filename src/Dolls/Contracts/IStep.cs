using System;
using System.Threading.Tasks;

namespace Dolls.Contracts
{
  public interface IStep
  {
    Task Invoke(Func<Task> next);
  }
}
