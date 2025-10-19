var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.DataGenerator_Api>("api");
var web = builder.AddProject<Projects.DataGeneratorBlazor>("datageneratorblazor")
    .WithReference(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();
