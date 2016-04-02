using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

#region ODP.NET 관련 네임스페이스
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.IO;
#endregion


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
{
    public partial class Patners : UserControl
    {
        private string strGENDER = null;

        static StringWriter stream = new StringWriter();
        public Dispatcher UIDispatcher = Application.Current.Dispatcher;
        static DataTable PARTNERS_Dt = new DataTable();
        static DataTable PARTNERS_Dt_copy = new DataTable();
        static DataSet PARTNERS_Ds = new DataSet();        
        static DataRow[] CellClickEventROW;

        public Patners()
        {
            InitializeComponent();
            StackPannel_control_init();
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

        #region Cell click event 
        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG1.SelectedIndex != -1)
            {                              
                DataGridRow row = (DataGridRow)DG1.ItemContainerGenerator.ContainerFromIndex(DG1.SelectedIndex);
                string expression = "NAME = '" + ((TextBlock)(DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                CellClickEventROW = PARTNERS_Dt.Select(expression);

                TextFirstName.Text = (string)CellClickEventROW[0][1];
                TextLastName.Text = (string)CellClickEventROW[0][2];
                if ((string)CellClickEventROW[0][3] == "남자")
                {
                    RadioGenderMan.IsChecked = true;
        }
                else if ((string)CellClickEventROW[0][3] == "여자")
        {
                    RadioGenderWoman.IsChecked = true;
                }
                TxtPhoneNumber.Text = (string)CellClickEventROW[0][4];
                TxtBirth.SelectedDate = ((DateTime)CellClickEventROW[0][5]);
                TxtAddress.Text = (string)CellClickEventROW[0][6];
                TxtAddress2.Text = (string)CellClickEventROW[0][7];

                btn_Insert.Content = "업데이트";
            }
        }
        #endregion

        #region button click event
        private void btn_Insert_Click(object sender, RoutedEventArgs e)
            {
            #region insert 시
            if ((DG1.SelectedIndex == -1) && ((string)btn_Insert.Content == "등록"))
            {
                PARTNERS_Dt.Rows.Add(PARTNERS_Dt.Rows.Count + 1, TextFirstName.Text, TextLastName.Text, getGender(), TxtPhoneNumber.Text, TxtBirth.Text, TxtAddress.Text, TxtAddress2.Text);

                try
                {
                    PARTNERS_Ds.Reset();
                    CSampleClient.Program.SrvrConn();
                    PARTNERS_Dt_copy = PARTNERS_Dt.Copy();
                    PARTNERS_Ds.Tables.Add(PARTNERS_Dt_copy);

                    stream.Dispose();
                    stream = new StringWriter();

                    PARTNERS_Ds.WriteXml(stream, XmlWriteMode.WriteSchema);
                    CSampleClient.Program.SendMessage_insert(stream.ToString());                    
                }
                catch (Exception ex)
                {
                    PARTNERS_Dt.Rows.RemoveAt(PARTNERS_Dt.Rows.Count - 1);
                    MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
                }

                MessageBox.Show("추가가 완료되었습니다.");
                StackPannel_control_init();
                btn_Insert.Content = "등록";
                DG1.SelectedIndex = -1;
            }
            #endregion
            else if ((DG1.SelectedIndex > -1) && ((string)btn_Insert.Content == "업데이트"))
            {
                DataTable DT_search = PARTNERS_Dt.Copy();
                DT_search.Clear();
                DT_search.Rows.Add(0, "", "", getGender(), "", TxtBirth.Text, "", "");

                #region update compare
                if ( (string)CellClickEventROW[0][1] != TextFirstName.Text.Trim() )
                {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][1] = TextFirstName.Text.Trim();
                }
                if ((string)CellClickEventROW[0][2] != TextLastName.Text.Trim())
            {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][2] = TextLastName.Text.Trim();
                }
                if ((string)CellClickEventROW[0][3] != strGENDER )
                {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][3] = strGENDER;
                }
                if ((string)CellClickEventROW[0][4] != TxtPhoneNumber.Text.Trim())
                {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][4] = TxtPhoneNumber.Text.Trim();
                }
                if ( (DateTime)CellClickEventROW[0][5] != (DateTime)DT_search.Rows[0][5])
                    {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][5] = (DateTime)DT_search.Rows[0][5];
                    }
                if ((string)CellClickEventROW[0][6] != ((ComboBoxItem)TxtAddress.SelectedItem).Content.ToString())                    
                {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][6] = ((ComboBoxItem)TxtAddress.SelectedItem).Content.ToString();
                }
                if ((string)CellClickEventROW[0][7] != TxtAddress2.Text.Trim())
                {
                    PARTNERS_Dt.Rows[DG1.SelectedIndex][7] = TxtAddress2.Text.Trim();
            }

            try
            {
                    PARTNERS_Dt.AcceptChanges();
            }
                catch(Exception ex)
            {
                    MessageBox.Show("오류오류 : " + ex);                    
            }
                #endregion

                #region DB update connect
            try
            {
                    PARTNERS_Ds.Reset();
                    CSampleClient.Program.SrvrConn();
                    PARTNERS_Dt_copy = PARTNERS_Dt.Copy();
                    PARTNERS_Ds.Tables.Add(PARTNERS_Dt_copy);

                    stream.Dispose();
                    stream = new StringWriter();

                    PARTNERS_Ds.WriteXml(stream, XmlWriteMode.WriteSchema);
                                        
                    CSampleClient.Program.SendMessage_update(DG1.SelectedIndex.ToString());
                    CSampleClient.Program.SendMessage_update(stream.ToString());
            }
                catch (Exception ex)
            {
                    MessageBox.Show("에러가 발생해 수정이 되지 않았습니다\n 에러메세지: " + ex.ToString());
                }
                MessageBox.Show("수정이 완료되었습니다. ");
                #endregion
            }

        }

        private void btn_Init_Click(object sender, RoutedEventArgs e)
        {
            StackPannel_control_init();
            btn_Insert.Content = "등록";
            DG1.SelectedIndex = -1;
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)DG1.ItemContainerGenerator.ContainerFromIndex(DG1.SelectedIndex);
                string expression = "NAME = '" + ((TextBlock)(DG1.Columns[1].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                DataRow[] DeleteRow = YOS.CAccessDB.getds().Tables[0].Select(expression);

                YOS.CAccessDB.getds().Tables[0].Rows[DG1.SelectedIndex].Delete();

                stream.Dispose();
                stream = new StringWriter();

                CSampleClient.Program.SrvrConn();
                YOS.CAccessDB.getds().WriteXml(stream, XmlWriteMode.WriteSchema);
                CSampleClient.Program.SendMessage_delete(((TextBlock)(DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text);
                CSampleClient.Program.SendMessage_delete(stream.ToString());

                StackPannel_control_init();
                DG1.SelectedIndex = -1;
                btn_Insert.Content = "등록";
                MessageBox.Show("강사 삭제 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }
        #endregion

        private void DG1_LayoutUpdated(object sender, EventArgs e)
        {
            UIDispatcher.Invoke(new Action(() => PARTNERS_Dt = YOS.CAccessDB.getdt()));
            UIDispatcher.Invoke(new Action(() => DG1.ItemsSource = PARTNERS_Dt.DefaultView));//수신
        }

        private void DG1_Loaded(object sender, RoutedEventArgs e)
        {
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PARTNERS")));
        }
    }
}