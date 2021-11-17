using DataAccess.DataContexts.ApplicationDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContexts.ApplicationDbContext
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
