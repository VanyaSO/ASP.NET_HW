using hw_18.Data;
using hw_18.Helpers;
using hw_18.Interfaces;
using hw_18.Models;
using hw_18.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hw_18.Controllers;

[Authorize]
public class JournalController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ISubject _subjects;
    private readonly IGrade _grades;
    
    public JournalController(UserManager<User> userManager, ISubject subjects, IGrade grades)
    {
        _userManager = userManager;
        _subjects = subjects;
        _grades = grades;
    } 
    
    [Route("/journal")]
    public async Task<IActionResult> Index([FromServices]EmailsConfig emailsConfig)
    {
        if (!HttpContext.User.IsHasAccess(HttpContext)) return NotFound();
        
        var emails = emailsConfig.Emails.Select(e => e.ToLower()).ToList();
        var subjects = await _subjects.GetAllSubjectsWithGradesAsync();
        var users = _userManager.Users
            .Where(u => !emails.Contains(u.Email.ToLower()))
            .Select(u => new UserJournalViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            })
            .ToList();

        var model = new JournalViewModel()
        {
            Users = users,
            Subjects = subjects
        };
    
        return View(model);   

    }

    [HttpPost]
    [Route("/update-grades")]
    public async Task<IActionResult> UpdateGrades([FromBody] List<GradeUpdateViewModel> grades)
    {
        if (!HttpContext.User.IsHasAccess(HttpContext)) return NotFound();
        
        if(grades.Count != null) {
            var selectedGrades = grades.Where(g => g.IsUpdated).ToList();
            var updatedGrades = new List<Grade>();
            var createdGrades = new List<Grade>();
        
            foreach (var grade in selectedGrades)
            {
                if (string.IsNullOrEmpty(grade.Id))
                {
                    createdGrades.Add(new Grade()
                    {
                        UserId = grade.UserId,
                        SubjectId = grade.SubjectId,
                        Numbers = new List<int>() { grade.Number }
                    });
                    continue;
                }

                Grade? findGrade = await _grades.GetGradeByIdAsync(grade.Id);
                if (findGrade != null)
                {
                    findGrade.Numbers.Add(grade.Number);
                    updatedGrades.Add(findGrade);
                }
            }

            if (updatedGrades.Count != 0)
                await _grades.UpdateGradesAsync(updatedGrades);
        
            if (createdGrades.Count != 0)
                await _grades.CreateGradesAsync(createdGrades);
            
            return Ok();
        }

        return BadRequest();
    }

    [Route("/create-subject")]
    public async Task<IActionResult> CreateSubject()
    {
        return View();
    }

    [HttpPost]
    [Route("/create-subject")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSubject(CreateSubjectViewModel subject)
    {
        if (ModelState.IsValid)
        {
            bool isHaveSubject = await _subjects.IsHasSubjectAsync(subject.Name);
            if (!isHaveSubject)
            {
                await _subjects.CreateSubjectAsync(new Subject()
                {
                    Name = subject.Name,
                    Users = _userManager.Users.ToList()
                });

                return RedirectToAction("Index");
            }
            
            ModelState.AddModelError("", "Предмет с таким названием уже существует");
        }
        
        return View(subject);
    }
}