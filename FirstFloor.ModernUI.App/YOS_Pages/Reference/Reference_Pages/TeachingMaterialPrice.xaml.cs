using System.Windows.Controls;
using System.Windows.Threading;

#region ODP.NET @ CONNECTIONSTRING namespace 추가
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Windows;
using System.Data;
using System;
using System.IO;
#endregion

namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{
    //되는 새로운 UI (3/23)
    public partial class TeachingMaterialPrice : UserControl
    {
        #region 비연결기반 객체들 준비
        static StringWriter stream = new StringWriter();
        public Dispatcher UIDispatcher = Application.Current.Dispatcher;
        static DataTable EDUCATIONTOOL_Dt = new DataTable();
        static DataTable EDUCATIONTOOL_Dt_copy = new DataTable();
        static DataSet EDUCATIONTOOL_Ds = new DataSet();
		#endregion

		public TeachingMaterialPrice()
        {
            InitializeComponent();
        }

        #region 추가 button click event
        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_Insert.Content == "확인")
            {
                try
                {
                    EDUCATIONTOOL_Ds.Reset();   
                    CSampleClient.Program.SrvrConn();
                    EDUCATIONTOOL_Dt_copy = EDUCATIONTOOL_Dt.Copy();

                    EDUCATIONTOOL_Ds.Tables.Add(EDUCATIONTOOL_Dt_copy);

                    stream.Dispose();
                    stream = new StringWriter();

                    EDUCATIONTOOL_Ds.WriteXml(stream, XmlWriteMode.WriteSchema);
                    CSampleClient.Program.SendMessage_insert(stream.ToString());

                    MessageBox.Show("교구 추가 성공");
                    btn_Insert.Content = "추가";
                    EDUTOOL_DG1.IsReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류 : " + ex.ToString());
                }
            }
            else
            {
                btn_Insert.Content = "확인";
                EDUTOOL_DG1.IsReadOnly = false;
            }
        }
        #endregion

        #region 삭제 button click event
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)EDUTOOL_DG1.ItemContainerGenerator.ContainerFromIndex(EDUTOOL_DG1.SelectedIndex);
                string expression = "EDUCATIONTOOLNAME = '" + ((TextBlock)(EDUTOOL_DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                DataRow[] DeleteRow = YOS.CAccessDB.getds().Tables[0].Select(expression);

                YOS.CAccessDB.getds().Tables[0].Rows[EDUTOOL_DG1.SelectedIndex].Delete();

                stream.Dispose();
                stream = new StringWriter();

                CSampleClient.Program.SrvrConn();
                YOS.CAccessDB.getds().WriteXml(stream, XmlWriteMode.WriteSchema);
                CSampleClient.Program.SendMessage_delete(((TextBlock)(EDUTOOL_DG1.Columns[1].GetCellContent(row).Parent as DataGridCell).Content).Text);
                CSampleClient.Program.SendMessage_delete(stream.ToString());             

                MessageBox.Show("교구 삭제 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }
        #endregion

        private void EDUTOOL_DG1_LayoutUpdated(object sender, EventArgs e)
        {                       
            UIDispatcher.Invoke(new Action(() => EDUCATIONTOOL_Dt = YOS.CAccessDB.getdt()));
            UIDispatcher.Invoke(new Action(() => EDUTOOL_DG1.ItemsSource = EDUCATIONTOOL_Dt.DefaultView));//수신
        }

        private void EDUTOOL_DG1_Loaded(object sender, RoutedEventArgs e)
        {
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("EDUCATION_SUPPORT_TOOL")));
        }
    }
}

////////////////////////////////////////
//using System;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Threading;

//#region ODP.NET 관련 네임스페이스
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Types;
//using System.Data;
//using System.IO;
//#endregion

//namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
//{
//    public partial class Patners : UserControl
//    {
//        private string strGENDER = null;

//        static StringWriter stream = new StringWriter();
//        public Dispatcher UIDispatcher = Application.Current.Dispatcher;
//        static DataTable PARTNERS_Dt = new DataTable();
//        static DataTable PARTNERS_Dt_copy = new DataTable();
//        static DataSet PARTNERS_Ds = new DataSet();
//        static DataRow updateRow_1;
//        static DataRow updateRow_2;

//        public Patners()
//        {
//            InitializeComponent();

//            //DataTable PARTNERS_dt = LECTURER_DS.Tables["PARTNERS_Dt"];
//            //DG1.CanUserAddRows = false;
//        }

//        void OnLoaded(object sender, RoutedEventArgs e)
//        {
//            Keyboard.Focus(this.TextFirstName);
//        }

//        public void StackPannel_control_init()
//        {
//            TextFirstName.Text = null;
//            TextLastName.Text = null;
//            RadioGenderMan.IsChecked = false;
//            RadioGenderWoman.IsChecked = false;
//            TxtPhoneNumber.Text = null;
//            TxtBirth.SelectedDate = null;
//            TxtAddress.Text = null;
//            TxtAddress2.Text = null;
//        }

//        private void btn_Init_Click(object sender, RoutedEventArgs e)
//        {
//            DG1.SelectedIndex = -1;
//            StackPannel_control_init();
//            Btn_Register.Content = "등록";
//        }

//        #region Radio value get
//        private void RadioGenderMan_Checked(object sender, RoutedEventArgs e)
//        {
//            strGENDER = (string)(sender as RadioButton).Content;
//        }
//        private void RadioGenderWoman_Checked(object sender, RoutedEventArgs e)
//        {
//            strGENDER = (string)(sender as RadioButton).Content;
//        }

//        private string getGender()
//        {
//            if (RadioGenderMan.IsChecked == true)
//            {
//                return "남자";
//            }
//            else
//            {
//                return "여자";
//            }
//        }
//        #endregion

//        private void Btn_Register_Click(object sender, RoutedEventArgs e)
//        {
//            if ((DG1.SelectedIndex == -1) && ((string)Btn_Register.Content == "등록"))
//            {
//                PARTNERS_Dt.Rows.Add(PARTNERS_Dt.Rows.Count + 1, TextFirstName.Text, TextLastName.Text, getGender(), TxtPhoneNumber.Text, TxtBirth.Text, TxtAddress.Text, TxtAddress2.Text);

//                try
//                {
//                    PARTNERS_Ds.Reset();
//                    CSampleClient.Program.SrvrConn();
//                    PARTNERS_Dt_copy = PARTNERS_Dt.Copy();
//                    PARTNERS_Ds.Tables.Add(PARTNERS_Dt_copy);

//                    stream.Dispose();
//                    stream = new StringWriter();

//                    PARTNERS_Ds.WriteXml(stream, XmlWriteMode.WriteSchema);
//                    CSampleClient.Program.SendMessage_insert(stream.ToString());

//                    MessageBox.Show("추가가 완료되었습니다.");
//                }
//                catch (Exception ex)
//                {
//                    PARTNERS_Dt.Rows.RemoveAt(PARTNERS_Dt.Rows.Count - 1);
//                    MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
//                }
//            }
//            else if ((DG1.SelectedIndex > -1) && ((string)Btn_Register.Content == "수정"))
//            {
//                updateRow_2 = PARTNERS_Dt.Rows[DG1.SelectedIndex];
//                foreach (DataColumn C in PARTNERS_Dt.Columns)
//                {
//                    if (!updateRow_1[C, DataRowVersion.Proposed].Equals(updateRow_2[C, DataRowVersion.Current]))
//                    {
//                        try
//                        {
//                            PARTNERS_Ds.Reset();
//                            CSampleClient.Program.SrvrConn();
//                            PARTNERS_Dt_copy = PARTNERS_Dt.Copy();
//                            PARTNERS_Ds.Tables.Add(PARTNERS_Dt_copy);

//                            stream.Dispose();
//                            stream = new StringWriter();

//                            PARTNERS_Ds.WriteXml(stream, XmlWriteMode.WriteSchema);

//                            CSampleClient.Program.SendMessage_update(stream.ToString());
//                            CSampleClient.Program.SendMessage_update(stream.ToString());
//                            CSampleClient.Program.SendMessage_update(stream.ToString());
//                            //   MessageBox.Show("수정이 완료되었습니다. " + a);
//                        }
//                        catch (Exception ex)
//                        {
//                            MessageBox.Show("에러가 발생해 수정이 되지 않았습니다\n 에러메세지: " + ex.ToString());
//                        }
//                    }
//                }
//            }
//        }

//        private void btn_Delete_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                DataGridRow row = (DataGridRow)DG1.ItemContainerGenerator.ContainerFromIndex(DG1.SelectedIndex);
//                string expression = "NAME = '" + ((TextBlock)(DG1.Columns[1].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
//                DataRow[] DeleteRow = YOS.CAccessDB.getds().Tables[0].Select(expression);

//                YOS.CAccessDB.getds().Tables[0].Rows[DG1.SelectedIndex].Delete();

//                stream.Dispose();
//                stream = new StringWriter();

//                CSampleClient.Program.SrvrConn();
//                YOS.CAccessDB.getds().WriteXml(stream, XmlWriteMode.WriteSchema);
//                CSampleClient.Program.SendMessage_delete(((TextBlock)(DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text);
//                CSampleClient.Program.SendMessage_delete(stream.ToString());

//                MessageBox.Show("교구 삭제 성공");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("오류 : " + ex.ToString());
//            }
//        }

//        private void DG1_LayoutUpdated(object sender, EventArgs e)
//        {
//            UIDispatcher.Invoke(new Action(() => PARTNERS_Dt = YOS.CAccessDB.getdt()));
//            UIDispatcher.Invoke(new Action(() => DG1.ItemsSource = PARTNERS_Dt.DefaultView));//수신
//        }

//        private void DG1_Loaded(object sender, RoutedEventArgs e)
//        {
//            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
//            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PARTNERS")));
//        }

//        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            if (DG1.SelectedIndex != -1)
//            {
//                Btn_Register.Content = "수정";
//                updateRow_1 = PARTNERS_Dt.Rows[DG1.SelectedIndex];
//            }
//        }
//    }
//}
