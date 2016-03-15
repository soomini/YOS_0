using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
{
    //DataTable EXAM_EMP;
    /// <summary>
    /// Interaction logic for ControlsStylesSampleForm.xaml
    /// </summary>
    public partial class Patners : UserControl
    {

        private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();

        private DataSet PERSON_DS = new DataSet("PERSON_DS");

        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleDataAdapter Adpt;

        public Patners()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM PERSON", strOraConn);

            DataTable PERSON_dt = PERSON_DS.Tables["PERSON_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(PERSON_DS, "PERSON_dt");

            DG1.ItemsSource = PERSON_DS.Tables["PERSON_dt"].DefaultView;
            DG1.CanUserAddRows = false;
            #endregion

            //DataRowView DRView = (DataRowView)DG1.CurrentCell.Item;
            //if (RadioGenderMale.Content == string.Format("남자"))
            //{
            //    this.RadioGenderMale.IsChecked = true;
            //}
            //else if(RadioGenderMale.Content.ToString = "여자")
            //{
            //    this.RadioGenderMale.IsChecked = true;
            //}
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {

            // select first control on the form
            Keyboard.Focus(this.TextFirstName);
        }

        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            DataTable PERSON_dt = PERSON_DS.Tables["PERSON_dt"];


            if (DG1.SelectedIndex == -1)
            {
                //1. 체크박스.Checked == true라면 변수를 1 아니라면 변수를 2로 해서 저장하고 불러오는 방법
                //2. 체크박스.Checked.ToString() 자체를 저장한 후 불러온 값이 "true"라면 남자 아니면 여자로 설정하는 방법

                PERSON_dt.Rows.Add(PERSON_dt.Rows.Count + 1, TextFirstName.Text, TextLastName.Text, TxtPhoneNumber.Text, TxtBirth.Text, "", TxtAddress.Text, TxtAddress2.Text);

                try
                {
                    Adpt.Update(PERSON_DS, "PERSON_dt");
                    MessageBox.Show("추가가 완료되었습니다.");
                }
                catch (Exception ex)
                {
                    PERSON_dt.Rows.RemoveAt(PERSON_dt.Rows.Count - 1);
                    MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
                }
            }

            string Record = "";
            foreach (DataRow R in PERSON_dt.Rows)
            {
                switch (R.RowState)
                {
                    case DataRowState.Added:
                        Record = string.Format("추가: {0}", Convert.ToString(R["NAME"]));
                        MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                        break;

                    case DataRowState.Deleted:
                        Record = string.Format("삭제: {0}", Convert.ToString(R["NAME", DataRowVersion.Original]));
                        MessageBox.Show($"데이터가 삭제되었습니다. {Record}");
                        break;

                    case DataRowState.Modified:
                        Record = string.Format("수정: {0}", Convert.ToString(R["NAME"]));
                        MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                        break;
                }
            }

            try
            {
                Adpt.Update(PERSON_DS, "PERSON_dt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.ToString());
            }


            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));

        }
    }
}
