using hw_18.Data;
using hw_18.Interfaces;
using hw_18.Models;
using Microsoft.EntityFrameworkCore;

namespace hw_18.Repositories;

public class SubjectRepository : ISubject
{
    private readonly ApplicationContext _context;
    
    public SubjectRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<Subject>> GetAllSubjectsAsync() => await _context.Subjects.ToListAsync();
    
    public async Task<List<Subject>> GetAllSubjectsWithGradesAsync() => await _context.Subjects.Include(s => s.Grades).ToListAsync();

    public async Task<bool> IsHasSubjectAsync(string name) => await _context.Subjects.AnyAsync(s => s.Name.ToLower().Equals(name.ToLower()));

    public async Task CreateSubjectAsync(Subject subject)
    {
        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
    }
}