using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurfsUpExam.Models;

namespace SurfsUpExam.Data
{
    public class SurfsUpExamContext : DbContext
    {
        public SurfsUpExamContext (DbContextOptions<SurfsUpExamContext> options)
            : base(options)
        {
        }

        public DbSet<SurfsUpExam.Models.SurfsBoard> SurfsBoard { get; set; } = default!;
    }
}
