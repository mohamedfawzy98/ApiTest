namespace ApiTest.Data.Model
{
    public class Department : BaseModel
    {
        public string Name { get; set; } = null!;
        public string? MangerName { get; set; }
        public ICollection<Employee>? Employees { get; set; } = new HashSet<Employee>();
    }
}
