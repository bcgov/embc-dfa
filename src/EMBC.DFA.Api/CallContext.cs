namespace EMBC.DFA.Api
{
    public class CallContext
    {
        public CancellationTokenSource Cts { get; set; } = null!;
        public IServiceProvider ScopedProvider { get; set; } = null!;
    }
}
