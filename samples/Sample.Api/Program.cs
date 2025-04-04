using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ogu.AspNetCore.Compressions;
using Ogu.Compressions;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCompressions(opts =>
{
    opts.Level = CompressionLevel.Optimal;
});

builder.Services.AddResponseCompression(opts =>
{
    // Server decides which provider to use - (e.g. If client requests gzip, br - server will use available first encoding )
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.Providers.Add<ZstdCompressionProvider>();
    opts.Providers.Add<GzipCompressionProvider>();
    opts.Providers.Add<SnappyCompressionProvider>();
    opts.Providers.Add<DeflateCompressionProvider>();

    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes;

    opts.EnableForHttps = true;
});

builder.Services.AddTransient<DecompressionHandler>();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("DecompressionHandler").AddHttpMessageHandler<DecompressionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();