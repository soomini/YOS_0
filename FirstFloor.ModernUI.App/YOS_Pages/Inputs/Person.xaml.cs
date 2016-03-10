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

#region ODP.NET @ CONNECTIONSTRING namespace 추가
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Configuration;
#endregion

namespace FirstFloor.ModernUI.App.YOS_Pages.Inputs
{
    /// <summary>
    /// Interaction logic for ControlsStylesSampleForm.xaml
    /// </summary>
    /// 
    public partial class Person : UserControl
    {
        #region 비연결기반 객체들 준비
        private DataSet PERSON_DS = new DataSet("PERSON_DS");

        private OracleCommandBuilder oraBuilder;

        private OracleDataAdapter oraDA;

        private string connStr = "User Id=dba_soo;Password=tnalsl;Data Source=MYORACLE";
        #endregion

        public Person()
        {
            InitializeComponent();
            
            this.Loaded += OnLoaded;           
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            #region 데이터 가져오기 및 DataGrid에 추가
            oraDA = new OracleDataAdapter("SELECT * FROM PERSON", connStr);

            oraBuilder = new OracleCommandBuilder(oraDA);

            oraDA.Fill(PERSON_DS, "PERSON");

            DG1.ItemsSource = PERSON_DS.Tables["PERSON"].DefaultView;
            #endregion

            // select first control on the form
            Keyboard.Focus(this.TextFirstName);            
        }
    }
}
