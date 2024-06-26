using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
   {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
   });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
   });

var pizzas = app.MapGroup("/pizzas");

pizzas.MapGet("/{id}", (int id) => PizzaDB.GetPizza(id));
pizzas.MapGet("/", () => PizzaDB.GetPizzas());
pizzas.MapPost("/", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
pizzas.MapPut("/", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
pizzas.MapDelete("/{id}", (int id) => PizzaDB.RemovePizza(id));

app.Run();
