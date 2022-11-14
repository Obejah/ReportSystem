using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.Options;

public class AddOptions : PageModel
{
    private readonly CrudRepository _crudRepository;


    public AddOptions(CrudRepository crudRepository)
    {
        _crudRepository = crudRepository;
        _Points = _crudRepository.ReadAllRows<Point>().Result.ConvertAll(x => new SelectListItem()
        {
            Text = x.Name,
            Value = x.PointId.ToString()
        });
    }
    
    public List<SelectListItem>? _Points { get; set; }
    [BindProperty] public Option? Option { get; set; }
    
    public async Task<IActionResult> OnPostAddOption()
    {
        
        if (ModelState.IsValid)
        {
            await _crudRepository.AddRow(Option);
        }
        return RedirectToPage("/Options/OptionsOverview");
    }
}

