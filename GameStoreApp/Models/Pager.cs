namespace GameStoreApp.Models
{
    /// <summary>
    /// Represents a pager used for pagination.
    /// </summary>
    public class Pager
    {
        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        public int TotalItems { get; private set; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Gets the first page number in the range of displayed pages.
        /// </summary>
        public int StartPage { get; private set; }

        /// <summary>
        /// Gets the last page number in the range of displayed pages.
        /// </summary>
        public int EndPage { get; private set; }

        /// <summary>
        /// Initialises a new instance of the Pager class.
        /// </summary>
        public Pager()
        {
            // Default constructor
        }

        // <summary>
        /// Initialises a new instance of the Pager class with the specified parameters.
        /// </summary>
        /// <param name="totalItems">The total number of items.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        public Pager(int totalItems, int page, int pageSize = 10)
        {
            // Calculate total number of pages
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            // Set current page number
            int currentPage = page;

            // Calculate start and end pages for pagination links
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            // Adjust start and end pages if necessary to ensure they are within valid range
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            // Set property values
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

    }
}
