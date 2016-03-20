using Dolls.Messages;
using System;
using System.Threading.Tasks;

namespace Dolls.Contracts
{
  public interface IPushMessages
  {
    Task StartAsync(Func<TransportMessage, Task> onMessage);
    Task StopAsync();
  }
}
