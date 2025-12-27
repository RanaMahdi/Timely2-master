namespace Timely.Dtos
{
    public class ServicesDto
    {
        public string Name { get; set; }

        public double Price { get; set; }
        public bool IsActive { get; set; }
        public int? TypeServiceId { get; set; }
        public int DepartmentId { get; set; }
    }
}
