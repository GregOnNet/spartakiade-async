using Dolls.Contracts;
using Dolls.Messages;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Dolls
{
  [TestFixture]
  class PushMessageTest
  {
    [Test]
    public async Task UsePumpWithTransportMessages()
    {
      var messages = new ConcurrentQueue<TransportMessage>();
      messages.Enqueue(new TransportMessage());
      messages.Enqueue(new TransportMessage());

      var pipelineBuilder = new PipelineBuilder()
                              .Register(() => new Doll())
                              .Register(() => new Doll());

      var pumpMessages = new PushMessages(messages);

      await pumpMessages.StartAsync(tm => Connector(pipelineBuilder, tm));
    }

    static Task Connector(IProvideExecutionPipeline pipelineBuilder, TransportMessage message)
    {
      var pipeline = pipelineBuilder.Create();
      return pipeline.Invoke(message);
    }

    public class Doll : IStep
    {
      public Task Invoke(TransportMessage message, Func<Task> next)
      {
        Console.WriteLine("{0}: {1}", message.Id, message.Headers);
        return next();
      }
    }
  }
}
