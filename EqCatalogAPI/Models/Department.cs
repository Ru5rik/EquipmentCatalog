﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EqCatalogAPI.Models;

public partial class Department
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}