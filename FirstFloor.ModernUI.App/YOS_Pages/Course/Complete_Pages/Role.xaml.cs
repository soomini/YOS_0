using System.Windows.Controls;
using System;
using System.Windows;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course.Complete_Pages
{

	public partial class Role: UserControl
    {

        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        //private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet LECTURE_DS = new DataSet("LECTURE_DS");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;
        public Role()
        {
            InitializeComponent();

            wUpdate();

        }

        private void wUpdate()
        {
            try
            {
                DG_CO_R.ItemsSource = null;

                DataTable d1 = LECTURE_DS.Tables["LECTURE_dt"];

                d1.Clear();
            }
            catch
            {

            }
            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);

            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            //CB_ST_C.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DG_CO_R.CanUserAddRows = false;

            #endregion
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

            DG_CO_R.ItemsSource = null;
            //CBOXLEC.ItemsSource = null;

            LECTURE_dt.Clear();

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            //CBOXLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DG_CO_R.CanUserAddRows = false;
        }

        private void LB_PM_Selected(object sender, RoutedEventArgs e)
        {
            Adpt = new OracleDataAdapter("SELECT p.NAME, l.CLOSEDATE, l.LECTURENAME, l.LECTUREFEE FROM LECTURE l, PARTNERS p WHERE l.PROJMANAGER=p.NAME", strOraConn);

            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            DG_CO_R.ItemsSource = null;
            //CBOXLEC.ItemsSource = null;

            LECTURE_dt.Clear();

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DG_CO_R.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            //CB_ST_C.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DG_CO_R.CanUserAddRows = false;
        }

        private void LB_INT_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void LB_LEC_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wUpdate();
        }
    }
}
