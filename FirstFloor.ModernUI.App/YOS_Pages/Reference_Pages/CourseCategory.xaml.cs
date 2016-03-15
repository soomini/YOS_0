using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
{
    public partial class CourseCategory : UserControl
    {

        private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();

        private DataSet CATEGORY_DS1 = new DataSet("CATEGORY_DS");
        private DataSet CATEGORY_DS2 = new DataSet("CATEGORY_DS2");
        private DataSet CATEGORY_DS3 = new DataSet("CATEGORY_DS3");
        private DataSet CATEGORY_DS4 = new DataSet("CATEGORY_DS4");

        private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleDataAdapter Adpt;

        public CourseCategory()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM SubjectCATEGORY", strOraConn);
            Adpt = new OracleDataAdapter("SELECT * FROM TargetCATEGORY", strOraConn);
            Adpt = new OracleDataAdapter("SELECT * FROM InstitutionCATEGORY", strOraConn);
            Adpt = new OracleDataAdapter("SELECT * FROM PurposeCATEGORY", strOraConn);

            DataTable SubjectCATEGORY_dt = CATEGORY_DS4.Tables["SubjectCATEGORY_dt"];
            DataTable TargetCATEGORY_dt = CATEGORY_DS3.Tables["TargetCATEGORY_dt"];
            DataTable InstitutionCATEGORY_dt = CATEGORY_DS2.Tables["InstitutionCATEGORY_dt"];
            DataTable PurposeCATEGORY_dt = CATEGORY_DS1.Tables["PurposeCATEGORY_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(CATEGORY_DS4, "SubjectCATEGORY_dt");
            Adpt.Fill(CATEGORY_DS3, "TargetCATEGORY_dt");
            Adpt.Fill(CATEGORY_DS2, "InstitutionCATEGORY_dt");
            Adpt.Fill(CATEGORY_DS1, "PurposeCATEGORY_dt");

            DGCat4.ItemsSource = CATEGORY_DS4.Tables["SubjectCATEGORY_dt"].DefaultView;
            DGCat3.ItemsSource = CATEGORY_DS3.Tables["TargetCATEGORY_dt"].DefaultView;
            DGCat2.ItemsSource = CATEGORY_DS2.Tables["InstitutionCATEGORY_dt"].DefaultView;
            DGCat1.ItemsSource = CATEGORY_DS1.Tables["PurposeCATEGORY_dt"].DefaultView;
            //DGCat.CanUserAddRows = false;
            #endregion
        }


    }
}
