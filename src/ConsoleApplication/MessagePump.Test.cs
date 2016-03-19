using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
  [TestFixture]
  class MessagePumpTest
  {
    [Test]
    public void Foo()
    {
      var messagePump = new MessagePump();
      var canellation = new CancellationTokenSource();
          canellation.CancelAfter(100);

      messagePump.Start(Pump);
      Thread.Sleep(100);
      messagePump.Stop().GetAwaiter().GetResult();
    }

    public Task Pump()
    {
      return Task.Delay(100);
    }
  }
}
