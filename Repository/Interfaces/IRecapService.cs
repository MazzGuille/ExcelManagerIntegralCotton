using ExcelManagerIntegralCotton.Models;

namespace ExcelManagerIntegralCotton.Repository.Interfaces
{
    public interface IRecapService
    {
        Task<List<RECAP>> GetRecapListAsync();
        //void AmountUHML(List<decimal> list, List<RECAP> recap, int pos1, int pos2, List<int> total);
    }
}
