using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#region ODP.NET 관련 네임스페이스
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
#endregion

namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{
    public partial class Patners : UserControl
    {
		private DataSet LECTURER_DS = new DataSet("LECTURER_DS");

        private string strGENDER = "";

        public Patners()
        {
            InitializeComponent();

            DataTable LECTURER_dt = LECTURER_DS.Tables["LECTURER_dt"];

            DG1.ItemsSource = LECTURER_DS.Tables["LECTURER_dt"].DefaultView;
            DG1.CanUserAddRows = false;

        }
        public void StackPannel_control_init()
        {
            TextFirstName.Text = null;
            TextLastName.Text = null;
            RadioGenderMan.IsChecked = false;
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
        #endregion


        void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this.TextFirstName);
        }
        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURER_dt = LECTURER_DS.Tables["LECTURER_dt"];


            if (DG1.SelectedIndex == -1)
            {
				LECTURER_dt.Rows.Add(LECTURER_dt.Rows.Count + 1, TextFirstName.Text, TextLastName.Text, TxtPhoneNumber.Text, TxtBirth.Text, "", TxtAddress.Text, TxtAddress2.Text);

                try
                {
                //    Adpt.Update(LECTURER_DS, "PERSON_dt");
                    MessageBox.Show("추가가 완료되었습니다.");
                }
                catch (Exception ex)
                {
					LECTURER_dt.Rows.RemoveAt(LECTURER_dt.Rows.Count - 1);
                    MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
                }
            }

            string Record = "";
            foreach (DataRow R in LECTURER_dt.Rows)
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
          //      Adpt.Update(LECTURER_DS, "LECTURER_dt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.ToString());
            }
        }

        private void btn_Init_Click(object sender, RoutedEventArgs e)
        {
            StackPannel_control_init();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)DG1.ItemContainerGenerator.ContainerFromIndex(DG1.SelectedIndex);
                string expression = "NAME = '" + ((TextBlock)(DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                DataRow[] DeleteRow = LECTURER_DS.Tables["LECTURER_dt"].Select(expression);

				LECTURER_DS.Tables["LECTURER_dt"].Rows[DG1.SelectedIndex].Delete();

             //   Adpt.Update(LECTURER_DS, "LECTURER_dt");

                MessageBox.Show("삭제 성공");
                StackPannel_control_init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }
    }
}
