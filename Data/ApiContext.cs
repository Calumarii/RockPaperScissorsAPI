using Microsoft.EntityFrameworkCore;
using RockPaperScissorsAPI.Models;

namespace RockPaperScissorsAPI.Data
{
    public class ApiContext : DbContext
    {

        public DbSet<Game> Games { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
    }
}
