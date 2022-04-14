var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run("http://localhost:3000");
