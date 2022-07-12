using System;
using System.Threading.Tasks;

namespace EMBC.DFA.Resources.Submissions
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        public async Task<string> Manage(Command form) => await (form switch
        {
            SubmitGovFormCommand f => Handle(f),
            SubmitIndFormCommand f => Handle(f),
            SubmitSmbFormCommand f => Handle(f),
            _ => throw new NotImplementedException($"type {form.GetType().FullName}")
        });

        private Task<string> Handle(SubmitGovFormCommand f)
        {
            throw new NotImplementedException();
        }

        private Task<string> Handle(SubmitIndFormCommand f)
        {
            throw new NotImplementedException();
        }

        private Task<string> Handle(SubmitSmbFormCommand f)
        {
            throw new NotImplementedException();
        }
    }
}
