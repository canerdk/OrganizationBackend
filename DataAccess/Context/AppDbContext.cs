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

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<SurveyResponseAnswer> SurveyResponseAnswers { get; set; }
        public DbSet<UserContract> UserContracts { get; set; }

    }
}
