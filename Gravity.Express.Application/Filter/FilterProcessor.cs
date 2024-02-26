using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Gravity.Express.Application.Filter;

public class FilterProcessor : SieveProcessor, IFilterProcessor
{
    public FilterProcessor(
        IOptions<SieveOptions> options,
        ISieveCustomSortMethods sortMethods,
        ISieveCustomFilterMethods filterMethods) : base(options, sortMethods, filterMethods)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        return mapper.ApplyConfigurationsFromAssembly(typeof(FilterProcessor).Assembly);
    }
}