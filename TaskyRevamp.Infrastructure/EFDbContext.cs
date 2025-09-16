using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Models.Users.UserDelegations;

namespace SurveyRevamp.Infrastructure;

public class EfDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DbSet<User> Users { get; set; }
    public DbSet<UserDelegation> UserDelegation { get; set; }
    public DbSet<TaskItem> TaskItem { get; set; }
    public DbSet<RecycleBinSettings> RecycleBinSettings { get; set; }
    public DbSet<RejectionSettings> RejectionSettings { get; set; }
    public DbSet<PrioritySettings> PrioritySettings { get; set; }
	public DbSet<StatusSettings> StatusSettings { get; set; }
    public DbSet<ViewTaskSettings> ViewTaskSettings { get; set; }
    public DbSet<DefaultViewSettings> DefaultViewSettings { get; set; }

    public DbSet<WorkingDaysSettings> WorkingDaysSettings { get; set; }
	public EfDbContext(DbContextOptions<EfDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<int> SaveChangeWithoutUpdate()
    {
        return await base.SaveChangesAsync();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var Id = _httpContextAccessor.HttpContext == null ? "" : _httpContextAccessor.HttpContext.User.Identity?.Name;
        Guid currentUserId;
        if (Guid.TryParse(Id, out currentUserId))
            Guid.TryParse(Id, out currentUserId);

        var insertedEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added).ToList();

        foreach (var insertedEntry in insertedEntries)
        {
            if (insertedEntry.Entity is IHasCreationMetaData creationMetaData)
            {
                if (creationMetaData.CreatedById == null || (creationMetaData.CreatedById != null && creationMetaData.CreatedById == Guid.Empty))
                    creationMetaData.CreatedById = currentUserId;
                creationMetaData.CreateDate = DateTime.UtcNow;
            }
            if (insertedEntry.Entity is IHasUpdateMetaData updateData)
            {
                if (currentUserId != null && currentUserId != Guid.Empty)

                    updateData.UpdatedById = currentUserId;
                updateData.UpdateDate = DateTime.UtcNow;
            }
        }

        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified).ToList();

        foreach (var modifiedEntry in modifiedEntries)
        {
            if (modifiedEntry.Entity is IHasUpdateMetaData updateMetaData)
            {
                if (updateMetaData.UpdatedById == null || (updateMetaData.UpdatedById != null && updateMetaData.UpdatedById == Guid.Empty))
                    updateMetaData.UpdatedById = currentUserId;

                updateMetaData.UpdateDate = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }


    public EfDbContext(DbContextOptions<EfDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfDbContext).Assembly);


        foreach (var fk in modelBuilder.Model
         .GetEntityTypes()
         .SelectMany(t => t.GetForeignKeys())
         .Where(fk => fk.PrincipalEntityType.ClrType == typeof(User)))
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        //var serverTimeZone = TimeZoneInfo.Local; // Your server's timezone

        //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //{
        //    foreach (var property in entityType.GetProperties()
        //                 .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
        //    {
        //        var converter = new ValueConverter<DateTime, DateTime>(
        //            v => v.ToUniversalTime(), // When saving: convert to UTC
        //            v => TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(v, DateTimeKind.Utc), serverTimeZone) // When reading: convert to local
        //        );

        //        property.SetValueConverter(converter);
        //    }
        //}

    }
}