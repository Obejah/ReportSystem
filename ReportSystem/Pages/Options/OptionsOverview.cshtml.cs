using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.Options;

public class OptionsOverview : PageModel
{
    private readonly CrudRepository _crudRepository;
    private readonly UserRepository _userRepository;


    public OptionsOverview(CrudRepository crudRepository, UserRepository userRepository)
    {
        _crudRepository = crudRepository;
        _userRepository = userRepository;
        _Options = crudRepository.ReadAllRows<Option>().Result;
        _Points = crudRepository.ReadAllRows<Point>().Result;
    }
    
    
    [BindProperty] public Point? Point { get; set; }
    [BindProperty] public Option? Option { get; set; }

    public List<Option>? _Options { get; set; }
    
    public List<Point>? _Points { get; set; }

    public async Task<IActionResult> OnPostUpdateOption()
    {
        if (_Options != null)
        {
            await _crudRepository.UpdateRow(Option);
        }
        return RedirectToPage("/Options/OptionsOverview");
    }
    public async Task<IActionResult> OnPostDeleteOption()
    {
        if (_Options != null)
        {
            await _crudRepository.RemoveRow(_Options.First(x => Option != null && x.OptionId == Option.OptionId));
            _Options.RemoveAll(x => Option != null && x.OptionId == Option.OptionId);
        }

        return RedirectToPage("/Options/OptionsOverview");

    }
}