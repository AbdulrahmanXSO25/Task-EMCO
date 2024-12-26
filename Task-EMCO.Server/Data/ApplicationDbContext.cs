namespace Task_EMCO.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            
            entity.Ignore(p => p.AccessFailedCount);
            entity.Ignore(p => p.TwoFactorEnabled);
            entity.Ignore(p => p.LockoutEnabled);
            entity.Ignore(p => p.LockoutEnd);
            entity.Ignore(p => p.ConcurrencyStamp);
            entity.Ignore(p => p.SecurityStamp);

            entity.Property(u => u.EmailConfirmed).HasDefaultValue(true);
            entity.Property(u => u.PhoneNumberConfirmed).HasDefaultValue(true);
        });

        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}