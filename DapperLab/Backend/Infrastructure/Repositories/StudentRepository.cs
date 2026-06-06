using System.Data;
using Application.Interfaces;
using Domain.Entities;
using Dapper;
using MySql.Data.MySqlClient; 
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), "Chưa cấu hình DefaultConnection trong appsettings.json");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var sql = "SELECT * FROM Students";
            using var db = CreateConnection();
            return await db.QueryAsync<Student>(sql);
        }
    }
}