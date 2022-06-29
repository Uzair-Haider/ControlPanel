using ControlPanel.DAL;
using ControlPanel.IRepos;
using ControlPanel.IRepos.IAddressRepo;
using ControlPanel.IRepos.IUserAccountRepo;
using ControlPanel.IRepos.IUserRepo;
using ControlPanel.Repos.AddressRepo;
using ControlPanel.Repos.UserAccountRepo;
using ControlPanel.Repos.UserRepo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserCreateRepo, UserCreateRepo>();
builder.Services.AddTransient<IUserAccountCreateRepo, UserAccountCreate>();
builder.Services.AddTransient<IAddressCreate, AddressCreate>();
string connectionString = builder.Configuration.GetConnectionString("ControlPanelDb");
builder.Services.AddDbContext<CPContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
