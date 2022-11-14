using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.Melding;

public class Meldingen : PageModel
{
    private readonly CrudRepository _crudRepository;
    private readonly bool succeeded;
    public readonly Guid PointId;
    public Point? Point { get; set; }

    public Meldingen(CrudRepository crudRepository, IHttpContextAccessor httpContext)
    {
        _crudRepository = crudRepository;
        succeeded = GeneralRepository.parseId(httpContext, out PointId);

        Point = crudRepository.FindRow<Point>(x => x.PointId == PointId).Result;

        _Options = _crudRepository.ReadAllRows<Option>().Result.ConvertAll(x => new SelectListItem()
        {
            Text = x.OptionName,
            Value = x.PointId.ToString()
        });
        _Notices = crudRepository.ReadAllRows<Notice>().Result;
        _options = crudRepository.ReadAllRows<Option>().Result;
        _Points = crudRepository.ReadAllRows<Point>().Result;

        List<Option> _pointoptions = new List<Option>();
        foreach (var option in _options)
        {
            if (option.PointId == Point.PointId)
            {
                _pointoptions.Add(option);
            }
        }

        _PointOptions = _pointoptions.ConvertAll(a =>
        {
            return new SelectListItem()
            {
                Text = a.OptionName,
                Value = a.OptionId.ToString(),
                Selected = false
            };
        });
    }


    public Option? Option { get; set; }
    [BindProperty] public Notice? Notice { get; set; }
    public List<SelectListItem>? _Options { get; set; }
    public List<SelectListItem>? _PointOptions { get; set; }
    public List<Notice>? _Notices { get; set; }
    public List<Option>? _options { get; set; }
    public List<Point>? _Points { get; set; }


    public async Task<IActionResult> OnPostAddMelding()
    {
        if (ModelState.IsValid)
        {
            await _crudRepository.AddRow(Notice);
        }

        return Redirect("/Melding/Meldingen?PointId=" + PointId);
    }
}