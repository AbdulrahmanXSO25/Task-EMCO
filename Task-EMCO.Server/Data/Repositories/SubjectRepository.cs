namespace Task_EMCO.Server.Data.Repositories;

public class SubjectRepository(ApplicationDbContext context) : ISubjectRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Subject>> GetAllAsync(int pageNumber, int pageSize, string search = null)
    {
        var query = _context.Subjects.Include(x => x.StudentSubjects).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.Code.Contains(search) || s.Name.Contains(search) || s.Description.Contains(search));
        }

        return await query.Skip(pageNumber * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
    }

    public async Task<IEnumerable<Subject>> GetAllAsync(string search = null)
    {
        var query = _context.Subjects.Include(x => x.StudentSubjects).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.Code.Contains(search) || s.Name.Contains(search) || s.Description.Contains(search));
        }

        return await query.ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string search = null)
    {
        var query = _context.Subjects.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.Code.Contains(search) || s.Name.Contains(search) || s.Description.Contains(search));
        }

        return await query.CountAsync();
    }

    public async Task<Subject> GetByIdAsync(int id)
    {
        return await _context.Subjects.Include(s => s.StudentSubjects).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task CreateAsync(Subject subject)
    {
        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subject subject)
    {
        var existingSubject = await _context.Subjects.FindAsync(subject.Id);
        
        if (existingSubject == null)
        {
            throw new Exception("Subject not found.");
        }

        existingSubject.Code = subject.Code;
        existingSubject.Name = subject.Name;
        existingSubject.Description = subject.Description;

        _context.Subjects.Update(existingSubject);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}