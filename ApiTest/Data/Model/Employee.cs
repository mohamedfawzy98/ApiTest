namespace ApiTest.Data.Model
{
    public class Employee : BaseModel
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
