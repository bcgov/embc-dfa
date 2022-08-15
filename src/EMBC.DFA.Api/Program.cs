﻿using System.Net;
using EMBC.DFA.Api;
using EMBC.DFA.Api.Dynamics;
using EMBC.DFA.Managers.Intake;
using EMBC.DFA.Resources.Submissions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors()
    .AddCache()
    .AddDfaDynamics(builder.Configuration)
    .AddIntakeManager()
    .AddSubmissionsRepository()
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
app.MapPost("/forms/smb", async ctx =>
{
    var model = await ctx.Request.ReadJsonModelAsync<EMBC.DFA.Api.Models.SmbForm>();
    if (!model.IsValid)
    {
        await ctx.Response.ValidationError("Invalid payload");
        return;
    }
    var mgr = ctx.RequestServices.GetRequiredService<IIntakeManager>();
    var submissionId = await mgr.Handle(new NewSmbFormSubmissionCommand { Form = Mapper.Map<SmbForm>(model.Payload) ?? null! });
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
    var mgr = ctx.RequestServices.GetRequiredService<IIntakeManager>();
    var submissionId = await mgr.Handle(new NewIndFormSubmissionCommand { Form = Mapper.Map<IndForm>(model.Payload) ?? null! });
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
    var mgr = ctx.RequestServices.GetRequiredService<IIntakeManager>();
    var submissionId = await mgr.Handle(new NewGovFormSubmissionCommand { Form = Mapper.Map<GovForm>(model.Payload) ?? null! });
    ctx.Response.StatusCode = (int)HttpStatusCode.Created;
    await ctx.Response.WriteAsJsonAsync(new { id = submissionId });
}).WithName("Local government Form");

app.Run();
