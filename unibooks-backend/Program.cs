using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using unibooks_backend.Data;
using Newtonsoft.Json;
using unibooks_backend.Interfaces;
using unibooks_backend.Repositories;
using unibooks_backend.Models;
using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using unibooks_backend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options => 
    {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }
);

builder.Services.AddDbContext<ApplicationDBContext>( options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<APPUser, IdentityRole>(options => {
     options.Password.RequireDigit = true;
     options.Password.RequireLowercase = true;
     options.Password.RequireUppercase = true;
     options.Password.RequireNonAlphanumeric = true;
     options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( options => {
     options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"], //check app settings
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])      //Add app.UseAuthentication(); app.UseAuthorization();
        )
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBookMarkRepository, BookmarkRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      //.WithOrigins("https://localhost:44351))
      .SetIsOriginAllowed(origin => true));


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();