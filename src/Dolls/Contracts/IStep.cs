using Dolls.Messages;
using System;
using System.Threading.Tasks;

namespace Dolls.Contracts
{
  public interface IStep
  {
    Task Invoke(TransportMessage message, Func<Task> next);
  }
}
