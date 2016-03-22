using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Globalization;


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
{
    public partial class Patners : UserControl
    {

        private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet PERSON_DS = new DataSet("PERSON_DS");
        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleDataAdapter Adpt;

        private string strGENDER = "";

        public Patners()
        {
            InitializeComponent();
            
            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM PERSON", strOraConn);

            DataTable PERSON_dt = PERSON_DS.Tables["PERSON_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(PERSON_DS, "PERSON_dt");
            DG1.ItemsSource = PERSON_DS.Tables["PERSON_dt"].DefaultView;
            DG1.CanUserAddRows = false;

            #endregion
        }
        public void StackPannel_control_init()
        {
            TextFirstName.Text = null;
            TextLastName.Text = null;
            RadioGenderMan.IsChecked = true;
            RadioGenderWoman.IsChecked = false;
            TxtPhoneNumber.Text = null;
            TxtBirth.SelectedDate = null;
            TxtAddress.Text = null;
            TxtAddress2.Text = null;
        }
        #region Radio value get
        private void RadioGenderMan_Checked(object sender, RoutedEventArgs e)
        {
            strGENDER = (string)(sender as RadioButton).Content;
        }
        private void RadioGenderWoman_Checked(object sender, RoutedEventArgs e)
        {
            strGENDER = (string)(sender as RadioButton).Content;
        }

        private string getGender()
        {
            if (RadioGenderMan.IsChecked == true)
            {
                return "남자";
            }
            else
            {
                return "여자";
            }
        }
        #endregion


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

                PERSON_dt.Rows.Add(PERSON_dt.Rows.Count + 1, TextFirstName.Text, TextLastName.Text, TxtPhoneNumber.Text, TxtBirth.Text, getGender(), TxtAddress.Text, TxtAddress2.Text);

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

                    //case DataRowState.Modified:
                    //    Record = string.Format("수정: {0}", Convert.ToString(R["NAME"]));
                    //    MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                    //    break;
                }

                foreach(DataColumn C in PERSON_dt.Columns)
                {
                    if(!R[C, DataRowVersion.Original].Equals(R[C, DataRowVersion.Current]))
                    {
                        Record = string.Format("수정: {0}", Convert.ToString(R["NAME"]));
                        MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                    }
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

            try
            {
                PERSON_dt.AcceptChanges();
            }
            catch
            {

            }

            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));

        }

        private void btn_Init_Click(object sender, RoutedEventArgs e)
        {
            DG1.SelectedIndex = -1;
            StackPannel_control_init();
            Btn_Register.Content = "등록";
        }


        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //DataGridRow row = (DataGridRow)DG1.ItemContainerGenerator.ContainerFromIndex(DG1.SelectedIndex);
                //string expression = "NAME = '" + ((TextBlock)(DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                //DataRow[] DeleteRow = PERSON_DS.Tables["PERSON_dt"].Select(expression);

                PERSON_DS.Tables["PERSON_dt"].Rows[DG1.SelectedIndex].Delete();

                Adpt.Update(PERSON_DS, "PERSON_dt");

                MessageBox.Show("삭제 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }

        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Btn_Register.Content = "업데이트";
        }
    }
}