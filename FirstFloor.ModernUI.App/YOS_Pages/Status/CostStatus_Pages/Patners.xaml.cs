using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Pages.Status.CostStatus_Pages
{
    public partial class Patners: UserControl
    {
        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        //private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet STATUS_DS = new DataSet("STATUS_DS");
        private DataSet STATUS_DS1 = new DataSet("STATUS_DS");
        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleCommandBuilder oraBuilder1;
        private OracleDataAdapter Adpt;
        private OracleDataAdapter Adpt1;
        private Boolean LC = false;

        //private DataRelation RelPatner;

        public Patners()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            //SELECT * FROM STATUS
            Adpt = new OracleDataAdapter("SELECT p.NAME, s.JOB, s.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID", strOraConn);
            Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, s.JOB FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID ORDER BY NAME", strOraConn);

            DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];
            DataTable STATUS_dt1 = STATUS_DS1.Tables["STATUS_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);
            oraBuilder1 = new OracleCommandBuilder(Adpt1);

            Adpt.Fill(STATUS_DS, "STATUS_dt");
            DG_ST_P.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            Adpt1.Fill(STATUS_DS1, "STATUS_dt");
            CB_ST_P.ItemsSource = STATUS_DS1.Tables["STATUS_dt"].DefaultView;
            DG_ST_P.CanUserAddRows = false;

            #endregion

            //RelPatner = new DataRelation("Course", STATUS_DS.Tables["STATUS_dt"].Columns["NAME"], STATUS_DS.Tables["STATUS_dt1"].Columns["NAME"]);
            //STATUS_DS.Relations.Add(RelPatner);
        }

        private void CheckWhole_Checked(object sender, RoutedEventArgs e)
        {
            Adpt = new OracleDataAdapter("SELECT p.NAME, s.JOB, s.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID", strOraConn);
            DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];

            STATUS_dt.Clear();

            Adpt.Fill(STATUS_DS, "STATUS_dt");
            DG_ST_P.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;

            if (CB_ST_P_T.Text != "")
                CB_ST_P.Text = "";
            else
                if (DG_ST_P.Items.Count == 0)
                CB_ST_P_T_TextChanged(null, null);

            if (CheckWhole.IsChecked != true)
                CheckWhole.IsChecked = true;
        }

        private void CheckWhole_Unchecked(object sender, RoutedEventArgs e)
        {
            DG_ST_P.ItemsSource = null;
        }

        private void CB_ST_P_T_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(LC == true)
            {
                LC = false;
                return;
            }
            CheckWhole.IsChecked = false;


            string ps = CB_ST_P_T.Text;

            string dynamicquery = "";

            if (ps != "")
            {
                dynamicquery += "p.NAME=('" + ps + "')";
            }

            if (dynamicquery != "")
                dynamicquery = " where s.NAME=p.ID and " + dynamicquery;

            Adpt = new OracleDataAdapter("SELECT p.NAME, s.JOB, s.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p" + dynamicquery, strOraConn);
            DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];

            DG_ST_P.ItemsSource = null;
            //CB_ST_P.ItemsSource = null;

            STATUS_dt.Clear();

            //CB_ST_P.Text = null;

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(STATUS_DS, "STATUS_dt");
            DG_ST_P.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            //CB_ST_P.ItemsSource = STATUS_DS.Tables["STATUS_dt1"].DefaultView;
            DG_ST_P.CanUserAddRows = false;
        }
    
        private void DG_ST_P_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];
            //CB_ST_P.Text = STATUS_dt.Rows[DG_ST_P.SelectedIndex].ItemArray[0].ToString();
        }

        private void CB_ST_P_T2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CB_ST_P_T2.Text != "")
            {
                LC = true;
                CB_ST_P.Text = CB_ST_P_T2.Text;
                
            }
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate.SelectedDate <= StartDate.SelectedDate)
            {
                MessageBox.Show("시작일보다 빠름! 시작일 이후로 다시 선택해 주십시오.");
                EndDate.SelectedDate = null;
            }
        }

        private void searchDate()
        {
            string sd = String.Format("{0:yy-MM-dd}", StartDate.SelectedDate);
            string ed = String.Format("{0:yy-MM-dd}", EndDate.SelectedDate);

            string dynamicquery = "";

            if (sd != "" && ed != "")
            {

            }

            if (sd != "")
            {
                dynamicquery += "CLOSEDATE >= to_date('" + sd + "', 'yy-mm-dd')";
            }

            if (ed != "")
            {
                if (dynamicquery != "")
                    dynamicquery += " and ";
                else
                    dynamicquery += " ";

                dynamicquery += "STARTDATE <= to_date('" + ed + "', 'yy-mm-dd')";
            }

            //if (dynamicquery != "")
            //    dynamicquery = " where " + dynamicquery;

            //Adpt = new OracleDataAdapter("SELECT * FROM LECTURE" + dynamicquery, strOraConn);
            //DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            //DG_ST_P.ItemsSource = null;
            //DG_ST_P.ItemsSource = null;

            //LECTURE_dt.Clear();

            //oraBuilder = new OracleCommandBuilder(Adpt);

            //Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            //DG_ST_P.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            //DG_ST_P.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            //DG_ST_P.CanUserAddRows = false;
        }
    }
}
