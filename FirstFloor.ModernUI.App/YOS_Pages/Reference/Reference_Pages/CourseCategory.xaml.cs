using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{
    public partial class CourseCategory : UserControl
    {

        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        //private OracleConnection Con = new OracleConnection();

        private DataSet CATEGORY_DS = new DataSet("CATEGORY_DS");

        private OracleCommandBuilder oraBuilder1; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleCommandBuilder oraBuilder2;
        private OracleCommandBuilder oraBuilder3;
        private OracleCommandBuilder oraBuilder4;
        private OracleDataAdapter Adpt1;
        private OracleDataAdapter Adpt2;
        private OracleDataAdapter Adpt3;
        private OracleDataAdapter Adpt4;

        static int Swap;

        public CourseCategory()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt1 = new OracleDataAdapter("SELECT * FROM PurposeCATEGORY", strOraConn);
            Adpt2 = new OracleDataAdapter("SELECT * FROM InstitutionCATEGORY", strOraConn);
            Adpt3 = new OracleDataAdapter("SELECT * FROM TargetCATEGORY", strOraConn);
            Adpt4 = new OracleDataAdapter("SELECT * FROM SubjectCATEGORY", strOraConn);

            DataTable PurposeCATEGORY_dt = CATEGORY_DS.Tables["PurposeCATEGORY_dt"];
            DataTable InstitutionCATEGORY_dt = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"];
            DataTable TargetCATEGORY_dt = CATEGORY_DS.Tables["TargetCATEGORY_dt"];
            DataTable SubjectCATEGORY_dt = CATEGORY_DS.Tables["SubjectCATEGORY_dt"];

            oraBuilder1 = new OracleCommandBuilder(Adpt1);
            oraBuilder2 = new OracleCommandBuilder(Adpt2);
            oraBuilder3 = new OracleCommandBuilder(Adpt3);
            oraBuilder4 = new OracleCommandBuilder(Adpt4);

            Adpt1.Fill(CATEGORY_DS, "PurposeCATEGORY_dt");
            Adpt2.Fill(CATEGORY_DS, "InstitutionCATEGORY_dt");
            Adpt3.Fill(CATEGORY_DS, "TargetCATEGORY_dt");
            Adpt4.Fill(CATEGORY_DS, "SubjectCATEGORY_dt");

            DGCat1.ItemsSource = CATEGORY_DS.Tables["PurposeCATEGORY_dt"].DefaultView;
            DGCat2.ItemsSource = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"].DefaultView;
            DGCat3.ItemsSource = CATEGORY_DS.Tables["TargetCATEGORY_dt"].DefaultView;
            DGCat4.ItemsSource = CATEGORY_DS.Tables["SubjectCATEGORY_dt"].DefaultView;

            #endregion
        }

        private void Purpose_Checked(object sender, RoutedEventArgs e)
        {
            DGCat1.Visibility = Visibility.Visible;
            DGCat2.Visibility = Visibility.Hidden;
            DGCat3.Visibility = Visibility.Hidden;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Institution_Checked(object sender, RoutedEventArgs e)
        {
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Visible;
            DGCat3.Visibility = Visibility.Hidden;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Target_Checked(object sender, RoutedEventArgs e)
        {
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Collapsed;
            DGCat3.Visibility = Visibility.Visible;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Subject_Checked(object sender, RoutedEventArgs e)
        {
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Collapsed;
            DGCat3.Visibility = Visibility.Collapsed;
            DGCat4.Visibility = Visibility.Visible;
        }

        private void Whole_Checked(object sender, RoutedEventArgs e)
        {
            if (Swap == 1)
            {
                DGCat1.Visibility = Visibility.Visible;
                DGCat2.Visibility = Visibility.Visible;
                DGCat3.Visibility = Visibility.Visible;
                DGCat4.Visibility = Visibility.Visible;
            }
            else
            {
                Swap = 1;
            }
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(btn_Insert.Content) == "업데이트")
            {
                try
                {
                    DataTable PurposeCATEGORY_dt = CATEGORY_DS.Tables["PurposeCATEGORY_dt"];
                    btn_Insert.Content = "추가/수정";
                    //string Record = "";
                    //foreach (DataRow R in PurposeCATEGORY_dt.Rows)
                    //{
                    //switch (R.RowState)
                    //{
                    //    case DataRowState.Added:
                    //        Record = string.Format("추가: {0}", Convert.ToString(R["NAME"]));
                    //        MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                    //        break;

                    //    case DataRowState.Deleted:
                    //        Record = string.Format("삭제: {0}", Convert.ToString(R["NAME", DataRowVersion.Original]));
                    //        MessageBox.Show($"데이터가 삭제되었습니다. {Record}");
                    //        break;
                    //}
                    //    foreach (DataColumn C in PurposeCATEGORY_dt.Columns)
                    //    {
                    //        if (!R[C, DataRowVersion.Original].Equals(R[C, DataRowVersion.Current]))
                    //        {
                    //            Record = string.Format("수정: {0}", Convert.ToString(R["PURPOSE"]));
                    //            MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                    //        }
                    //    }
                    //} 
                    Adpt1.Update(CATEGORY_DS, "PurposeCATEGORY_dt");
                    Adpt2.Update(CATEGORY_DS, "InstitutionCATEGORY_dt");
                    Adpt3.Update(CATEGORY_DS, "TargetCATEGORY_dt");
                    Adpt4.Update(CATEGORY_DS, "SubjectCATEGORY_dt");

                    DGCat1.IsReadOnly = true;
                    DGCat2.IsReadOnly = true;
                    DGCat3.IsReadOnly = true;
                    DGCat4.IsReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("에러 발생: " + ex.ToString());
                }
            }
            else
            {
                btn_Insert.Content = "업데이트";

                DGCat1.IsReadOnly = false;
                DGCat2.IsReadOnly = false;
                DGCat3.IsReadOnly = false;
                DGCat4.IsReadOnly = false;
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CATEGORY_DS.Tables["PurposeCATEGORY_dt"].Rows[DGCat1.SelectedIndex].Delete();
                CATEGORY_DS.Tables["InstitutionCATEGORY_dt"].Rows[DGCat2.SelectedIndex].Delete();
                CATEGORY_DS.Tables["TargetCATEGORY_dt"].Rows[DGCat3.SelectedIndex].Delete();
                CATEGORY_DS.Tables["SubjectCATEGORY_dt"].Rows[DGCat4.SelectedIndex].Delete();

                Adpt1.Update(CATEGORY_DS, "PurposeCATEGORY_dt");
                Adpt2.Update(CATEGORY_DS, "InstitutionCATEGORY_dt");
                Adpt3.Update(CATEGORY_DS, "TargetCATEGORY_dt");
                Adpt4.Update(CATEGORY_DS, "SubjectCATEGORY_dt");

                MessageBox.Show("삭제 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }
    }
}
