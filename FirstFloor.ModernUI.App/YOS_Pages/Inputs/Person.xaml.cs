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
        private DataSet Lecturer_DS = new DataSet("Lecturer_DS");

        private OracleCommandBuilder oraBuilder;

        private OracleDataAdapter oraDA;

        private string connStr = "User Id=scott;Password=tiger;Data Source=ORCL";
        #endregion

        private string strGENDER="";

        public Person()
        {
            InitializeComponent();
            
            this.Loaded += OnLoaded;

            #region 데이터 가져오기 및 DataGrid에 추가
            oraDA = new OracleDataAdapter("SELECT * FROM LECTURER", connStr);

            oraBuilder = new OracleCommandBuilder(oraDA);

            oraDA.Fill(Lecturer_DS, "LECTURER");

            DG1.ItemsSource = Lecturer_DS.Tables["LECTURER"].DefaultView;
            #endregion
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            // select first control on the form
            Keyboard.Focus(this.TextFirstName);            
        }

        private void btn_INSERT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region set nuwRow value
                DataRow newInsertRow = Lecturer_DS.Tables["LECTURER"].NewRow();
                // 시퀀스 사용으로 수정 필요
                newInsertRow["LECTURERNO"] = 4;
                newInsertRow["LECTURERNAME"] = TextFirstName.Text.Trim();
                newInsertRow["JOB"] = TextLastName.Text.Trim();
                newInsertRow["GENDER"] = strGENDER;                
                newInsertRow["PHONENUMBER"] = TextCity.Text.Trim();
                newInsertRow["BIRTHDATE"] = DateBirth.SelectedDate.Value.ToString();              
                newInsertRow["CITY"] = ((ComboBoxItem)ComboState.SelectedItem).Content.ToString();
                newInsertRow["DETAILADDRESS"] = TextAddress.Text.Trim();

                Lecturer_DS.Tables["LECTURER"].Rows.Add(newInsertRow);
                #endregion
                oraDA.Update(Lecturer_DS, "LECTURER");
                MessageBox.Show("추가 성공");
            }
            catch(Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }

        #region Radio value get
        private void RadioGendeWan_Checked(object sender, RoutedEventArgs e)
        {
            strGENDER = (string)(sender as RadioButton).Content;
        }

        private void RadioGendeWoman_Checked(object sender, RoutedEventArgs e)
        {
            strGENDER = (string)(sender as RadioButton).Content;
        }
        #endregion        
    }
}
