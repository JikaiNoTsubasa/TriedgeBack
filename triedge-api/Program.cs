using log4net;
using Newtonsoft.Json.Converters;
using triedge_api.Database;
using triedge_api.Global;

var builder = WebApplication.CreateBuilder(args);

// Add log4net
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

ILog log = LogManager.GetLogger(typeof(Program));

builder.Services.AddDbContext<TriContext>();

builder.Services.AddControllers(o => {
        o.ModelBinderProviders.Insert(0, new UTCDateTimeBinderProvider());
    })
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

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


app.Run();
log.Info("Application started");