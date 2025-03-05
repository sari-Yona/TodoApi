using Microsoft.EntityFrameworkCore;
using TodoApi;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// הוספת קונפיגורציה
builder.Configuration.AddJsonFile("appsettings.json");

// גישה למחרוזת הקישור
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// הוספת DbContext לשירותים
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql")));



//הזרקת תלויות
builder.Services.AddSingleton<Item>();

builder.Services.AddEndpointsApiExplorer();
//פתיחה באמצעות SWAGGER
builder.Services.AddSwaggerGen();
//חיבור למסד הנתונים
builder.Services.AddDbContext<ToDoDbContext>();
//אישור אבטחה
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




var app = builder.Build();


//מצב מפתח 
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

//שימוש בקורס
app.UseCors();
app.MapGet("/",  ()=>"Hello World!");
//הפונקציונליות של האתר
app.MapGet("/", async (ToDoDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapPost("/",
        async ([FromBody] Item newItem, 
        ToDoDbContext db) => {
                 var item = new Item 
                { 
                        Name = newItem.Name, 
                        IsComplete = false 
                };
                db.Items.Add(item);
               return await db.SaveChangesAsync();               
                });

app.MapPut("/{Id}", 
    async (int Id, 
    [FromBody] Item updatedItem, 
    ToDoDbContext db) => 
{
    var existingItem = await db.Items.FindAsync(Id);    
    if (existingItem is null) return Results.NotFound();
    existingItem.IsComplete = updatedItem.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/{Id}",async(int Id, ToDoDbContext db)=>{
        var existingItem = await db.Items.FindAsync(Id);    
        if (existingItem is null) return Results.NotFound();
        db.Remove(existingItem);
        await db.SaveChangesAsync();
        return Results.NoContent();


});


app.Run();
