var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PokeMatch>("pokematch");

builder.Build().Run();
