namespace Timely.Dtos
{
    public class PaymentDto
    {
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int AppointmentId { get; set; }
        public int ClientId { get; set; }
    }
}
