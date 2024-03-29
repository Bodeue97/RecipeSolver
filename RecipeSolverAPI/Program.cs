global using Microsoft.EntityFrameworkCore;
global using RecipeSolverAPI.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeSolverAPI.Services.Auth;
using RecipeSolverAPI.Services.FoodProduct;
using RecipeSolverAPI.Services.Mailer;
using RecipeSolverAPI.Services.MailerService;
using RecipeSolverAPI.Services.PantryItem;
using RecipeSolverAPI.Services.Recipe;
using RecipeSolverAPI.Templates;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Connect to db
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMailerService, MailerService>();
builder.Services.AddScoped<ITemplate, Template>();

builder.Services.AddScoped<IFoodProductService, FoodProductService>();
builder.Services.AddScoped<IPantryItemService, PantryItemService>();

builder.Services.AddScoped<IRecipeService, RecipeService>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//cors
builder.Services.AddCors(options => options.AddPolicy(name: "All",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));



var app = builder.Build();

DbInitializer.Seed(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("All");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
