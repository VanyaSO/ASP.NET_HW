using hw_18.Models;

namespace hw_18.Interfaces;

public interface ISubject
{
    public Task<List<Subject>> GetAllSubjectsAsync();
    public Task<List<Subject>> GetAllSubjectsWithGradesAsync();
    public Task<bool> IsHasSubjectAsync(string name);
    public Task CreateSubjectAsync(Subject subject);
}