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

        [Test]
        public async Task SubmitSmbForm()
        {
            var form = new SmbForm
            {
                data =
                {
                    pleaseSelectTheAppropriateOption = "test",
                    yes7 = "No",
                    //fill in fields
                }
            };


            //add intake manager and try to submit

            var host = Application.Host;

            await host.Scenario(s =>
            {
                s.Post.Json(new GovForm()).ToUrl("/forms/gov");
                s.StatusCodeShouldBe((int)HttpStatusCode.Created);
            });
        }
    }
}
