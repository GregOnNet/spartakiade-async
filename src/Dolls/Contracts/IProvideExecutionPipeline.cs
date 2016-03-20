using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolls.Contracts
{
  public interface IProvideExecutionPipeline
  {
    IProvideExecutionPipeline Register(Func<IStep> step);
    IPipeline Create();
  }
}
