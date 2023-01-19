using EMBC.DFA.Dynamics;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace EMBC.Tests.Integration.DFA.Api
{
    public class DynamicsConnectivityTests
    {
        [Test]
        public async Task GetSecurityToken()
        {
            var host = Application.Host;

            var tokenProvider = host.Services.GetRequiredService<ISecurityTokenProvider>();
            var token = await tokenProvider.AcquireToken();
            Console.WriteLine("Authorization: Bearer " + token);
        }

        [Test]
        public async Task CanConnectToDynamics()
        {
            var host = Application.Host;

            var factory = host.Services.GetRequiredService<IDfaContextFactory>();
            var ctx = factory.Create();

            //var results = await ctx.dfa_regions.GetAllPagesAsync();
            //results.ShouldNotBeEmpty();
        }
    }
}
