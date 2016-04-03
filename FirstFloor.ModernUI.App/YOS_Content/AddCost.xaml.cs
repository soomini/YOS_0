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
        private DataSet FEE_DS = new DataSet("FEE_dt");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;

        public AddCost()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM FEE", strOraConn);

            DataTable FEE_dt = FEE_DS.Tables["FEE_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(FEE_DS, "FEE_dt");
            tbx_FoodCosts.DataContext = FEE_DS.Tables["FEE_dt"];
            //DGLEC.ItemsSource = PARTNERS_DS.Tables["PARTNERS_dt"].DefaultView;
            //DGLEC.CanUserAddRows = false;

            #endregion
            //foreach (DataRow R in PARTNERS_dt.Rows)
            //{
            //    //bool rowdeleted = false;
            ////    switch (R.RowState)
            ////    {
            ////        MessageBox.Show($"R.ItemArray[6]");
            ////}

            //}
        }
    }
}
