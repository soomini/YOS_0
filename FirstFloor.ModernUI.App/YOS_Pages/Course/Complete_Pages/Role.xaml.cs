using System.Windows.Controls;
using System;
using System.Windows;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course.Complete_Pages
{

    public partial class Role : UserControl
    {

        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        private DataSet LECTURE_DS = new DataSet("LECTURE_DS");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;
        private OracleDataAdapter Adpt1;
        public Role()
        {
            InitializeComponent();

            LB_PM.IsSelected = true;
            //wUpdate();

        }

        //private void wUpdate()
        //{
        //    try
        //    {
        //        DG_CO_R.ItemsSource = null;

        //        DataTable d1 = LECTURE_DS.Tables["LECTURE_dt"];

        //        d1.Clear();
        //    }
        //    catch
        //    {

        //    }
        //    #region 데이터 가져오기 및 DataGrid에 추가

        //    Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p", strOraConn);

        //    DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

        //    oraBuilder = new OracleCommandBuilder(Adpt);

        //    Adpt.Fill(LECTURE_DS, "LECTURE_dt");
        //    DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
        //    DG_CO_R.CanUserAddRows = false;


        //    #endregion
        //}

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

            if (LB_PM.IsSelected == true)
            {
                if (dynamicquery != "")
                    dynamicquery = " where l.PROJMANAGER=p.ID and " + dynamicquery;

                Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p" + dynamicquery, strOraConn);
                Adpt1 = new OracleDataAdapter("SELECT p.NAME, COUNT(p.NAME) AS RC, SUM(l.LECTUREFEE*0.5) AS RS FROM LECTURE l, PARTNERS p" + dynamicquery + " GROUP BY p.NAME", strOraConn);
                DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable LECTURE_dt1 = LECTURE_DS.Tables["LECTURE_dt1"];
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;

                LECTURE_dt.Clear();
                LECTURE_dt1.Clear();

                oraBuilder = new OracleCommandBuilder(Adpt);
                oraBuilder = new OracleCommandBuilder(Adpt1);

                Adpt.Fill(LECTURE_DS, "LECTURE_dt");
                DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
                Adpt1.Fill(LECTURE_DS, "LECTURE_dt1");
                DG_CO_RD.ItemsSource = LECTURE_DS.Tables["LECTURE_dt1"].DefaultView;
                DG_CO_R.CanUserAddRows = false;
                DG_CO_RD.CanUserAddRows = false;
            }

            else if (LB_INT.IsSelected == true)
            {
                if (dynamicquery != "")
                    dynamicquery = " where l.RECOMMENDER=p.ID and " + dynamicquery;

                Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p" + dynamicquery, strOraConn);
                Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, COUNT(p.NAME) AS RC, SUM(l.LECTUREFEE*0.5) AS RS FROM LECTURE l, PARTNERS p" + dynamicquery + " GROUP BY p.NAME", strOraConn);
                DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable LECTURE_dt1 = LECTURE_DS.Tables["LECTURE_dt1"];
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;

                LECTURE_dt.Clear();
                LECTURE_dt1.Clear();

                oraBuilder = new OracleCommandBuilder(Adpt);
                oraBuilder = new OracleCommandBuilder(Adpt1);

                Adpt.Fill(LECTURE_DS, "LECTURE_dt");
                DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
                Adpt1.Fill(LECTURE_DS, "LECTURE_dt1");
                DG_CO_RD.ItemsSource = LECTURE_DS.Tables["LECTURE_dt1"].DefaultView;
                DG_CO_R.CanUserAddRows = false;
                DG_CO_RD.CanUserAddRows = false;
            }

            else if (LB_LEC.IsSelected == true)
            {
                if (dynamicquery != "")
                    dynamicquery = " where l.LECTURER=p.ID and " + dynamicquery;

                Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p" + dynamicquery, strOraConn);
                Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, COUNT(p.NAME) AS RC, SUM(l.LECTUREFEE*0.5) AS RS FROM LECTURE l, PARTNERS p" + dynamicquery + " GROUP BY p.NAME", strOraConn);
                DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable LECTURE_dt1 = LECTURE_DS.Tables["LECTURE_dt1"];
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;

                LECTURE_dt.Clear();
                LECTURE_dt1.Clear();

                oraBuilder = new OracleCommandBuilder(Adpt);
                oraBuilder = new OracleCommandBuilder(Adpt1);

                Adpt.Fill(LECTURE_DS, "LECTURE_dt");
                DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
                Adpt1.Fill(LECTURE_DS, "LECTURE_dt1");
                DG_CO_RD.ItemsSource = LECTURE_DS.Tables["LECTURE_dt1"].DefaultView;
                DG_CO_R.CanUserAddRows = false;
                DG_CO_RD.CanUserAddRows = false;
            }
        }

        private void LB_PM_Selected(object sender, RoutedEventArgs e)
        {
            string sd = String.Format("{0:yy-MM-dd}", StartDate.SelectedDate);
            string ed = String.Format("{0:yy-MM-dd}", EndDate.SelectedDate);

            try
            {
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;
                DataTable d1 = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable d2 = LECTURE_DS.Tables["LECTURE_dt1"];
                d1.Clear();
                d2.Clear();
            }
            catch
            {

            }
            if (sd != "" || ed != "")
            {
                searchDate();
            }
            else {
                Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p WHERE l.PROJMANAGER=p.ID", strOraConn);
                Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, COUNT(p.NAME) AS RC, SUM(l.LECTUREFEE*0.5) AS RS FROM LECTURE l, PARTNERS p WHERE l.PROJMANAGER=p.ID GROUP BY p.NAME", strOraConn);
                DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable LECTURE_dt1 = LECTURE_DS.Tables["LECTURE_dt1"];
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;

                oraBuilder = new OracleCommandBuilder(Adpt);
                oraBuilder = new OracleCommandBuilder(Adpt1);

                Adpt.Fill(LECTURE_DS, "LECTURE_dt");
                DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
                Adpt1.Fill(LECTURE_DS, "LECTURE_dt1");
                DG_CO_RD.ItemsSource = LECTURE_DS.Tables["LECTURE_dt1"].DefaultView;
                DG_CO_R.CanUserAddRows = false;
                DG_CO_RD.CanUserAddRows = false;
            }
        }

        private void LB_INT_Selected(object sender, RoutedEventArgs e)
        {
            string sd = String.Format("{0:yy-MM-dd}", StartDate.SelectedDate);
            string ed = String.Format("{0:yy-MM-dd}", EndDate.SelectedDate);

            if (sd != "" || ed != "")
            {
                searchDate();
            }
            else {
                Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p WHERE l.RECOMMENDER=p.ID", strOraConn);
                Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, COUNT(p.NAME) AS RC, SUM(l.LECTUREFEE*0.5) AS RS FROM LECTURE l, PARTNERS p WHERE l.RECOMMENDER=p.ID GROUP BY p.NAME", strOraConn);
                DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable LECTURE_dt1 = LECTURE_DS.Tables["LECTURE_dt1"];
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;

                LECTURE_dt.Clear();
                LECTURE_dt1.Clear();

                oraBuilder = new OracleCommandBuilder(Adpt);
                oraBuilder = new OracleCommandBuilder(Adpt1);

                Adpt.Fill(LECTURE_DS, "LECTURE_dt");
                DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
                Adpt1.Fill(LECTURE_DS, "LECTURE_dt1");
                DG_CO_RD.ItemsSource = LECTURE_DS.Tables["LECTURE_dt1"].DefaultView;
                DG_CO_R.CanUserAddRows = false;
                DG_CO_RD.CanUserAddRows = false;
            }
        }

        private void LB_LEC_Selected(object sender, RoutedEventArgs e)
        {
            string sd = String.Format("{0:yy-MM-dd}", StartDate.SelectedDate);
            string ed = String.Format("{0:yy-MM-dd}", EndDate.SelectedDate);

            if (sd != "" || ed != "")
            {
                searchDate();
            }
            else {
                Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p WHERE l.LECTURER=p.ID", strOraConn);
                Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, COUNT(p.NAME) AS RC, SUM(l.LECTUREFEE*0.5) AS RS FROM LECTURE l, PARTNERS p WHERE l.LECTURER=p.ID GROUP BY p.NAME", strOraConn);
                DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable LECTURE_dt1 = LECTURE_DS.Tables["LECTURE_dt1"];
                DG_CO_R.ItemsSource = null;
                DG_CO_RD.ItemsSource = null;

                LECTURE_dt.Clear();
                LECTURE_dt1.Clear();

                oraBuilder = new OracleCommandBuilder(Adpt);
                oraBuilder = new OracleCommandBuilder(Adpt1);

                Adpt.Fill(LECTURE_DS, "LECTURE_dt");
                DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
                Adpt1.Fill(LECTURE_DS, "LECTURE_dt1");
                DG_CO_RD.ItemsSource = LECTURE_DS.Tables["LECTURE_dt1"].DefaultView;
                DG_CO_R.CanUserAddRows = false;
                DG_CO_RD.CanUserAddRows = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LB_PM.IsSelected = true;
            //wUpdate();
        }
    }
}
