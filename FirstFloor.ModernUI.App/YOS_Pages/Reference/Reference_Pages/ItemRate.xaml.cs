using System.Windows.Controls;
using System.Windows.Threading;

#region ODP.NET @ CONNECTIONSTRING namespace 추가
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Windows;
using System.IO;
#endregion

namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{

    public partial class ItemRate : UserControl
    {
        #region 비연결기반 객체들 준비
        static StringWriter stream = new StringWriter();
        public Dispatcher UIDispatcher = Application.Current.Dispatcher;
        static DataTable ITEMRATE_Dt = new DataTable();
        static DataTable ITEMRATE_Dt_copy = new DataTable();
        static DataSet ITEMRATE_Ds = new DataSet();

		#endregion

		static int switchint = 0;

        public ItemRate()
        {
            InitializeComponent();
        }

        private void Purpose_Checked(object sender, RoutedEventArgs e)
        {
            ItemRate_DG1.Columns[2].Visibility = Visibility.Visible;
            ItemRate_DG1.Columns[3].Visibility = Visibility.Hidden;
        }

        private void Staff_Checked(object sender, RoutedEventArgs e)
        {
            ItemRate_DG1.Columns[2].Visibility = Visibility.Hidden;
            ItemRate_DG1.Columns[3].Visibility = Visibility.Visible;
        }

        private void Whole_Checked(object sender, RoutedEventArgs e)
        {
            if (switchint > 0)
            {
                if (ItemRate_DG1.Columns[2].Visibility == Visibility.Hidden)
                {
                    ItemRate_DG1.Columns[2].Visibility = Visibility.Visible;
                }
                if (ItemRate_DG1.Columns[3].Visibility == Visibility.Hidden)
                {
                    ItemRate_DG1.Columns[3].Visibility = Visibility.Visible;
                }
            }
            switchint++;
        }

        private void ItemRate_DG1_Loaded(object sender, RoutedEventArgs e)
        {
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("ITEMRATE")));          
        }

        private void ItemRate_DG1_LayoutUpdated(object sender, EventArgs e)
        {
            UIDispatcher.Invoke(new Action(() => ITEMRATE_Dt = YOS.CAccessDB.getdt()));
            UIDispatcher.Invoke(new Action(() => ItemRate_DG1.ItemsSource = ITEMRATE_Dt.DefaultView));//수신
        }
    }
}
