using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.IO;


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{
    public partial class CourseCategory : UserControl
    {
        static StringWriter stream = new StringWriter();
        public Dispatcher UIDispatcher = Application.Current.Dispatcher;

        private DataSet CATEGORY_DS = new DataSet("CATEGORY_DS");

        static DataTable PurposeCATEGORY_Dt = new DataTable();
        static DataTable PurposeCATEGORY_Dt_copy = new DataTable();
        static DataSet PurposeCATEGORY_Ds = new DataSet();

        static DataTable InstitutionCATEGORY_Dt = new DataTable();
        static DataTable InstitutionCATEGORY_Dt_copy = new DataTable();
        static DataSet InstitutionCATEGORY_Ds = new DataSet();

        static DataTable TargetCATEGORY_Dt = new DataTable();
        static DataTable TargetCATEGORY_Dt_copy = new DataTable();
        static DataSet TargetCATEGORY_Ds = new DataSet();

        static DataTable SubjectCATEGORY_Dt = new DataTable();
        static DataTable SubjectCATEGORY_Dt_copy = new DataTable();
        static DataSet SubjectCATEGORY_Ds = new DataSet();

        static string getmsave = null;
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
            DGCat2.Visibility = Visibility.Visible;
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat3.Visibility = Visibility.Hidden;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Target_Checked(object sender, RoutedEventArgs e)
        {
            DGCat3.Visibility = Visibility.Visible;
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Collapsed;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Subject_Checked(object sender, RoutedEventArgs e)
        {
            DGCat4.Visibility = Visibility.Visible;
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Collapsed;
            DGCat3.Visibility = Visibility.Collapsed;
        }

        private void Whole_Checked(object sender, RoutedEventArgs e)
        {
            getmsave = CSampleClient.Program.getmsave();

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
        static bool aaa = true;
        static bool bbb = true;

        private void DGCat1_LayoutUpdated(object sender, EventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => DGCat1.ItemsSource = PurposeCATEGORY_Dt_copy.DefaultView));//수신
            DGCat1.ItemsSource = PurposeCATEGORY_Dt_copy.DefaultView;
        }
        private void DGCat2_LayoutUpdated(object sender, EventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => DGCat2.ItemsSource = InstitutionCATEGORY_Dt_copy.DefaultView));//수신
        }
        private void DGCat3_LayoutUpdated(object sender, EventArgs e)
        {
         //   UIDispatcher.Invoke(new Action(() => TargetCATEGORY_Dt = YOS.CAccessDB.getdt()));
          //  UIDispatcher.Invoke(new Action(() => DGCat3.ItemsSource = TargetCATEGORY_Dt.DefaultView));//수신
        }

        private void DGCat4_LayoutUpdated(object sender, EventArgs e)
        {
          //  UIDispatcher.Invoke(new Action(() => SubjectCATEGORY_Dt = YOS.CAccessDB.getdt()));
           // UIDispatcher.Invoke(new Action(() => DGCat4.ItemsSource = SubjectCATEGORY_Dt.DefaultView));//수신
        }


        private void DGCat1_Loaded(object sender, RoutedEventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PURPOSECATEGORY")));
            //if (aaa)
            //{
            //    UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt = YOS.CAccessDB.getdt()));
            //    UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt_copy = PurposeCATEGORY_Dt.Clone()));
            //    aaa = false;
            //}      

            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PURPOSECATEGORY")));
            UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt = YOS.CAccessDB.getdt()));
            UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt_copy = PurposeCATEGORY_Dt.Clone()));
            UIDispatcher.Invoke(new Action(() => DGCat1.ItemsSource = PurposeCATEGORY_Dt_copy.DefaultView));//수신    
            
        }

        private void DGCat2_Loaded(object sender, RoutedEventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("INSTITUTIONCATEGORY")));
            //UIDispatcher.Invoke(new Action(() => InstitutionCATEGORY_Dt = YOS.CAccessDB.getdt()));
            //InstitutionCATEGORY_Dt_copy = InstitutionCATEGORY_Dt.Copy();
           
        }

        private void DGCat3_Loaded(object sender, RoutedEventArgs e)
        {
          //  UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
          //  UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("TARGETCATEGORY")));
        }

        private void DGCat4_Loaded(object sender, RoutedEventArgs e)
        {
           // UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("SUBJECTCATEGORY")));
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(btn_Insert.Content) == "업데이트")
            {
                try
                {
                    DataTable PurposeCATEGORY_dt = CATEGORY_DS.Tables["PurposeCATEGORY_dt"];
                    btn_Insert.Content = "추가/수정";
                    btn_Delete.Content = "삭제";
                    string Record = "";

                    //if (DGCat1.SelectedIndex == PurposeCATEGORY_dt.Rows.Count-1)
                    //{

                    //    PurposeCATEGORY_dt.Rows.Add(PurposeCATEGORY_dt.Rows.Count + 1,DGCat1.);

                    //    try
                    //    {
                    //        Adpt1.Update(CATEGORY_DS, "PurposeCATEGORY_dt");
                    //        MessageBox.Show("추가가 완료되었습니다.");
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        PurposeCATEGORY_dt.Rows.RemoveAt(PurposeCATEGORY_dt.Rows.Count - 1);
                    //        MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
                    //    }
                    //}
                        foreach (DataRow R in PurposeCATEGORY_dt.Rows)
                        {
                            switch (R.RowState)
                            {
                                case DataRowState.Added:
                                    Record = string.Format("추가: {0}", Convert.ToString(R["PURPOSE"]));
                                    MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                                    break;

                                case DataRowState.Deleted:
                                    Record = string.Format("삭제: {0}", Convert.ToString(R["PURPOSE", DataRowVersion.Original]));
                                    MessageBox.Show($"데이터가 삭제되었습니다. {Record}");
                                    break;
                            }
                            foreach (DataColumn C in PurposeCATEGORY_dt.Columns)
                            {
                                if (!R[C, DataRowVersion.Original].Equals(R[C, DataRowVersion.Current]))
                                {
                                    Record = string.Format("수정: {0}", Convert.ToString(R["PURPOSE"]));
                                    MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                                }
                            }
                        }
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
                btn_Delete.Content = "취소";

                DGCat1.IsReadOnly = false;
                DGCat2.IsReadOnly = false;
                DGCat3.IsReadOnly = false;
                DGCat4.IsReadOnly = false;
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            //if (Convert.ToString(btn_Delete.Content) == "취소")
            //{
            //    btn_Insert.Content = "추가/수정";
            //    btn_Delete.Content = "삭제";


            //    DGCat1.ItemsSource = null;
            //    DGCat2.ItemsSource = null;
            //    DGCat3.ItemsSource = null;
            //    DGCat4.ItemsSource = null;

            //    DGCat1.ItemsSource = CATEGORY_DS.Tables["PurposeCATEGORY_dt"].DefaultView;
            //    DGCat2.ItemsSource = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"].DefaultView;
            //    DGCat3.ItemsSource = CATEGORY_DS.Tables["TargetCATEGORY_dt"].DefaultView;
            //    DGCat4.ItemsSource = CATEGORY_DS.Tables["SubjectCATEGORY_dt"].DefaultView;
         //}
         //   else {
                try
                {
                //if (DGCat1) ;
                //{ }
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
        //}
    }
}
