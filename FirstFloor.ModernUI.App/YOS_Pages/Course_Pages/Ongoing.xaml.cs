using FirstFloor.ModernUI.App.Content;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course_Pages
{

	public partial class Ongoing: UserControl
    {
        public Ongoing()
        {
            InitializeComponent();
        }

		private void CommonDialog_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new ModernDialog
			{
				Title = "Common dialog",
				Content = new YOS_Content.AddCost()
			};
			dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
			
			dlg.ShowDialog();
		}
	}
}
