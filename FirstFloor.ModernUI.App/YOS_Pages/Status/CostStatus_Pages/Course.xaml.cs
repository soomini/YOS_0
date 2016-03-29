using System.Windows.Controls;
using System;
using System.Windows;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Pages.Status.CostStatus_Pages
{

	public partial class Course: UserControl
    {
        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        //private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet LECTURE_DS = new DataSet("LECTURE_DS");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;

        public Course()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);

            DataTable PERSON_dt = LECTURE_DS.Tables["LECTURE_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DG_ST_C.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            CB_ST_C.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DG_ST_C.CanUserAddRows = false;

            #endregion
        }

        private void CB_ST_C_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DG_ST_C.SelectedIndex = CB_ST_C.SelectedIndex;
        }

        private void Total_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                long tmoney = 0;
                long.TryParse(Total.Text, out tmoney);

                long money = (long)(tmoney * 0.65);
                long comm = (long)(tmoney * 0.10);
                long bexp = (long)(tmoney * 0.10);
                long mfee = (long)(tmoney * 0.10);
                long cpro = (long)(tmoney * 0.10);

                Money.Text = money.ToString();
                Commission.Text = comm.ToString();
                BusinessExpenses.Text = bexp.ToString();
                MembershipFees.Text = mfee.ToString();
                CorporationProfit.Text = cpro.ToString();
            }
            catch
            {

            }
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();

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
                dynamicquery = " where " + dynamicquery;

            Adpt = new OracleDataAdapter("SELECT * FROM LECTURE" + dynamicquery, strOraConn);
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            DG_ST_C.ItemsSource = null;
            CB_ST_C.ItemsSource = null;

            LECTURE_dt.Clear();

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DG_ST_C.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            CB_ST_C.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DG_ST_C.CanUserAddRows = false;
        }
    }
}
