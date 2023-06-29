var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAzureFileShareService, AzureFileShareService>();
builder.Services.AddScoped<ITableStorageService, TableStorageService>();
builder.Services.AddScoped<IAzureQueue, AzureQueue>();
builder.Services.AddScoped<IDeviceManagerService, DeviceManagerService>();
builder.Services.AddScoped<IAzureBlobService, AzureBlobService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.DocumentFilter<TitleFilter>());

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
