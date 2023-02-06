using System.Net;
using EMBC.DFA.Api;
using EMBC.DFA.Dynamics;
using EMBC.DFA.Managers.Intake;
using EMBC.DFA.Resources.Submissions;
using EMBC.Utilities;
using EMBC.Utilities.Runtime;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors()
    .AddCache(string.Empty)
    .AddDfaDynamics(builder.Configuration)
    .AddIntakeManager()
    .AddSubmissionsRepository()
    .AddHealthChecks()
    ;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(opts =>
{
    opts.AllowAnyOrigin().AllowAnyHeader();
});

app.Use((ctx, next) =>
{
    CallContext.Current = new CallContext(ctx.RequestServices, new CancellationTokenSource(), ctx.TraceIdentifier);
    var headers = ctx.Response.GetTypedHeaders();
    headers.Append("TraceIdentifier", ctx.TraceIdentifier);
    headers.Append("ContextIdentifier", CallContext.Current.ContextIdentifier);
    return next(ctx);
});
app.MapHealthChecks("/hc/ready", new HealthCheckOptions() { Predicate = check => check.Tags.Contains("ready") });
app.MapHealthChecks("/hc/live", new HealthCheckOptions() { Predicate = check => check.Tags.Contains("alive") });

app.MapPost("/forms/smb", async ctx =>
{
    //Console.WriteLine("Testing");
    //await ctx.Response.WriteAsJsonAsync(new { res = "success" });

    var model = await ctx.Request.ReadJsonModelAsync<EMBC.DFA.Api.Models.SmbForm>();
    if (!model.IsValid)
    {
        await ctx.Response.ValidationError("Invalid payload");
        return;
    }
    var mgr = CallContext.Current.Services.GetRequiredService<IIntakeManager>();
    var submissionId = await mgr.Handle(new NewSmbFormSubmissionCommand { Form = EMBC.DFA.Api.Mappings.Map(model.Payload) });
    ctx.Response.StatusCode = (int)HttpStatusCode.Created;
    await ctx.Response.WriteAsJsonAsync(new { id = submissionId });
}).WithName("SMB Form");

app.MapPost("/forms/ind", async ctx =>
{
    var model = await ctx.Request.ReadJsonModelAsync<EMBC.DFA.Api.Models.IndForm>();
    if (!model.IsValid)
    {
        await ctx.Response.ValidationError("Invalid payload");
        return;
    }
    var mgr = CallContext.Current.Services.GetRequiredService<IIntakeManager>();
    var submissionId = await mgr.Handle(new NewIndFormSubmissionCommand { Form = EMBC.DFA.Api.Mappings.Map(model.Payload) });
    ctx.Response.StatusCode = (int)HttpStatusCode.Created;
    await ctx.Response.WriteAsJsonAsync(new { id = submissionId });
}).WithName("Individual Form");

app.MapPost("/forms/gov", async ctx =>
{
    var model = await ctx.Request.ReadJsonModelAsync<EMBC.DFA.Api.Models.GovForm>();
    if (!model.IsValid)
    {
        await ctx.Response.ValidationError("Invalid payload");
        return;
    }
    var mgr = CallContext.Current.Services.GetRequiredService<IIntakeManager>();
    var submissionId = await mgr.Handle(new NewGovFormSubmissionCommand { Form = EMBC.DFA.Api.Mappings.Map(model.Payload) });
    ctx.Response.StatusCode = (int)HttpStatusCode.Created;
    await ctx.Response.WriteAsJsonAsync(new { id = submissionId });
}).WithName("Local government Form");

app.MapGet("/test", async ctx =>
{
    Console.WriteLine("Testing");
    await ctx.Response.WriteAsJsonAsync(new { res = "success" });
}).WithName("Test");

app.Run();
