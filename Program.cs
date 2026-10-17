using BooksApiDemo.Authorization;
using BooksApiDemo.Repositories;
using BooksApiDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IBookRepository, BookRepository>();

var auth0Domain = builder.Configuration["Auth0:Domain"]
    ?? throw new InvalidOperationException("Auth0:Domain no está configurado.");

var auth0Audience = builder.Configuration["Auth0:Audience"]
    ?? throw new InvalidOperationException("Auth0:Audience no está configurado.");

var usersApiBaseUrl = builder.Configuration["UsersApi:BaseUrl"]
    ?? throw new InvalidOperationException("UsersApi:BaseUrl no está configurado.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{auth0Domain.TrimEnd('/')}";
        options.Audience = auth0Audience;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IAuthorizationApiService, AuthorizationApiService>(client =>
{
    client.BaseAddress = new Uri(usersApiBaseUrl);
});

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:books", policy =>
        policy.Requirements.Add(new PermissionRequirement("read:books")));

    options.AddPolicy("create:books", policy =>
        policy.Requirements.Add(new PermissionRequirement("create:books")));

    options.AddPolicy("update:books", policy =>
        policy.Requirements.Add(new PermissionRequirement("update:books")));

    options.AddPolicy("delete:books", policy =>
        policy.Requirements.Add(new PermissionRequirement("delete:books")));
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
