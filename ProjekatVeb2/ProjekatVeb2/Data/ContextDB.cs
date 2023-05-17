using Microsoft.EntityFrameworkCore;

namespace ProjekatVeb2.Data
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }
    }
}
