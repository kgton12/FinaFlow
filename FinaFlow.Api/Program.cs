using FinaFlow.Api.Data;
using FinaFlow.Api.Handlers;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = $"Data Source=D:\\DB-Projetos\\FinaFlow.db";

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(connectionString));

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.MapGet("/", (GetCategoryByIdRequest request, ICategoryHandler handler) => handler.GetByIdAsync(request));

app.Run();
