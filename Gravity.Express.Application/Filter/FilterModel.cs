using Gravity.Express.Application.Common;
using Sieve.Models;

namespace Gravity.Express.Application.Filter;

public class FilterModel : SieveModel
{
    public FilterModel()
    {
        Page = Constants.DefaultPageNumber;
        PageSize = Constants.DefaultPageSize;
    }
}
