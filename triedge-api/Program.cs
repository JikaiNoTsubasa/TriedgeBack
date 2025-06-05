using log4net;
using Newtonsoft.Json.Converters;
using triedge_api.Database;
using triedge_api.Global;
using triedge_api.JobManagers;

var builder = WebApplication.CreateBuilder(args);

// Add log4net
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

ILog log = LogManager.GetLogger(typeof(Program));

builder.Services.AddDbContext<TriContext>();
builder.Services.AddScoped<TriManager>();
builder.Services.AddScoped<BlogManager>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddControllers(o =>
{
    o.ModelBinderProviders.Insert(0, new UTCDateTimeBinderProvider());
})
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Disables the string conversions from empty to null
builder.Services.AddMvc().AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider()));

var app = builder.Build();

// Disable CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

log.Info("Init data");
using var scope = app.Services.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager>();
log.Info("Init admin user");
ProjectInit.InitAdminUser(userManager);

app.Run();
log.Info("Application started");