namespace Gravity.Express.Infrastructure.Persistence;

public interface IContextWrapper
{
    int GetTenantId();
}

public class NullContextWrapper : IContextWrapper
{
    public int GetTenantId()
    {
        return 0;
    }
}
