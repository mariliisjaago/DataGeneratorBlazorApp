var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddConnectionString("DataGenerator");

var api = builder.AddProject<Projects.DataGenerator_Api>("api")
    .WithReference(db);
var web = builder.AddProject<Projects.DataGeneratorBlazor>("datageneratorblazor")
    .WithReference(api)
    .WithExternalHttpEndpoints();



builder.Build().Run();
