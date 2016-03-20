using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolls
{
  [TestFixture]
  class AsyncDolls
  {
    [Test]
    public void AsyncChainOfResponsibility()
    {
      Func<Task> done = () =>
      {
        Console.WriteLine("Done!");
        return Task.CompletedTask;
      };

      var actions = new List<Func<Func<Task>, Task>>
      {
        HandleException,
        Son,
        Wife,
        Husband,
        EvilMethod,
        next => done()
      };

      Invoke(actions);
    }

    public Task Invoke(IList<Func<Func<Task>, Task>> actions, int actionIndex = 0)
    {
      if (actionIndex == actions.Count)
        return Task.CompletedTask;

      var action = actions[actionIndex];

      return action(() => Invoke(actions, actionIndex + 1));
    }

    public Task Son(Func<Task> next)
    {
      Console.WriteLine("Son's work");
      return next();
    }

    public Task Wife(Func<Task> next)
    {
      Console.WriteLine("Wife's work");
      return next();
    }

    public Task Husband(Func<Task> next)
    {
      Console.WriteLine("Husband's work");
      return next();
    }

    public Task HandleException(Func<Task> next)
    {
      try
      {
        return next();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: {0}", ex.Message);
        return Task.FromResult(1);
      }
    }

    public Task EvilMethod(Func<Task> next)
    {
      Console.WriteLine("Breaking the wheel...");
      throw new InvalidOperationException("Can't continue");
    }
  }
}
