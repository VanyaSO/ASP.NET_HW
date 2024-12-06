using hw_9_dop_2.Models;

namespace hw_9_dop_2.Services;

public class CategoryService
{
    private List<Category> _categories = new List<Category>
    {
        new Category 
        { 
            Id = 1, 
            Title = "Набор мышечной массы", 
            Description = "Эта категория включает в себя комплексы упражнений и диеты, направленные на увеличение мышечной массы." 
        },
        new Category 
        { 
            Id = 2, 
            Title = "Для похудения", 
            Description = "Категория с рекомендациями по снижению жировой массы и улучшению общего состояния здоровья." 
        },
        new Category 
        { 
            Id = 3, 
            Title = "Людям за 60", 
            Description = "Программы и советы, подходящие для людей старшего возраста, чтобы поддерживать физическую активность." 
        },
        new Category 
        { 
            Id = 4, 
            Title = "Для выносливости", 
            Description = "Программы тренировок, направленные на повышение физической выносливости и улучшение аэробной активности." 
        }
    };

    public IEnumerable<Category> GetAllCategories() => _categories.ToList();
    public Category? GetCategoryById(int id) => _categories.FirstOrDefault(c => c.Id == id);
}