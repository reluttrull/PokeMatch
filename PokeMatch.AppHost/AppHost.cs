var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PokeMatch>("pokematch");

builder.AddProject<Projects.DeckApi>("deckapi");

builder.Build().Run();
