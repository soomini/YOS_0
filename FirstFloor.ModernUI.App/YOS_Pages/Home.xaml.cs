using FirstFloor.ModernUI.App.Content;
using FirstFloor.ModernUI.App.YOS_Pages.Course;
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

namespace FirstFloor.ModernUI.App.YOS_Pages
{
    /// <summary>
    /// Interaction logic for Introduction.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

		private void SetCustomMenu(object sender, RoutedEventArgs e)
		{
			MenuItem selectedMenuItem = (MenuItem)sender;
			MenuItem parentMenuItem = (MenuItem)selectedMenuItem.Parent;

			Button btn = ((ContextMenu)parentMenuItem.Parent).PlacementTarget as Button;
			btn.Content = selectedMenuItem.Header;
			btn.Command = NavigationCommands.GoToPage;

			switch ((string)btn.Content)
			{
				case "진행 중 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Ongoing.xaml";
					break;
				case "전체 완료 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Complete_Pages/Whole.xaml";
					break;
				case "파트너별 완료 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Complete_Pages/Patners.xaml";
					break;
				case "역할별 완료 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Complete_Pages/Role.xaml";
					break;
				case "강좌별 수수료 현황":
					btn.CommandParameter = "/YOS_Pages/Status/CostStatus_Pages/Course.xaml";
					break;
				case "파트너별 수수료 현황":
					btn.CommandParameter = "/YOS_Pages/Status/CostStatus_Pages/Patners.xaml";
					break;
				case "파트너":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference.xaml";
					break;
				case "항목별 요율":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference_Pages/ItemRate.xaml";
					break;
				case "강좌 분류":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference_Pages/CourseCategory.xaml";
					break;
				case "교구 단가":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference_Pages/TeachingMaterialPrice.xaml";
					break;

			}
			
		}

		private void btnEdit_Checked(object sender, RoutedEventArgs e)
		{
			//rtbEdit.IsReadOnly = false;
			//rtbEdit.IsEnabled = true;
		}

		private void btnEdit_Unchecked(object sender, RoutedEventArgs e)
		{
			//rtbEdit.IsReadOnly = true;
			//rtbEdit.IsEnabled = false;
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new ModernDialog
			{
				Title = "공지사항",
				Content = new RichTextBox()
			};
			dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
			dlg.ShowDialog();

			//this.dialogResult.Text = dlg.DialogResult.HasValue ? dlg.DialogResult.ToString() : "<null>";
			//this.dialogMessageBoxResult.Text = dlg.MessageBoxResult.ToString();

		}
	}
}
