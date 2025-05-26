using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    // Inherit from IdentityDbContext so EF knows how to create
    // AspNetUsers, AspNetRoles, etc., alongside your TaskItems table.
    public class ApplicationDbContext
        : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Your TaskItems table
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    }
}
