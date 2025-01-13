using System.ComponentModel.DataAnnotations;

namespace lesson_08_01_blog.ViewModels;

public class CategoryViewModel
{
    [Key]
    public Guid? Id { get; set; }

    [Display(Name = "Название")]
    [Required(ErrorMessage = "Не указано название категории")]
    public string? Name { get; set; }

    [Display(Name = "Описание")]
    public string? Description { get; set; }
}
