using DataAccess.Utilities.Auth;
using Entities.Common;
using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly ILoginUserManager _loginUser;

        public AppDbContext(DbContextOptions options, ILoginUserManager loginUser) : base(options)
        {
            _loginUser = loginUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            entry.Entity.CreatedDate = DateTime.Now;
                            entry.Entity.IsDeleted = false;
                            entry.Entity.CreatedUserId = _loginUser.GetLoginUserId();
                            entry.Entity.TenantId = _loginUser.GetLoginUserTenant();
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entry.Property(x => x.CreatedDate).IsModified = false;
                            entry.Entity.ModifiedDate = DateTime.Now;
                            break;
                        }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
