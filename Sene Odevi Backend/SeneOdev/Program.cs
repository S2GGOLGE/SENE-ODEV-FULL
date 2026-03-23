using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SeneOdev;

var builder = WebApplication.CreateBuilder(args);

// CORS politikası ekle: tüm kaynaklardan gelen istekleri kabul et
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// LOGIN ENDPOINT
app.MapPost("/login", ([FromBody] LoginRequest request) =>
{
    // Frontend'den gelen login isteğini CMD ekranına yazdır
    Console.WriteLine("LOGIN İSTEĞİ GELDİ");
    Console.WriteLine($"Username: {request.Username}");
    Console.WriteLine($"Password: {request.Password}");

    string sonuc = Login.GirisYap(request.Username, request.Password);

    if (sonuc == "OK")
        return Results.Ok(new { success = true, message = "Giriş başarılı" });

    return Results.BadRequest(new { success = false, message = sonuc });
});

// SIGNUP ENDPOINT
app.MapPost("/sign_up", ([FromBody] SignupRequest request) =>
{
    // Frontend'den gelen kayıt isteğini CMD ekranına yazdır
    Console.WriteLine("SIGNUP İSTEĞİ GELDİ");
    Console.WriteLine($"Name: {request.Name}");
    Console.WriteLine($"Surname: {request.Surname}");
    Console.WriteLine($"Username: {request.Username}");
    Console.WriteLine($"Email: {request.Email}");
    Console.WriteLine($"Phone: {request.Phone}");
    Console.WriteLine($"Gender: {request.Gender}");
    Console.WriteLine($"Password: {request.Password}");
    Console.WriteLine($"Password Repeat: {request.PasswordRepeat}");

    var user = new KayitOl
    {
        // Yeni kullanıcı ekle
        Name = request.Name,
        Surname = request.Surname,
        Username = request.Username,
        Email = request.Email,
        Phone = request.Phone,
        Gender = request.Gender,
        Password = request.Password,
        PasswordRepeat = request.PasswordRepeat
    };

    bool sonuc = user.Kayit();

    if (sonuc)
        return Results.Ok(new { success = true, message = "Kayıt başarılı" });

    return Results.BadRequest(new { success = false, message = "Kayıt başarısız" });
});

// ADMIN LOGIN ENDPOINT
app.MapPost("/adminlogin", ([FromBody] AdminLoginRequest request) =>
{
    // Admin giriş isteğini CMD ekranına yazdır
    Console.WriteLine("ADMIN LOGIN İSTEĞİ GELDİ");
    Console.WriteLine($"Username: {request.Username}");
    Console.WriteLine($"Password: {request.Password}");

    string sonuc = admin.Login(request.Username, request.Password, request.Role);

    if (sonuc == "OK")
        return Results.Ok(new { success = true, message = "Giriş başarılı" });

    return Results.BadRequest(new { success = false, message = sonuc });
});

// SUNUCU ENDPOINT (henüz hazır değil)
app.MapGet("/sunucu", ([FromBody] AdminLoginRequest request) =>
{
    SUNUCU.Client("127.0.0.1", 8587);
});

app.Run();

// DTO TANIMLARI
// DTO: Data Transfer Object, veri kapsülleme için kullanılır, mantıksal işlem içermez
public record LoginRequest(
    string Username,
    string Password
);

public record SignupRequest(
    string Name,
    string Surname,
    string Username,
    string Email,
    string Phone,
    string Gender,
    string Password,
    string PasswordRepeat
);

public record AdminLoginRequest(
    string Username,
    string Password,
    string Role
);