using Gravity.Express.Application.Common.Models;

namespace Gravity.Express.Application.Mapping;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }

    public static PaginatedList<TDestination> PaginatedListAsync<TDestination>(
        this IEnumerable<TDestination> list, int pageNumber, int pageSize)
    {
        return PaginatedList<TDestination>.Create(list, pageNumber, pageSize);
    }
}
