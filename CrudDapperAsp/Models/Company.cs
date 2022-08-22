using System.ComponentModel.DataAnnotations;

namespace CrudDapperAsp.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string Country { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
       
    }

    public class CompanyForCreationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
       
        public List<Employee> Employees { get; set; }
    }
    public class CompanyForUpdateDto : CompanyForCreationDto
    {
        public int Id { get; set; }
    }
}
