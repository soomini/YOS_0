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

        private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();

        private DataSet PERSON_DS = new DataSet("PERSON_DS");

        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;

        public Person()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM PERSON", strOraConn);
            
            DataTable PERSON_dt = PERSON_DS.Tables["PERSON_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(PERSON_DS, "PERSON_dt");

            DG1.ItemsSource = PERSON_DS.Tables["PERSON_dt"].DefaultView;
            #endregion


            //public Binding Add(string propertyName, Object dataSource, string dataMember);
          
            // select first control on the form
            Keyboard.Focus(this.TxtName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable PERSON_dt = PERSON_DS.Tables["PERSON_dt"];
       
            string Record = "";
            foreach (DataRow R in PERSON_dt.Rows)
            {
                switch (R.RowState)
                {
                    case DataRowState.Added:
                        Record=string.Format("추가: {0}", Convert.ToString(R["NAME"]));
                        MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                        break;

                    case DataRowState.Deleted:
                        Record=string.Format("삭제: {0}", Convert.ToString(R["NAME", DataRowVersion.Original]));
                        MessageBox.Show($"데이터가 삭제되었습니다. {Record}");
                        break;

                    case DataRowState.Modified:
                        Record=string.Format("수정: {0}", Convert.ToString(R["NAME"]));
                        MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                        break;
                }
            }
            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));
            try
            {
                Adpt.Update(PERSON_DS, "PERSON_dt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.ToString());
            }
        }
    }
}
