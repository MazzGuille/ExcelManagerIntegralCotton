using ExcelManagerIntegralCotton.Models;
using ExcelManagerIntegralCotton.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExcelManagerIntegralCotton.Repository
{
    public class RecapService : IRecapService
    {
        private readonly IntegralCottonDbContext _context;
        List<RECAP> recapList = new();

        public RecapService(IntegralCottonDbContext context)
        {
            _context = context;
        }

        public async Task<List<RECAP>> GetRecapListAsync()
        {
            var hviList = await _context.ExcelData.ToListAsync();

            foreach (var item in hviList)
            {
                recapList.Add(new RECAP
                {
                    Id = item.Id,
                    Uhml = Convert.ToDecimal(item.Uhml),
                    Ui = Convert.ToDecimal(item.Ui),
                    Strength = Convert.ToDecimal(item.Strength),
                    Sfi = Convert.ToDecimal(item.Sfi),
                    Mic = Convert.ToDecimal(item.Mic),
                    Colorgrade = item.Colorgrade,
                    TrashId = Convert.ToDecimal(item.Ui),
                });
            }

            return recapList;
        }     

    }
}
