using System.ComponentModel.DataAnnotations.Schema;

namespace NetBootcampProject.Models.ORM
{
    public class Client : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        [ForeignKey("CompanyId")]
        public Company CompanyId { get; set; }
    }
}
