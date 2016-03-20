using Dolls.Contracts;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Dolls
{
  [TestFixture]
  class PipelineTest
  {
    //[Test]
    //public async Task Do()
    //{
    //  var pipeBuilder = new PipelineBuilder();
    //  var pipeline = pipeBuilder
    //        .Register(() => new Doll())
    //        .Register(() => new Doll())
    //        .Create();

    //  await pipeline.Invoke();
    //}

    //public class Doll : IStep
    //{
    //  public Task Invoke(Func<Task> next)
    //  {
    //    Console.WriteLine("Doll");
    //    return next();
    //  }
    //}
  }
}
