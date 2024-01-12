#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EqCatalogAPI.Models;

public partial class Equipment
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Detail { get; set; }

    [Column("TypeID")]
    public int TypeId { get; set; }

    [Column("PlaceID")]
    public int PlaceId { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(250)]
    public string FilePaths { get; set; }

    [ForeignKey("PlaceId")]
    [InverseProperty("Equipment")]
    [JsonIgnore]
    public virtual Place Place { get; set; }

	[ForeignKey("StatusId")]
    [InverseProperty("Equipment")]
    [JsonIgnore]
    public virtual EquipmentStatus Status { get; set; }

	[ForeignKey("TypeId")]
    [InverseProperty("Equipment")]
    [JsonIgnore]
    public virtual Type Type { get; set; }
    public string TypeName => Type?.Type1;

}