using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.Punten;

public class PointsOverview : PageModel
{
    private readonly CrudRepository _crudRepository;
    private readonly UserRepository _userRepository;


    public PointsOverview(CrudRepository crudRepository, UserRepository userRepository)
    {
        _crudRepository = crudRepository;
        _userRepository = userRepository;
        _Points = crudRepository.ReadAllRows<Point>().Result;
    }

    [BindProperty] public Point? Point { get; set; }

    public List<Point>? _Points { get; set; }

    public async Task<IActionResult> OnPostUpdatePoint()
    {
        if (_Points != null)
        {
            await _crudRepository.UpdateRow(Point);
        }
        return RedirectToPage("/Punten/PointsOverview");
    }
    public async Task<IActionResult> OnPostDeletePoint()
    {
        if (_Points != null)
        {
            await _crudRepository.RemoveRow(_Points.First(x => Point != null && x.PointId == Point.PointId));
            _Points.RemoveAll(x => Point != null && x.PointId == Point.PointId);
        }

        return RedirectToPage("/Punten/PointsOverview");

    }

    public async Task<IActionResult> OnPostQr()
    {
        return Redirect("/qrcode?PointId=" + Point.PointId);
    }
}
    
