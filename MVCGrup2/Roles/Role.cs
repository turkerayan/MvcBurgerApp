using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Roles
{
	public class Role
	{
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
