using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using hw_12_dop1.Models;
using hw_12_dop1.ViewModel;

namespace hw_12_dop1.Controllers;

public class HomeController : Controller
{
    private readonly IMapper _mapper;

    public HomeController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        UserViewModel uvm = _mapper.Map<UserViewModel>(new User()
        {
            Id = 1,
            FirstName = "FName",
            LastName = "LName",
            Email = "Email",
            Password = "****"
        });

        User user = new User();
        new AppMappingProfile().Map(user, uvm);
        
        
        
        return Content($"{uvm} \n{user} \n{user.ToUserViewModel()}");
    }
}