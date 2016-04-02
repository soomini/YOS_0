using System.Windows.Controls;
using System;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course.Complete_Pages
{

	public partial class Patners: UserControl
    {
        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        //private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet STATUS_DS = new DataSet("STATUS_DS");
        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        //private OracleCommandBuilder oraBuilder1;
        //private OracleCommandBuilder oraBuilder2;
        private OracleDataAdapter Adpt;
        private OracleDataAdapter Adpt1;
        //private OracleDataAdapter Adpt2;
        public Patners()
        {
            InitializeComponent();

            wUpdate();
        }

        private void wUpdate()
        {
            try
            {
                DG_CO_P.ItemsSource = null;

                DataTable d1 = STATUS_DS.Tables["STATUS_dt"];
                DataTable d2 = STATUS_DS.Tables["STATUS_dt1"];

                d1.Clear();
                d2.Clear();
            }
            catch
            {

            }
            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT p.NAME, p.JOB, s.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID", strOraConn);
            Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, p.JOB FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID ORDER BY NAME", strOraConn);
            //Adpt2 = new OracleDataAdapter("SELECT COUNT(*) \"C1\" FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID AND p.NAME='김수민';",strOraConn);
            DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];
            DataTable STATUS_dt1 = STATUS_DS.Tables["STATUS_dt1"];
            //DataTable STATUS_dt2 = STATUS_DS.Tables["STATUS_dt2"];
            oraBuilder = new OracleCommandBuilder(Adpt);
            oraBuilder = new OracleCommandBuilder(Adpt1);
            //oraBuilder2 = new OracleCommandBuilder(Adpt2);

            Adpt.Fill(STATUS_DS, "STATUS_dt");
            DG_CO_P.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            Adpt1.Fill(STATUS_DS, "STATUS_dt1");
            DG_CO_PD.ItemsSource = STATUS_DS.Tables["STATUS_dt1"].DefaultView;
            DG_CO_PL.ItemsSource = STATUS_DS.Tables["STATUS_dt1"].DefaultView;
            //Adpt.Fill(STATUS_DS, "STATUS_dt2");
            //DG_CO_PD.ItemsSource = STATUS_DS.Tables["STATUS_dt2"].DefaultView;
            DG_CO_P.CanUserAddRows = false;
            DG_CO_PL.CanUserAddRows = false;
            DG_CO_PD.CanUserAddRows = false;
            #endregion
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();
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

            Adpt = new OracleDataAdapter("SELECT * FROM STATUS" + dynamicquery, strOraConn);
            DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];

            DG_CO_PL.ItemsSource = null;
            //CBOXLEC.ItemsSource = null;

            STATUS_dt.Clear();

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(STATUS_DS, "STATUS_dt");
            DG_CO_PL.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            //CBOXLEC.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            DG_CO_PL.CanUserAddRows = false;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            wUpdate();
        }
    }
}
