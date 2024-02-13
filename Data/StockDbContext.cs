using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aråstock.Models;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions<StockDbContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Item { get; set; } = default!;

    public DbSet<Category> Category { get; set; } = default!;

    public DbSet<Unit> Unit { get; set; } = default!;

    public DbSet<Information> Information { get; set; } = default!;


}
