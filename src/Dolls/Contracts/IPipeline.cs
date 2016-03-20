using System.Threading.Tasks;
using Dolls.Messages;

namespace Dolls.Contracts
{
  public interface IPipeline
  {
    Task Invoke(TransportMessage message);
  }
}
