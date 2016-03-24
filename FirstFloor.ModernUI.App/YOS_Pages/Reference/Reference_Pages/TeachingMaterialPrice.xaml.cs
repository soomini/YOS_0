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
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("EDUCATION_SUPPORT_TOOL")));
        }

        #region 추가 button click event
        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_Insert.Content == "확인")
            {
                try
                {
                    CSampleClient.Program.SrvrConn();
                    EDUCATIONTOOL_Dt_copy = EDUCATIONTOOL_Dt.Copy();

                    EDUCATIONTOOL_Ds.Tables.Add(EDUCATIONTOOL_Dt_copy);
                    EDUCATIONTOOL_Ds.WriteXml(stream, XmlWriteMode.WriteSchema);
                    CSampleClient.Program.SendMessage_update(stream.ToString());

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

                CSampleClient.Program.SrvrConn();
                YOS.CAccessDB.getds().WriteXml(stream, XmlWriteMode.WriteSchema);
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
            //UIDispatcher.Invoke(new Action(() => EDUCATIONTOOL_Dt = YOS.CAccessDB.getdt()));
            //UIDispatcher.Invoke(new Action(() => EDUTOOL_DG1.ItemsSource = EDUCATIONTOOL_Dt.DefaultView));//수신
        }
    }
} 