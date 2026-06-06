using System.Data;
using System.Collections.Generic;
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
                ?? throw new ArgumentNullException(nameof(configuration), "Chưa cấu hình DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        // 1. Lấy tất cả sinh viên
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var sql = "SELECT * FROM Students";
            using var db = CreateConnection();
            return await db.QueryAsync<Student>(sql);
        }

        // 2. Lấy 1 sinh viên theo ID
        public async Task<Student?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Students WHERE Id = @Id";
            using var db = CreateConnection();
            return await db.QueryFirstOrDefaultAsync<Student>(sql, new { Id = id });
        }

        // 3. Thêm mới sinh viên
        public async Task<int> CreateAsync(Student student)
        {
            var sql = "INSERT INTO Students (Name, Age) VALUES (@Name, @Age);";
            using var db = CreateConnection();
            return await db.ExecuteAsync(sql, student);
        }

        // 4. Cập nhật sinh viên
        public async Task<int> UpdateAsync(Student student)
        {
            var sql = "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id";
            using var db = CreateConnection();
            return await db.ExecuteAsync(sql, student);
        }

        // 5. Xóa sinh viên
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Students WHERE Id = @Id";
            using var db = CreateConnection();
            return await db.ExecuteAsync(sql, new { Id = id });
        }

        // 6. BÀI TẬP NÂNG CAO: Lấy sinh viên kèm khóa học (JOIN)
        public async Task<IEnumerable<Student>> GetAllWithCoursesAsync()
        {
            var sql = @"
                SELECT s.*, c.* FROM Students s
                LEFT JOIN StudentCourses sc ON s.Id = sc.StudentId
                LEFT JOIN Courses c ON sc.CourseId = c.Id";

            var studentDic = new Dictionary<int, Student>();
            using var db = CreateConnection();
            
            await db.QueryAsync<Student, Course, Student>(sql, (student, course) =>
            {
                if (!studentDic.TryGetValue(student.Id, out var existingStudent))
                {
                    existingStudent = student;
                    existingStudent.Courses = new List<Course>();
                    studentDic.Add(student.Id, existingStudent);
                }
                
                if (course != null)
                {
                    existingStudent.Courses.Add(course);
                }
                
                return existingStudent;
            }, splitOn: "Id");

            return studentDic.Values;
        }
    }
}