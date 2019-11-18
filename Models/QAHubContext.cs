using Microsoft.EntityFrameworkCore;
namespace QAHub.Models
{
    public class QAHubContext : DbContext
    {
        public QAHubContext(DbContextOptions<QAHubContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets {get;set;}
    }
}