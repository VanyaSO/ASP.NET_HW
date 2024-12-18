using hw_12_dop1.ViewModel;

namespace hw_12_dop1.Models;

public static class UserExtensions
{
    public static User ToUser(this UserViewModel emv)
    {
        return new User
        {
            Id = 0,
            FirstName = emv.FirstName,
            LastName = emv.LastName,
            Email = emv.Email,
            Password = emv.Password
        };
    }
    
    public static UserViewModel ToUserViewModel(this User user)
    {
        return new UserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password
        };
    }
}