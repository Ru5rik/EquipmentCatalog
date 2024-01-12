using EquipmentCatalog.Service;
using EquipmentCatalog.Utils;
using System.Windows;
using System.Windows.Controls;

namespace EquipmentCatalog.Pages
{
	/// <summary>
	/// Interaction logic for AuthPage.xaml
	/// </summary>
	public partial class AuthPage : Page
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public AuthPage()
		{
			InitializeComponent();

			DataContext = this;
		}

		private async void ButtonClick(object sender, RoutedEventArgs e)
		{
			SubmitButton.IsEnabled = false;
			LoadingBar.Visibility = Visibility.Visible;

			if (!string.IsNullOrEmpty(Login) || !string.IsNullOrEmpty(Password))
			{
				string result = await APIService.AuthorizationAsync(Login, Password);

				if (!string.IsNullOrEmpty(result))
				{
					PageUtils.MainFrame.Navigate(new MainPage(result));
				}
				else
				{
					MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show("Необходимо заполнить поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			SubmitButton.IsEnabled = true;
			LoadingBar.Visibility = Visibility.Collapsed;
		}
	}
}
