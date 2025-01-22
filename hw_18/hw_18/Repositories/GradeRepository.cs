using hw_18.Data;
using hw_18.Interfaces;

namespace hw_18.Repositories;

public class GradeRepository : IGrade
{
    private readonly ApplicationContext _context;
    
    public GradeRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task CreateGradesAsync(List<Grade> grades)
    {
        await _context.Grades.AddRangeAsync(grades);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateGradesAsync(List<Grade> grades)
    {
        _context.Grades.UpdateRange(grades);
        await _context.SaveChangesAsync();
    }

    public async Task<Grade?> GetGradeByIdAsync(string id) => _context.Grades.FirstOrDefault(e => e.Id.Equals(id));
}