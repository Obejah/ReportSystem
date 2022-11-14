using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.Punten;


public class AddPoint : PageModel
{
    private readonly CrudRepository _crudRepository;
    private readonly UserRepository _userRepository;


    public AddPoint(CrudRepository crudRepository, UserRepository userRepository)
    {
        _crudRepository = crudRepository;
        _userRepository = userRepository;
    }

    
    
    [BindProperty] public Point? Point { get; set; }

    public Option? Option { get; set; } = new Option();

    public async Task<IActionResult> OnPostAddPoint()
    {
        
        Point.ApplicationUserId = _userRepository.GetCurrentUserId();
        if (ModelState.IsValid)
        {
            Guid pointId = Guid.NewGuid();
            Point.PointId = pointId;
            await _crudRepository.AddRow(Point);
            Option.OptionName = "Anders";
            Option.Urgency = 3;
            Option.PointId = Point.PointId;
            
            await _crudRepository.AddRow(Option);
            return Redirect("/qrcode?PointId=" + pointId);
        }
        return Page();
    }
    
}