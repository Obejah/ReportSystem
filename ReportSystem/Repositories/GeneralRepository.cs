using static System.Guid;
namespace ReportSystem.Repositories;

public class GeneralRepository
{
    public static bool parseId(IHttpContextAccessor? httpContext, out Guid guid)
    {
        if (httpContext?.HttpContext == null)
        {
            guid = default;
            return false;
        }

        var id = httpContext.HttpContext.Request.Query["PointId"].ToString();

        TryParse(id, out guid);

        return guid != new Guid();
    }
}