using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class ClientRepo : MainRepository<Client> ,IClientRepo
    {
        private readonly ApplicationDbContext _context;
        public ClientRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetClientswithNationality()
        {
            return _context.Clients.Include(n => n.Nationality);
        }
    }
}
