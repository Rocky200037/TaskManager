using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.TaskItems;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<TaskItem> TaskItems { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.TaskItems.AsQueryable();
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(t => t.Title.Contains(SearchTerm) ||
                                     t.Description!.Contains(SearchTerm));
        }
        TaskItems = await query.OrderBy(t => t.DueDate).ToListAsync();
    }
}
