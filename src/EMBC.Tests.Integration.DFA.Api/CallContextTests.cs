using System.Collections.Concurrent;
using Shouldly;

namespace EMBC.Tests.Integration.DFA.Api
{
    public class CallContextTests
    {
        //[Test]
        //public async Task ParallelRequests_DifferentIdentifiers()
        //{
        //    var host = Application.Host;

        //    var traceHeaders = new ConcurrentBag<string>();
        //    var contextHeaders = new ConcurrentBag<string>();
        //    await Parallel.ForEachAsync(Enumerable.Range(1, 30), async (t, ct) =>
        //    {
        //        var result = await host.Scenario(s =>
        //        {
        //            s.Get.Url("/hc");
        //        });
        //        traceHeaders.Add(result.Context.Response.Headers.Where(h => h.Key == "TraceIdentifier").ShouldHaveSingleItem().Value);
        //        contextHeaders.Add(result.Context.Response.Headers.Where(h => h.Key == "ContextIdentifier").ShouldHaveSingleItem().Value);
        //    });

        //    traceHeaders.ShouldBeUnique();
        //    //TODO: contextHeaders.ShouldBeUnique();
        //}

        //[Test]
        //public async Task SerialRequests_DifferentIdentifiers()
        //{
        //    var host = Application.Host;

        //    var scenarios = Enumerable.Range(1, 30).Select(i => host.Scenario(s =>
        //    {
        //        s.Get.Url("/hc");
        //    }));

        //    var traceHeaders = new ConcurrentBag<string>();
        //    var contextHeaders = new ConcurrentBag<string>();
        //    foreach (var scenario in scenarios)
        //    {
        //        var result = await scenario;
        //        traceHeaders.Add(result.Context.Response.Headers.Where(h => h.Key == "TraceIdentifier").ShouldHaveSingleItem().Value);
        //        contextHeaders.Add(result.Context.Response.Headers.Where(h => h.Key == "ContextIdentifier").ShouldHaveSingleItem().Value);
        //    }

        //    traceHeaders.ShouldBeUnique();
        //    contextHeaders.ShouldBeUnique();
        //}
    }
}
