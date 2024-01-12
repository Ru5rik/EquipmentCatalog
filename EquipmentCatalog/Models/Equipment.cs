

using CommunityToolkit.Mvvm.ComponentModel;

namespace EquipmentCatalog.Models;

public partial class Equipment : ObservableObject
{
	public int Id { get; set; }
	[ObservableProperty]

	public string _name;
	[ObservableProperty]

	public string _detail;
	public string Type { get; set; }
	public string Department { get; set; }
	public string Place { get; set; }
	public int TypeId { get; set; }
	public int PlaceId { get; set; }
	public int StatusId { get; set; }
	public string FilePaths { get; set; }
	public string TypeName { get; set; }
}