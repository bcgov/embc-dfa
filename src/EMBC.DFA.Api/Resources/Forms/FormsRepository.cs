using AutoMapper;
using EMBC.DFA.Api.Dynamics;

namespace EMBC.DFA.Api.Resources.Forms
{
    public class FormsRepository : IFormsRepository
    {
        private readonly IMapper mapper;
        private readonly IDfaContextFactory dfaContextFactory;

        public FormsRepository(IMapper mapper, IDfaContextFactory dfaContextFactory)
        {
            this.mapper = mapper;
            this.dfaContextFactory = dfaContextFactory;
        }

        public async Task<ManageFormCommandResult> Manage(ManageFormCommand cmd)
        {
            //map to dynamics
            //do the thing
            return await Task.FromResult(new ManageFormCommandResult { Id = "caseId" });
        }
    }
}
