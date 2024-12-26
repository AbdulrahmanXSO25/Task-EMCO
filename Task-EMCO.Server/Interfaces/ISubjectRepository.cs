namespace Task_EMCO.Server.Interfaces;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetAllAsync(int pageNumber, int pageSize, string search = null);
    Task<IEnumerable<Subject>> GetAllAsync(string search = null);
    Task<int> GetTotalCountAsync(string search = null);
    Task<Subject> GetByIdAsync(int id);
    Task CreateAsync(Subject subject);
    Task UpdateAsync(Subject subject);
    Task DeleteAsync(int id);
}
