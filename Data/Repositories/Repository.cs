using Data.Base;
using Data.util;

namespace Data.Repositories
{
    public class Repository : DbContext
    {
        private readonly IUserContext _userContext;
        public Repository(DbContextOptions<Repository> options, IUserContext userContext) : base(options)
        {
            _userContext = userContext;
        }



        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        private void UpdateAuditFields()
        {
            var eligibleEntries = ChangeTracker.Entries<IEntity>()
                .Where(x => x.State is EntityState.Added or EntityState.Modified).ToList();
            foreach (var entry in eligibleEntries)
            {
                if (entry.Entity is IEntity entity)
                    entity.UpdateAuditFields(entry.State, _userContext.CurrentUserId, DateTime.Now);
            }
        }
    }
}
