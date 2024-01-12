#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EqCatalogAPI.Models;

public partial class Place
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("DepartmentID")]
    public int DepartmentId { get; set; }

    [Required]
    [Column("Place")]
    [StringLength(50)]
    public string Place1 { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Places")]
    public virtual Department Department { get; set; }

    [InverseProperty("Place")]
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}