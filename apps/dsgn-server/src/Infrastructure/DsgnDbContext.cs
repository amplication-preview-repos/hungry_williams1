using Dsgn.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dsgn.Infrastructure;

public class DsgnDbContext : IdentityDbContext<IdentityUser>
{
    public DsgnDbContext(DbContextOptions<DsgnDbContext> options)
        : base(options) { }

    public DbSet<CourseDbModel> Courses { get; set; }

    public DbSet<LessonDbModel> Lessons { get; set; }

    public DbSet<DiscussionDbModel> Discussions { get; set; }

    public DbSet<EnrollmentDbModel> Enrollments { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
