using Core_API_ProductInfo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EcommContext>(options => 
{
      options.UseSqlServer(builder.Configuration.GetConnectionString("AppStr"));
    });
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options => 
{
    options.AddPolicy("cors", policy => 
    {
       policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();    
    });    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("cors");
app.MapGet("/products", async (EcommContext ctx) => 
{
    var products = await ctx.ProductInfos.ToListAsync();    
    return Results.Ok(products);
});

app.MapGet("/products/{id}", async (EcommContext ctx, string id) => 
{
    var product = await ctx.ProductInfos.FindAsync(id);    
    return Results.Ok(product);
});
app.MapPost("/products", async (EcommContext ctx, ProductInfo product) => 
{
     var entity = await ctx.ProductInfos.AddAsync(product);
     await ctx.SaveChangesAsync();
     return Results.Ok(entity.Entity);
});
app.MapPut("/products/{id}", async (EcommContext ctx, string id, ProductInfo updatedProduct) => 
{
    var product = await ctx.ProductInfos.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }

    product.ProductName = updatedProduct.ProductName;
    product.CategoryName = updatedProduct.CategoryName;
    product.Description = updatedProduct.Description;
    product.UnitPrice = updatedProduct.UnitPrice;

    await ctx.SaveChangesAsync();
    return Results.Ok(product);
});

app.MapDelete("/products/{id}", async (EcommContext ctx, string id) => 
{
    var product = await ctx.ProductInfos.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }

    ctx.ProductInfos.Remove(product);
    await ctx.SaveChangesAsync();
    return Results.Ok(product);
});

    

app.Run();

