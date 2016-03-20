using Dolls.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dolls
{
  public class Pipeline : IPipeline
  {
    private IList<Func<IStep>> _actions;

    public Pipeline(IList<Func<IStep>> actions)
    {
      _actions = actions;
    }

    public Task Invoke()
    {
      return InvokeInternal(0);
    }

    private Task InvokeInternal(int actionIndex = 0)
    {
      if (actionIndex == _actions.Count)
        return Task.CompletedTask;

      var step = _actions[actionIndex]();
      return step.Invoke(() => InvokeInternal(actionIndex + 1));
    }
  }
}