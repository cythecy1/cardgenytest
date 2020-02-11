using GLvNext.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLvNext.Data
{
    public class GLDbContext : DbContext
    {
        public GLDbContext(DbContextOptions<GLDbContext> options) : base(options)
        {

        }
        public DbSet<Offer> Offers { get; set; }
    }
}
