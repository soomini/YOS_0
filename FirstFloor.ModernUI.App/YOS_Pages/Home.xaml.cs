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

		private MenuItem CreateCourseMenu(string header)
		{
			var item = new MenuItem { Header = header };
			item.Items.Add("진행 중 강좌");
			item.Items.Add(new Separator());
			item.Items.Add("전체 진행 완료 강좌");
			item.Items.Add("파트너별 진행 완료 강좌");
			item.Items.Add("역할별 진행 완료 강좌");
			return item;
		}

		private MenuItem CreateStatusMenu(string header)
		{
			var item = new MenuItem { Header = header };
			item.Items.Add("강좌별 수수료 현황");
			item.Items.Add("파트너별 수수료 현황");
			return item;
		}

		private MenuItem CreateReferenceMenu(string header)
		{
			var item = new MenuItem { Header = header };
			item.Items.Add("Patners");
			item.Items.Add("항목별 요율");
			item.Items.Add("강좌 분류");
			item.Items.Add("교구 단가");
			return item;
		}


		private void btn1_Click(object sender, RoutedEventArgs e)
		{

			var contextMenu = new ContextMenu();

			contextMenu.Items.Add(CreateCourseMenu("Course"));
			contextMenu.Items.Add(CreateStatusMenu("Status"));
			contextMenu.Items.Add(CreateReferenceMenu("Reference"));

			contextMenu.IsOpen = true;
		}
	}
}
