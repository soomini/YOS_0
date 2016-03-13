using FirstFloor.ModernUI.App.Content;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course_Pages
{
    /// <summary>
    /// Interaction logic for LayoutTab.xaml
    /// </summary>
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
				//Content = new LoremIpsum()
			};
			//dlg.Content = new LoremIpsum();
			//dlg.Resources = 
			dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
			dlg.ShowDialog();

			//this.dialogResult.Text = dlg.DialogResult.HasValue ? dlg.DialogResult.ToString() : "<null>";
			//this.dialogMessageBoxResult.Text = dlg.MessageBoxResult.ToString();
		}
	}
}
