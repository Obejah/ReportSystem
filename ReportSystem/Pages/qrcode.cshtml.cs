using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Repositories;

namespace ReportSystem.Pages;

public class qrcode : PageModel
{
    
    public readonly Guid PointId;
    public qrcode(IHttpContextAccessor httpContext)
    {
        GeneralRepository.parseId(httpContext, out PointId);
    }
}