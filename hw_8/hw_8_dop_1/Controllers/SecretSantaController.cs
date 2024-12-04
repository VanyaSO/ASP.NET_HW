using Microsoft.AspNetCore.Mvc;

namespace hw_8_dop_1.Controllers;

public class SecretSantaController : Controller
{
    private static Dictionary<string, bool> _participants = new Dictionary<string, bool>()
    {
        {"Aliсe", true},
        {"Misha", true},
        {"Alex", true},
        {"Ivan", true},
        {"Tim", true}
    };
    private static Dictionary<string, string> _result = new Dictionary<string, string>();
    private readonly Random _random = new Random();

    public IActionResult Index()
    {
        return View(GetNames());
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(string? name)
    {
        if (name is not null && !_participants.ContainsKey(name))
            _participants.Add(name, true);
        
        return PartialView("_Participants", GetNames());
    }
    
    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        _participants = new Dictionary<string, bool>();
        _result = new Dictionary<string, string>();
        return PartialView("_Participants", GetNames());
    }
    
    public async Task<IActionResult> Distribute()
    {
        DistributeNames();
        return View(_result);
    }
    
    public IActionResult RevealName(string name)
    {
        return Content(_result[name]);
    }
    
    private List<string> GetNames() => _participants
        .Select(p => p.Key)
        .ToList();

    private void DistributeNames()
    {
        List<string> participantsNames = _participants
            .Select(p => p.Key)
            .ToList();

        for (int i = 0; i < participantsNames.Count; i++)
        {
            var availableNames = GetAvailableNames();
            string giver = participantsNames[i];
            string recipient = availableNames.Count !=0 ? availableNames[_random.Next(0, availableNames.Count)] : participantsNames[i];

            _result[giver] = recipient;
            _participants[recipient] = false;
        }
    }

    private List<string> GetAvailableNames() => _participants
        .Where(p => p.Value)
        .Select(p => p.Key)
        .ToList();
}