namespace ExcelManagerIntegralCotton.Models
{
    public class RECAP
    {
        public int Id { get; set; }

        public decimal Uhml { get; set; }

        public decimal Ui { get; set; }

        public decimal Strength { get; set; }

        public decimal Sfi { get; set; }

        public decimal Mic { get; set; }

        public string Colorgrade { get; set; } = null!;

        public decimal TrashId { get; set; }
    }
}
