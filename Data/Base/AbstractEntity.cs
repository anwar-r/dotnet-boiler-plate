
namespace Data.Base;

public abstract class AbstractEntity : IEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime? CreatedTime { get; set; }
    public long CreatedById { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public long? UpdatedById { get; set; }

    public bool IsNew => Id == 0;

    public void UpdateAuditFields(EntityState state,long userId, DateTime dateTime)
    {
        switch (state)
        {
            case EntityState.Added:
                CreatedById = userId;
                CreatedTime = dateTime;
                break;

            case EntityState.Modified:
                UpdatedById = userId;
                UpdatedTime = dateTime;
                break;
        }
    }
}