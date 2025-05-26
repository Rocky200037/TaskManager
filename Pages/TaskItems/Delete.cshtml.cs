using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.TaskItems;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public TaskItem TaskItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        TaskItem = await _context.TaskItems.FirstOrDefaultAsync(m => m.Id == id);
        if (TaskItem == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.TaskItems.FindAsync(id);
        if (item != null)
        {
            _context.TaskItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}
