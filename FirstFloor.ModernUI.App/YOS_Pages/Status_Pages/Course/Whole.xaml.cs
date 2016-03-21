using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;


namespace FirstFloor.ModernUI.App.YOS_Pages.Status_Pages.Course
{

	public partial class Whole: UserControl
    {
        private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet STATUS_DS = new DataSet("STATUS_DS");
        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleDataAdapter Adpt;


        public Whole()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM STATUS", strOraConn);

            DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(STATUS_DS, "STATUS_dt");
            DGST.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            DGST.CanUserAddRows = false;

            #endregion
        }
    }
}
