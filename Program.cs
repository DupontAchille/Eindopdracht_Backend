using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new List<string>()
                }
            });
            });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();


builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IMealRepository, MealRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IAreaRepository, AreaRepository>();
builder.Services.AddTransient<IMealService, MealService>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MealValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AreaValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>());
var apiKeySettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<ApiKeyConfig>(apiKeySettings);
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
        };
    }
);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

builder.Services.AddAuthorization(options =>
{

});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapPost("/authenticate", async (IAuthenticationService authenticationService, IOptions<ApiKeyConfig> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);

    if (user == null)
        return Results.Unauthorized();

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.SecretForKey!));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));

    var jwtSecurityToken = new JwtSecurityToken(
         authSettings.Value.Issuer,
         authSettings.Value.Audience,
         claimsForToken,
         DateTime.UtcNow,
         DateTime.UtcNow.AddHours(1),
         signingCredentials
    );

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);
});

// app.MapPost("/setuparea", (IMealService area) => area.SetupAreaData());

// app.MapPost("/setupcategory", (IMealService category) => category.SetupCategoryData());




app.MapPost("/addarea", async (IMealService MealService, IValidator<Area> validator, Area area) =>
{
    var validationResult = validator.Validate(area);
    if (validationResult.IsValid)
    {
        var result = await MealService.AddArea(area);
        return Results.Ok(result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

app.MapPost("/addcategory", async (IMealService MealService, IValidator<Category> validator, Category category) =>
{
    var validationResult = validator.Validate(category);
    if (validationResult.IsValid)
    {
        var result = await MealService.AddCategory(category);
        return Results.Ok(result);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

app.MapDelete("/deletearea/{id}", async (IMealService MealService, string Id) => await MealService.DeleteArea(Id));

app.MapGet("/getareas", [Authorize] async (IMealService area) => await area.GetAreas());

app.MapGet("/getcategories", async (IMealService category) => await category.GetCategories());

app.MapGet("/getareabyname/{AreaName}", async (IMealService MealService, string AreaName) =>
{
    var result = await MealService.GetOneAreaByName(AreaName);
    return Results.Ok(result);
});

app.MapGet("/getareabyid/{id}", async (IMealService MealService, string areaId) =>
{
    var result = await MealService.GetOneAreaById(areaId);
    return Results.Ok(result);
});

app.MapGet("/getcategorybyname/{CategoryName}", async (IMealService MealService, string CategoryName) =>
{
    var result = await MealService.GetOneCategoryByName(CategoryName);
    return Results.Ok(result);
});

app.MapGet("/getcategorybyid/{id}", async (IMealService MealService, string categoryId) =>
{
    var result = await MealService.GetOneCategoryById(categoryId);
    return Results.Ok(result);
});

// app.MapPut();


app.UseSwagger();
// app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.MapGraphQL();
app.Run("http://localhost:3000");
