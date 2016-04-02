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
        private DataSet SPARTNERS_DS = new DataSet("SPARTNERS_DS");
        private DataSet SPARTNERS_DS1 = new DataSet("SPARTNERS_DS");
        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleCommandBuilder oraBuilder1;
        private OracleDataAdapter Adpt;
        private OracleDataAdapter Adpt1;
        private Boolean LC = false;

        //private DataRelation RelPatner;

        public Patners()
        {
            InitializeComponent();

            wUpdate();
        }

        private void wUpdate()
        {
            try
            {
                DG_ST_P.ItemsSource = null;
                //CB_ST_P.ItemsSource = null;

                DataTable d1 = SPARTNERS_DS.Tables["SPARTNERS_dt"];
                DataTable d2 = SPARTNERS_DS1.Tables["SPARTNERS_dt1"];

                d1.Clear();
                d2.Clear();
            }
            catch
            {

            }
            #region 데이터 가져오기 및 DataGrid에 추가

            //SELECT * FROM STATUS
            Adpt = new OracleDataAdapter("SELECT p.NAME, p.JOB, l.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p, LECTURE l WHERE s.NAME=p.ID AND l.LECTUREID=s.LECTURENAME", strOraConn);
            Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, s.JOB FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID ORDER BY NAME", strOraConn);

            DataTable SPARTNERS_dt = SPARTNERS_DS.Tables["SPARTNERS_dt"];
            DataTable SPARTNERS_dt1 = SPARTNERS_DS1.Tables["SPARTNERS_dt1"];

            oraBuilder = new OracleCommandBuilder(Adpt);
            oraBuilder1 = new OracleCommandBuilder(Adpt1);

            Adpt.Fill(SPARTNERS_DS, "SPARTNERS_dt");
            DG_ST_P.ItemsSource = SPARTNERS_DS.Tables["SPARTNERS_dt"].DefaultView;
            Adpt1.Fill(SPARTNERS_DS1, "SPARTNERS_dt1");
            CB_ST_P.ItemsSource = SPARTNERS_DS1.Tables["SPARTNERS_dt1"].DefaultView;
            DG_ST_P.CanUserAddRows = false;

            #endregion

            //RelPatner = new DataRelation("Course", STATUS_DS.Tables["STATUS_dt"].Columns["NAME"], STATUS_DS.Tables["STATUS_dt1"].Columns["NAME"]);
            //STATUS_DS.Relations.Add(RelPatner);
        }

        private void CheckWhole_Checked(object sender, RoutedEventArgs e)
        {
            Adpt = new OracleDataAdapter("SELECT p.NAME, p.JOB, l.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p, LECTURE l WHERE s.NAME=p.ID AND l.LECTUREID=s.LECTURENAME", strOraConn);
            DataTable SPARTNERS_dt = SPARTNERS_DS.Tables["SPARTNERS_dt"];

            SPARTNERS_dt.Clear();

            Adpt.Fill(SPARTNERS_DS, "SPARTNERS_dt");
            DG_ST_P.ItemsSource = SPARTNERS_DS.Tables["SPARTNERS_dt"].DefaultView;

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
                dynamicquery = " where s.NAME=p.ID and l.LECTUREID=s.LECTURENAME and " + dynamicquery;

            Adpt = new OracleDataAdapter("SELECT p.NAME, p.JOB, l.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p, LECTURE l" + dynamicquery, strOraConn);
            DataTable SPARTNERS_dt = SPARTNERS_DS.Tables["SPARTNERS_dt"];

            DG_ST_P.ItemsSource = null;
            //CB_ST_P.ItemsSource = null;

            SPARTNERS_DS.Clear();

            //CB_ST_P.Text = null;

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(SPARTNERS_DS, "SPARTNERS_dt");
            DG_ST_P.ItemsSource = SPARTNERS_DS.Tables["SPARTNERS_dt"].DefaultView;
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

            if (dynamicquery != "")
                dynamicquery = " where s.NAME=p.ID and l.LECTUREID=s.LECTURENAME and " + dynamicquery;

            Adpt = new OracleDataAdapter("SELECT * FROM STATUS s, PARTNERS p, LECTURE l" + dynamicquery, strOraConn);
            DataTable SPARTNERS_dt = SPARTNERS_DS.Tables["SPARTNERS_dt"];

            DG_ST_P.ItemsSource = null;
            DG_ST_P.ItemsSource = null;

            SPARTNERS_dt.Clear();

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(SPARTNERS_DS, "SPARTNERS_dt");
            DG_ST_P.ItemsSource = SPARTNERS_DS.Tables["SPARTNERS_dt"].DefaultView;
            DG_ST_P.ItemsSource = SPARTNERS_DS.Tables["SPARTNERS_dt"].DefaultView;
            DG_ST_P.CanUserAddRows = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wUpdate();
        }
    }
}
