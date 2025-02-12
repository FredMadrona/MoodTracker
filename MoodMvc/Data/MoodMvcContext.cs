using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoodMvc.Models;

namespace MoodMvc.Data
{
    public class MoodMvcContext : DbContext
    {
        public MoodMvcContext (DbContextOptions<MoodMvcContext> options)
            : base(options)
        {
        }

        public DbSet<MoodMvc.Models.MoodEntry> MoodEntry { get; set; } = default!;
    }
}
