#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EqCatalogAPI.Models;

public partial class Type
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("Type")]
    [StringLength(30)]
    public string Type1 { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}