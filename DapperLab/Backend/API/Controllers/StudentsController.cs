using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: /api/students
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students);
        }

        // GET: /api/students/courses (Endpoint nâng cao đặt phía trên để tránh nhầm với id)
        [HttpGet("courses")]
        public async Task<IActionResult> GetAllWithCourses()
        {
            var result = await _studentRepository.GetAllWithCoursesAsync();
            return Ok(result);
        }

        // GET: /api/students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound($"Không tìm thấy sinh viên có ID = {id}");
            return Ok(student);
        }

        // POST: /api/students
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var result = await _studentRepository.CreateAsync(student);
            return Ok(new { message = "Thêm thành công!", rowsAffected = result });
        }

        // PUT: /api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            student.Id = id;
            var result = await _studentRepository.UpdateAsync(student);
            if (result == 0) return NotFound("Cập nhật thất bại, không tìm thấy sinh viên.");
            return Ok(new { message = "Cập nhật thành công!" });
        }

        // DELETE: /api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentRepository.DeleteAsync(id);
            if (result == 0) return NotFound("Xóa thất bại, không tìm thấy sinh viên.");
            return Ok(new { message = "Xóa sinh viên thành công!" });
        }
    }
}