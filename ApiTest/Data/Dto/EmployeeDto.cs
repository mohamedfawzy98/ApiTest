namespace ApiTest.Data.Dto
{
    public class EmployeeDto
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
    }
}
