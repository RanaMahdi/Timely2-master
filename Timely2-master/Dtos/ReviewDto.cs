namespace Timely.Dtos
{
    public class ReviewDto
    {
        public string? Comments { get; set; }
        public double Rating { get; set; }    
        public DateTime ReviewDate { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }


    }
}
