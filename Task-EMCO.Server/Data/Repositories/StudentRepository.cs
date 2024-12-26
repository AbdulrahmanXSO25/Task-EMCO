namespace Task_EMCO.Server.Data.Repositories;

public class StudentRepository(ApplicationDbContext context) : IStudentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Student>> GetAllAsync(int pageNumber, int pageSize, string search = null)
    {
        IQueryable<Student> query = _context.Students.Include(s => s.StudentSubjects).ThenInclude(ss => ss.Subject).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.FirstName.Contains(search) || s.SecondName.Contains(search) || s.LastName.Contains(search) || s.Email.Contains(search) || s.PhoneNumber.Contains(search));
        }

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetAllAsync(string search = null)
    {
        IQueryable<Student> query = _context.Students.Include(s => s.StudentSubjects).ThenInclude(ss => ss.Subject).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.FirstName.Contains(search) || s.SecondName.Contains(search) || s.LastName.Contains(search) || s.Email.Contains(search) || s.PhoneNumber.Contains(search));
        }

        return await query.ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string search = null)
    {
        IQueryable<Student> query = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.FirstName.Contains(search) || s.SecondName.Contains(search) || s.LastName.Contains(search) || s.Email.Contains(search) || s.PhoneNumber.Contains(search));
        }

        return await query.CountAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Students.Include(s => s.StudentSubjects).ThenInclude(ss => ss.Subject).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task CreateAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student student)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var dbStudent = await GetByIdAsync(student.Id);
            if (dbStudent == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            dbStudent.FirstName = student.FirstName;
            dbStudent.SecondName = student.SecondName;
            dbStudent.LastName = student.LastName;
            dbStudent.DateOfBirth = student.DateOfBirth;
            dbStudent.IsMale = student.IsMale;
            dbStudent.Email = student.Email;
            dbStudent.PhoneNumber = student.PhoneNumber;

            if (dbStudent.StudentSubjects != null)
            {
                _context.Set<StudentSubject>().RemoveRange(dbStudent.StudentSubjects);
            }

            dbStudent.StudentSubjects = student.StudentSubjects;
            
            _context.Students.Update(dbStudent);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}