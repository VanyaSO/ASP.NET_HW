using hw_18.Models;

namespace hw_18.ViewModels;

public class JournalViewModel
{
    public List<UserJournalViewModel> Users { get; set; }
    public List<Subject> Subjects { get; set; }
}