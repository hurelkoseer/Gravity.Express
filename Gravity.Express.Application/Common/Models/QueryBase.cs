namespace Gravity.Express.Application.Common.Models
{
    public abstract record QueryBase
    {
        public int PageNumber { get; set; }

        private int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize > MaxPageSize ? MaxPageSize : _pageSize;
            protected init => _pageSize = value;
        }

        protected QueryBase(PagingModel pagingModel)
        {
            this.PageNumber = pagingModel.PageNumber;
            this.PageSize = pagingModel.PageSize;
        }

        protected QueryBase()
        {
            this.PageNumber = Constants.DefaultPageNumber;
            this.PageSize = Constants.DefaultPageSize;
        }

        protected virtual int MaxPageSize => Constants.DefaultMaxPageSize;
    }
}