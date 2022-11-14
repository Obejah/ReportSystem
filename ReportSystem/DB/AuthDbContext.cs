using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReportSystem.Models;

namespace ReportSystem.DB;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }
    
    
    public DbSet<Notice> Notice { get; set; } = null!;
    public DbSet<Point> Point { get; set; } = null!;
    public DbSet<Option> Option { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);



        builder.Entity<Option>().HasKey(o => new {o.OptionId});
        builder.Entity<Option>().HasOne(x => x.Point).WithMany(x => x.Option)
            .HasForeignKey(x => x.PointId);
        builder.Entity<Notice>().HasKey(n => new {n.NoticeId});
        builder.Entity<Notice>().HasOne(x => x.Option).WithMany(x => x.Notice)
            .HasForeignKey(x => x.OptionId);
        
        
        builder.Entity<ApplicationUser>(entity => { entity.ToTable("User"); });
        builder.Entity<IdentityRole>(entity => { entity.ToTable("Role"); });
        builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
        builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
        builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
        builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
        builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
    }
    
}