namespace BookSeriesApi.Models
{
    public class BookItemDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? SeriesName { get; set; }
        public string? MainCouple { get; set; }
        public bool IsRead { get; set; }
        public int SpiceRating { get; set; }
    }
}