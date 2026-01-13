using Exam_asp.Models;

namespace Exam_asp.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<SecretMessage> SecretMessages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}