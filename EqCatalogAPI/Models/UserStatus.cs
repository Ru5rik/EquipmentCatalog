#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EqCatalogAPI.Models;

public partial class UserStatus
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}