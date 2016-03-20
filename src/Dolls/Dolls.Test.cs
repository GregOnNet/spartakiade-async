using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolls
{
  [TestFixture]
  class Dolls
  {
    [Test]
    public void ChainOfResponsibility()
    {
      Action done = () => { Console.WriteLine("Done!"); };

      var actions = new List<Action<Action>>
      {
        Son,
        Wife,
        Husband,
        next => done()
      };

      Invoke(actions);
    }

    public void Invoke(IList<Action<Action>> actions, int actionIndex = 0)
    {
      if (actionIndex == actions.Count)
        return;

      var action = actions[actionIndex];

      action(() => Invoke(actions, actionIndex + 1));
    }

    public void Son(Action next)
    {
      Console.WriteLine("Son's work");
      next();
    }

    public void Wife(Action next)
    {
      Console.WriteLine("Wife's work");
      next();
    }

    public void Husband(Action next)
    {
      Console.WriteLine("Husband's work");
      next();
    }
  }
}
