using Domain.Entities;

namespace Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);                 
        Task<int> CreateAsync(Student student);             
        Task<int> UpdateAsync(Student student);          
        Task<int> DeleteAsync(int id);         
        Task<IEnumerable<Student>> GetAllWithCoursesAsync(); 
    }
}