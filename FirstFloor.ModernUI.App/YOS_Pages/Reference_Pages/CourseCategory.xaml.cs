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

        private DataSet CATEGORY_DS = new DataSet("CATEGORY_DS");

        private OracleCommandBuilder oraBuilder1; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        private OracleCommandBuilder oraBuilder2;
        private OracleCommandBuilder oraBuilder3;
        private OracleCommandBuilder oraBuilder4;
        private OracleDataAdapter Adpt1;
        private OracleDataAdapter Adpt2;
        private OracleDataAdapter Adpt3;
        private OracleDataAdapter Adpt4;

        public CourseCategory()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt1 = new OracleDataAdapter("SELECT * FROM PurposeCATEGORY", strOraConn);
            Adpt2 = new OracleDataAdapter("SELECT * FROM InstitutionCATEGORY", strOraConn);
            Adpt3 = new OracleDataAdapter("SELECT * FROM TargetCATEGORY", strOraConn);
            Adpt4 = new OracleDataAdapter("SELECT * FROM SubjectCATEGORY", strOraConn);

            DataTable PurposeCATEGORY_dt = CATEGORY_DS.Tables["PurposeCATEGORY_dt"];
            DataTable InstitutionCATEGORY_dt = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"];
            DataTable TargetCATEGORY_dt = CATEGORY_DS.Tables["TargetCATEGORY_dt"];
            DataTable SubjectCATEGORY_dt = CATEGORY_DS.Tables["SubjectCATEGORY_dt"];

            oraBuilder1 = new OracleCommandBuilder(Adpt1);
            oraBuilder2 = new OracleCommandBuilder(Adpt2);
            oraBuilder3 = new OracleCommandBuilder(Adpt3);
            oraBuilder4 = new OracleCommandBuilder(Adpt4);

            Adpt1.Fill(CATEGORY_DS, "PurposeCATEGORY_dt");
            Adpt2.Fill(CATEGORY_DS, "InstitutionCATEGORY_dt");
            Adpt3.Fill(CATEGORY_DS, "TargetCATEGORY_dt");
            Adpt4.Fill(CATEGORY_DS, "SubjectCATEGORY_dt");

            DGCat1.ItemsSource = CATEGORY_DS.Tables["PurposeCATEGORY_dt"].DefaultView;
            DGCat2.ItemsSource = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"].DefaultView;
            DGCat3.ItemsSource = CATEGORY_DS.Tables["TargetCATEGORY_dt"].DefaultView;
            DGCat4.ItemsSource = CATEGORY_DS.Tables["SubjectCATEGORY_dt"].DefaultView;
            //DGCat.CanUserAddRows = false;
            #endregion
        }


    }
}
