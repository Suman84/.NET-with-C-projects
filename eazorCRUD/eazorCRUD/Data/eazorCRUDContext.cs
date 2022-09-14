using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eazorCRUD.Model;

namespace eazorCRUD.Data
{
    public class eazorCRUDContext : DbContext
    {
        public eazorCRUDContext (DbContextOptions<eazorCRUDContext> options)
            : base(options)
        {
        }

        public DbSet<eazorCRUD.Model.Student> Student { get; set; }
    }
}
