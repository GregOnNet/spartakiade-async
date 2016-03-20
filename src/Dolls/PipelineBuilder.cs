using Dolls.Contracts;
using System;
using System.Collections.Generic;

namespace Dolls
{
  public class PipelineBuilder : IProvideExecutionPipeline
  {
    private IList<Func<IStep>> _steps;

    public PipelineBuilder()
    {
      _steps = new List<Func<IStep>>();
    }

    public IPipeline Create()
    {
      return new Pipeline(_steps);
    }

    public IProvideExecutionPipeline Register(Func<IStep> step)
    {
      _steps.Add(step);
      return this;
    }
  }
}
