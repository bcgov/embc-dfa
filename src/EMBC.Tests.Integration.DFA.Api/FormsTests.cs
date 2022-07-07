using System.Net;
using EMBC.DFA.Api.Models;

namespace EMBC.Tests.Integration.DFA.Api
{
    public class FormsTests
    {
        [Test]
        public async Task SubmitGovForm()
        {
            var host = Application.Host;

            await host.Scenario(s =>
            {
                s.Post.Json(new GovForm()).ToUrl("/forms/gov");
                s.StatusCodeShouldBe((int)HttpStatusCode.Created);
            });
        }
    }
}
