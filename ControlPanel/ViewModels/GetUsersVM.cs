using ControlPanel.Entities;
using X.PagedList;

namespace ControlPanel.ViewModels
{
    public class GetUsersVM
    {
        public int TotalCount { get; set; }
        public int Count { get; set; }
        public List<User> users { get; set; }

        public int PageCount { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Search { get; set; }
        public string? OrderOn { get; set; }
        public string? OrderBy { get; set; }
    }
}
