using System.Threading.Tasks;

namespace Dolls.Contracts
{
  public interface IPipeline
  {
    Task Invoke();
  }
}
