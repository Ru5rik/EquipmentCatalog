using EquipmentCatalog.Models;
using EquipmentCatalog.Service;
using EquipmentCatalog.Utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EquipmentCatalog.Pages
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public ObservableCollection<Equipment> Equipments { get; set; }
		private IEnumerable<Equipment> _equipments;
		public MainPage(string Name)
		{
			InitializeComponent();

			Equipments = new ObservableCollection<Equipment>();

			DataContext = this;

			LoadEquipment();
		}

		private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
		{
			if (Search.Text == string.Empty)
			{
				PlaceHolder.Visibility = Visibility.Visible;
			}
			else
			{
				PlaceHolder.Visibility = Visibility.Collapsed;
			}
		}
		private void ExitClick(object sender, RoutedEventArgs e)
		{
			PageUtils.MainFrame.Navigate(new AuthPage());
		}

		private void SearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (string.IsNullOrEmpty(Search.Text))
				{
					foreach (var item in _equipments)
					{
						Equipments.Add(item);
					}
					return;
				}

				Equipments.Clear();

				var data = _equipments.Where(x => x.Name.Contains(Search.Text) || x.Detail.Contains(Search.Text) || x.Type.Contains(Search.Text) || x.Department.Contains(Search.Text) || x.Place.Contains(Search.Text));

				foreach (var item in data)
				{
					Equipments.Add(item);
				}
			}
		}

		private void AddClick(object sender, RoutedEventArgs e)
		{
			PageUtils.MainFrame.Navigate(new EditorPage(null, LoadEquipment));
		}
		
		public async void LoadEquipment()
		{
			_equipments = await APIService.GetEquipmentAsync();
			Equipments.Clear();
			foreach (var item in _equipments)
			{
				Equipments.Add(item);
			}
		}

		private void EditClick(object sender, RoutedEventArgs e)
		{
			var data = ((Button)sender).DataContext as Equipment;

			PageUtils.MainFrame.Navigate(new EditorPage(data, LoadEquipment));
		}

		// search
	}
}
