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

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Content
{
    /// <summary>
    /// Interaction logic for LoremIpsum.xaml
    /// </summary>
    public partial class AddCost : UserControl
    {

        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        private DataSet PARTNERS_DS = new DataSet("PARTNERS_DS");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;

        public AddCost()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM PARTNERS", strOraConn);

            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(PARTNERS_DS, "PARTNERS_dt");
            //DGLEC.ItemsSource = PARTNERS_DS.Tables["PARTNERS_dt"].DefaultView;
            //DGLEC.CanUserAddRows = false;

            #endregion
        }
    }
}
