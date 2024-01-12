using EquipmentCatalog.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace EquipmentCatalog.Service
{
	public static class APIService
	{
		public static async Task<string> AuthorizationAsync(string login, string pass)
		{
			HttpClient client = new();
			try
			{
				Uri link = new($"http://localhost:5000/api/Users?login={login}&pass={pass}");

				return await client.GetStringAsync(link);
			}
			catch (Exception)
			{

			}
			return string.Empty;
		}
		public static async Task<EquipmentType[]> GetEquipmentTypesAsync()
		{
			HttpClient client = new();
			try
			{
				Uri link = new("http://localhost:5000/api/Types");

				return await client.GetFromJsonAsync<EquipmentType[]>(link);
			}
			catch (Exception)
			{

			}
			return Array.Empty<EquipmentType>();
		}
		public static async Task<Department[]> GetDepartmentAsync()
		{
			HttpClient client = new();
			try
			{
				Uri link = new("http://localhost:5000/api/Departments");

				return await client.GetFromJsonAsync<Department[]>(link);
			}
			catch (Exception)
			{

			}
			return Array.Empty<Department>();
		}
		public static async Task<Place[]> GetPlacesAsync(string department)
		{
			HttpClient client = new();
			try
			{
				Uri link = new($"http://localhost:5000/api/Departments/{department}");

				return await client.GetFromJsonAsync<Place[]>(link);
			}
			catch (Exception)
			{

			}
			return Array.Empty<Place>();
		}
		public static async Task<bool> SendEquipmentAsync(Equipment equipment)
		{
			HttpClient client = new();
			try
			{
				Uri link = new("http://localhost:5000/api/Equipments");

				JsonContent body = JsonContent.Create(equipment);

				var result = await client.PostAsJsonAsync(link, equipment);

				if (result.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{

			}
			return false;
		}
		public static async Task<bool> UpdateEquipmentAsync(Equipment equipment)
		{
			HttpClient client = new();
			try
			{
				Uri link = new($"http://localhost:5000/api/Equipments/{equipment.Id}");

				JsonContent body = JsonContent.Create(equipment);

				var result = await client.PutAsJsonAsync(link, equipment);

				if (result.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{

			}
			return false;
		}
		public static async Task<Equipment[]> GetEquipmentAsync()
		{
			HttpClient client = new();
			try
			{
				Uri link = new("http://localhost:5000/api/Equipments");


				return await client.GetFromJsonAsync<Equipment[]>(link);

			}
			catch (Exception)
			{

			}
			return Array.Empty<Equipment>();
		}
	}
}
