using Application.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm các dịch vụ vào Container (Dependency Injection)
builder.Services.AddControllers();

// Đăng ký Repository để kết nối Interface và Class thực thi bằng Dapper
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Tự động cấu hình tài liệu Swagger/OpenAPI để test API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Cấu hình HTTP request pipeline (Middleware)
    // Bật giao diện Swagger khi chạy ở môi trường Development (Code máy local)
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseAuthorization();

// Định tuyến các API Controller
app.MapControllers();

app.Run("http://localhost:5200");