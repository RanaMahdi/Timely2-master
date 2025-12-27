namespace Timely.Dtos
{
    public class InvoiceDto
    {
        public DateTime InvoiceDate { get; set; }

        public double TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public int? AppointmentId { get; set; }
        public int? PaymentId { get; set; }
        public int? ClientId { get; set; }

    }
}
