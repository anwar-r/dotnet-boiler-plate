
namespace Data.Base;

public interface IEntity
{
    long Id { get; }
    bool IsNew { get; }
    long CreatedById { get; }
    DateTime? CreatedTime { get; }
    long? UpdatedById { get; }
    DateTime? UpdatedTime { get; }
    void UpdateAuditFields(EntityState state, long userId, DateTime dateTime);

}