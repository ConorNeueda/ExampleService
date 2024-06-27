using Microsoft.EntityFrameworkCore;

namespace ExampleService.Persistence.Context
{
    public partial class MagentaDbContext(DbContextOptions<MagentaDbContext> options) : DbContext(options)
    {
    }
}
