using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    // Đường dẫn API sẽ là: http://localhost:5200/api/students
    [Route("api/[controller]")] 
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        // Tiêm (Inject) Interface từ tầng Application vào đây
        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // Tạo endpoint GET để Swagger gọi dữ liệu
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi kết nối Database: {ex.Message}");
            }
        }
    }
}
