using EquipmentCatalog.Models;
using EquipmentCatalog.Service;
using EquipmentCatalog.Utils;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EquipmentCatalog.Pages
{
	/// <summary>
	/// Interaction logic for EditorPage.xaml
	/// </summary>
	/// 
	[ObservableObject]
	public partial class EditorPage : Page
	{
		public ObservableCollection<Tuple<string, byte[]>> Files { get; set; }
		[ObservableProperty]
		public Equipment _equipment;
		//public Equipment Equipment { get; set; }
		private bool _isNew;
		private IEnumerable<Department> _departments;

		public EditorPage(Equipment equipment)
		{
			InitializeComponent();
			DataContext = this;

			GetData(equipment);
		}

		private async Task LoadTypes()
		{
			var data = await APIService.GetEquipmentTypesAsync();
			TypeBox.ItemsSource = data;
		}
		private async Task LoadDepartment()
		{
			_departments = await APIService.GetDepartmentAsync();
			DepartmentBox.ItemsSource = _departments;

			foreach (var item in _departments)
			{
				var places = await APIService.GetPlacesAsync(item.Name);

				item.Places = places;
			}
		}
		private async void GetData(Equipment equipment)
		{
			LoadingBar.Visibility = Visibility.Visible;
			Body.IsEnabled = false;
			Files = new();

			await LoadTypes();
			await LoadDepartment();

			if (equipment != null)
			{
				Equipment = equipment;

				StatusBox.SelectedIndex = Equipment.StatusId - 1;
				TypeBox.SelectedIndex = equipment.TypeId - 1;
				DeleteButton.Visibility = Visibility.Visible;

				DepartmentBox.SelectedItem = _departments.Where(x => x.Places.Select(x => x.Id).Contains(Equipment.PlaceId)).First();
				PlaceBox.SelectedItem = PlaceBox.ItemsSource.Cast<Place>().First(x => x.Id == Equipment.PlaceId);

				var paths = JsonSerializer.Deserialize<string[]>(Equipment.FilePaths);

				foreach (var path in paths)
				{
					Files.Add(Tuple.Create<string, byte[]>(path, null));
				}
			}
			else
			{
				_isNew = true;
				Equipment = new();
				StatusBox.SelectedIndex = 0;
			}

			LoadingBar.Visibility = Visibility.Collapsed;
			Body.IsEnabled = true;
		}

		private void BackClick(object sender, RoutedEventArgs e)
		{
			PageUtils.MainFrame.GoBack();
		}

		private async void SubmitButtonClick(object sender, RoutedEventArgs e)
		{
			LoadingBar.Visibility = Visibility.Visible;
			Body.IsEnabled = false;

			// validation
			if (StatusBox.SelectedItem == null || TypeBox.SelectedItem == null || PlaceBox.SelectedItem == null || string.IsNullOrEmpty(Equipment.Name))
			{
				MessageBox.Show("Необходимо заполнить все обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			Equipment.StatusId = StatusBox.SelectedIndex + 1;
			Equipment.TypeId = ((EquipmentType)TypeBox.SelectedItem).Id;
			Equipment.PlaceId = ((Place)PlaceBox.SelectedItem).Id;
			if (Files.Count > 0)
				Equipment.FilePaths = JsonSerializer.Serialize(Files.Select(x => x.Item1));

			bool result;
			if (_isNew)
			{
				result = await APIService.SendEquipmentAsync(Equipment);
			}
			else
			{
				result = await APIService.UpdateEquipmentAsync(Equipment);
			}
			// load files to server

			if (!result)
			{
				MessageBox.Show("Не удалось добавить оборудование!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				PageUtils.MainFrame.GoBack();
				//((MainPage)PageUtils.MainFrame.DataContext).LoadEquipment();
			}

			LoadingBar.Visibility = Visibility.Collapsed;
			Body.IsEnabled = true;
		}

		private void LoadFileClick(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.Filter = "Text files(*.txt)|*.txt|Image files|*.jpeg,*.jpg";

			if (!dialog.ShowDialog().HasValue)
				return;

			foreach (var paths in dialog.FileNames)
			{
				Tuple<string, byte[]> file = Tuple.Create(dialog.SafeFileName, File.ReadAllBytes(paths));

				Files.Add(file);
			}
		}

		private void RemoveFileClick(object sender, RoutedEventArgs e)
		{
			Files.Remove(FilesListView.SelectedValue as Tuple<string, byte[]>);
		}

		private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (FilesListView.SelectedValue == null)
			{
				RemoveFileButton.IsEnabled = false;
			}
			else
			{
				RemoveFileButton.IsEnabled = true;
			}
		}
	}
}
