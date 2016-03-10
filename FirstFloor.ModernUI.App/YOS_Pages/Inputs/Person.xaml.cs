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


namespace FirstFloor.ModernUI.App.YOS_Pages.Inputs
{
    //DataTable EXAM_EMP;
    /// <summary>
    /// Interaction logic for ControlsStylesSampleForm.xaml
    /// </summary>
    public partial class Person : UserControl
    {
        string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";

        private OracleConnection Con = new OracleConnection();
        private OracleDataAdapter Adpt;

        public Person()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            Adpt = new OracleDataAdapter("SELECT * FROM PERSON", strOraConn);
            DataTable dt = new DataTable();

            OracleCommandBuilder OraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(dt);

            DG1.ItemsSource = dt.DefaultView;

        }
    }
}
