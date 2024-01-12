#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EqCatalogAPI.Models;

public partial class User
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string Login { get; set; }

    [Required]
    [StringLength(30)]
    public string Password { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Users")]
    public virtual UserStatus Status { get; set; }
}