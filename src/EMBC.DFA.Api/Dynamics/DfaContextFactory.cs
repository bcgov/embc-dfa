using Microsoft.Extensions.Options;
using Microsoft.OData.Client;
using Microsoft.OData.Extensions.Client;

namespace EMBC.DFA.Api.Dynamics
{
    public interface IDfaContextFactory
    {
        DfaContext Create();

        DfaContext CreateReadOnly();
    }

    internal class DfaContextFactory : IDfaContextFactory
    {
        private readonly IODataClientFactory odataClientFactory;
        private readonly DfaContextOptions dynamicsOptions;

        public DfaContextFactory(IODataClientFactory odataClientFactory, IOptions<DfaContextOptions> dynamicsOptions)
        {
            this.odataClientFactory = odataClientFactory;
            this.dynamicsOptions = dynamicsOptions.Value;
        }

        public DfaContext Create() => Create(MergeOption.AppendOnly);

        public DfaContext CreateReadOnly() => Create(MergeOption.NoTracking);

        private DfaContext Create(MergeOption mergeOption)
        {
            var ctx = odataClientFactory.CreateClient<DfaContext>(dynamicsOptions.DynamicsApiBaseUri, "dfa_dynamics");
            ctx.MergeOption = mergeOption;
            return ctx;
        }
    }
}
