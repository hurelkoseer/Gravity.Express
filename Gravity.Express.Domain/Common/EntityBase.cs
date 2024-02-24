namespace Gravity.Express.Domain.Common;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public bool IsDelete { get; set; }

    public bool IsPassive { get; set; }
}