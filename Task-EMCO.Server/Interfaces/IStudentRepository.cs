namespace Task_EMCO.Server.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync(int pageNumber, int pageSize, string search = null);
    Task<IEnumerable<Student>> GetAllAsync(string search = null);
    Task<int> GetTotalCountAsync(string search = null);
    Task<Student> GetByIdAsync(int id);
    Task CreateAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
}