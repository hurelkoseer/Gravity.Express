namespace Gravity.Express.Application.Common.Models;

public class PagingModel
{
    public int PageNumber { get; set; } = Constants.DefaultPageNumber;

    public int PageSize { get; set; } = Constants.DefaultPageSize;
}