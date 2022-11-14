using System.Linq.Expressions;
using ReportSystem.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static ReportSystem.Repositories.GeneralRepository;
namespace ReportSystem.Repositories;

public class CrudRepository
{
    /// <summary>
    /// is used to create services within the scope to control the db connection.
    /// </summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CrudRepository(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// Template for using the database
    /// </summary>
    /// <param name="action"></param>
    private async Task Template(Action<AuthDbContext>? action = default)
    {
        //using serviceScopeFactory to create a scope of CafeNHLContext
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        {
            //CafeNHLContext
            var context = scope.ServiceProvider.GetService<AuthDbContext>();

            //check if CafeNHLContext isn't null
            if (context == null) return;

            //invoke action
            action?.Invoke(context);

            //save database changes
            await context.SaveChangesAsync();
        }
    }

    public async Task AddRow<T>(T? obj)
    {
        await Template(Action);

        void Action(AuthDbContext context)
        {
            if (obj != null) context.Add(obj);
        }
    }

    public async Task<List<T>> ReadAllRows<T>() where T : class
    {
        //create new generic list
        var result = new List<T>();

        //read all rows from database
        await Template(context => { context.Set<T>().ToList().ForEach(x => result.Add(x)); });

        //return
        return result;
    }

    public async Task RemoveRow<T>(T obj) where T : new() =>
        await Template(context =>
        {
            if (obj != null) context.Remove(obj);
        });
    
    public async Task UpdateRow<T>(T? obj) =>
        await Template(context =>
        {
            if (obj != null) context.Update(obj);
        });
    
    public async Task<T?> FindRowWithValue<T>(Predicate<T> match) where T : class, new()
    {
        //create new generic list
        var result = new T();

        //use template
        await Template(context =>
        {
            //finds the first result, or sets result to null
            result = context.Set<T>().ToList().FirstOrDefault(x => match(x));
        });

        //return result
        return result;
    }
    public async Task<List<T>> FindRowsWithValue<T>(Predicate<T> match) where T : class, new()
    {
        //create new generic list
        var result = new List<T>();

        //use template
        await Template(context =>
        {
            //finds the first result, or sets result to null
            result = context.Set<T>().ToList().Where(x => match(x)).ToList();
        });

        //return result
        return result;
    }
    public async Task UpdateRows<T>(List<T>? obj) where T : class, new() =>
        await Template(context => obj?.ForEach(x => context.Update(x)));
    /// <summary>
    /// finds row with supplied value
    /// </summary>
    /// <param name="match"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>list of object class that have the supplied value in them</returns>
    public async Task<T> FindRow<T>(Expression<Func<T, bool>> match) where T : class, new()
    {
        //create new generic list
        var result = new T();

        //use template
        await Template(context =>
        {
            //finds the first result, or sets result to null
            result = context.Set<T>().FirstOrDefault(match);
        });

        //return result
        return result ?? new T();
    }
    
}