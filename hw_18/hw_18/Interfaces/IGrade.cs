namespace hw_18.Interfaces;

public interface IGrade
{
    public Task CreateGradesAsync(List<Grade> grades);
    public Task UpdateGradesAsync(List<Grade> grade);
    public Task<Grade> GetGradeByIdAsync(string id);
}