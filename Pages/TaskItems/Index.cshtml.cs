using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<IdentityUser> _userManager;

    public IndexModel(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<TaskItem> TaskItems { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        var query = _context.TaskItems
            .Where(t => t.UserId == user!.Id);

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(t =>
                t.Title.Contains(SearchTerm) ||
                (t.Description != null &&
                 t.Description.Contains(SearchTerm)));
        }

        TaskItems = await query
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }
}