using Dolls.Contracts;
using Dolls.Messages;
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

    public Task Invoke(TransportMessage message)
    {
      return InvokeInternal(message, 0);
    }

    private Task InvokeInternal(TransportMessage message, int actionIndex = 0)
    {
      if (actionIndex == _actions.Count)
        return Task.CompletedTask;

      var step = _actions[actionIndex]();
      return step.Invoke(message, () => InvokeInternal(message, actionIndex + 1));
    }
  }
}