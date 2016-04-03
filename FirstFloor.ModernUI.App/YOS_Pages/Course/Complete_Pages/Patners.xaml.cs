using System.Windows.Controls;
using System;
using System.Data;
using System.Windows;
using System.IO;
using System.Windows.Threading;


//test 
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace FirstFloor.ModernUI.App.YOS_Pages.Course.Complete_Pages
{

	public partial class Patners: UserControl
    {
        public Dispatcher UIDispatcher = Application.Current.Dispatcher;
        static StringWriter stream = new StringWriter();
        static DataTable LECTURE_Dt = new DataTable();
        static DataTable PARTNERS_Dt = new DataTable();
        static DataTable PARTNERS_filter_Dt = new DataTable();
        static DataRow[] CellClickEventROW;

        static int LectureDT_cnt = 0;
        static bool a = true;
        

        //private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        ////private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        ////private OracleConnection Con = new OracleConnection();
        //private DataSet STATUS_DS = new DataSet("STATUS_DS");
        //private OracleCommandBuilder oraBuilder; // SelectCommand(읽기), InsertCommend(삽입), DeleteCommand(삭제), UpdateCommand(수정)의 기능
        ////private OracleCommandBuilder oraBuilder1;
        ////private OracleCommandBuilder oraBuilder2;
        //private OracleDataAdapter Adpt;
        //private OracleDataAdapter Adpt1;
        //private OracleDataAdapter Adpt2;

        public Patners()
        {
            InitializeComponent();

            if (PARTNERS_Dt.TableName.ToString() == "")
            {              
                UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
                UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PARTNERS")));
            }

            //UIDispatcher.Invoke(new Action(() =>
            //   {
            //       if (PARTNERS_Dt.TableName.ToString() == "")
            //       {
            //           UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //           UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("LECTURE")));
            //           UIDispatcher.Invoke(new Action(() => LECTURE_Dt = YOS.CAccessDB.getdt()));

            //           UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //           UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PARTNERS")));
            //           UIDispatcher.Invoke(new Action(() => PARTNERS_Dt = YOS.CAccessDB.getdt()));
            //       }
            //   }
            // ));


            #region 데이터 가져오기 및 DataGrid에 추가
           
            ////DG_CO_PL(왼쪽) //DG_CO_P(오른쪽)

            //Adpt = new OracleDataAdapter("SELECT p.NAME, p.JOB, s.LECTURENAME, s.ROLE, s.AMOUNT FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID", strOraConn);
            //Adpt1 = new OracleDataAdapter("SELECT DISTINCT p.NAME, p.JOB FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID ORDER BY NAME", strOraConn);
            ////Adpt2 = new OracleDataAdapter("SELECT COUNT(*) \"C1\" FROM STATUS s, PARTNERS p WHERE s.NAME=p.ID AND p.NAME='김수민';",strOraConn);
            //DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];
            //DataTable STATUS_dt1 = STATUS_DS.Tables["STATUS_dt1"];
            //DataTable STATUS_dt2 = STATUS_DS.Tables["STATUS_dt2"];
            //oraBuilder = new OracleCommandBuilder(Adpt);
            //oraBuilder = new OracleCommandBuilder(Adpt1);
            ////oraBuilder2 = new OracleCommandBuilder(Adpt2);

            //Adpt.Fill(STATUS_DS, "STATUS_dt");
            //DG_CO_P.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            //Adpt1.Fill(STATUS_DS, "STATUS_dt1");
            //DG_CO_PD.ItemsSource = STATUS_DS.Tables["STATUS_dt1"].DefaultView;
            //DG_CO_PL.ItemsSource = STATUS_DS.Tables["STATUS_dt1"].DefaultView;  
            ////Adpt.Fill(STATUS_DS, "STATUS_dt2");
            ////DG_CO_PD.ItemsSource = STATUS_DS.Tables["STATUS_dt2"].DefaultView;
            //DG_CO_P.CanUserAddRows = false;
            //DG_CO_PL.CanUserAddRows = false;
            //DG_CO_PD.CanUserAddRows = false;
            #endregion

        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //searchDate();
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //searchDate();
        }

        private void searchDate()
        {
            //string sd = String.Format("{0:yy-MM-dd}", StartDate.SelectedDate);
            //string ed = String.Format("{0:yy-MM-dd}", EndDate.SelectedDate);

            //string dynamicquery = "";

            //if (sd != "" && ed != "")
            //{

            //}

            //if (sd != "")
            //{
            //    dynamicquery += "CLOSEDATE >= to_date('" + sd + "', 'yy-mm-dd')";
            //}

            //if (ed != "")
            //{
            //    if (dynamicquery != "")
            //        dynamicquery += " and ";
            //    else
            //        dynamicquery += " ";

            //    dynamicquery += "STARTDATE <= to_date('" + ed + "', 'yy-mm-dd')";
            //}

            //if (dynamicquery != "")
            //    dynamicquery = " where " + dynamicquery;

            //Adpt = new OracleDataAdapter("SELECT * FROM STATUS" + dynamicquery, strOraConn);
            //DataTable STATUS_dt = STATUS_DS.Tables["STATUS_dt"];

            //DG_CO_PL.ItemsSource = null;
            ////CBOXLEC.ItemsSource = null;

            //STATUS_dt.Clear();

            //oraBuilder = new OracleCommandBuilder(Adpt);

            //Adpt.Fill(STATUS_DS, "STATUS_dt");
            //DG_CO_PL.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            ////CBOXLEC.ItemsSource = STATUS_DS.Tables["STATUS_dt"].DefaultView;
            //DG_CO_PL.CanUserAddRows = false;
        }

        private void DG_CO_PL_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            DataSet DS = new DataSet();

            string strconn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
            OracleDataAdapter orada = new OracleDataAdapter("select * from lecture2", strconn);
            orada.Fill(DS, "LECTURE_Dt");

            //for (int i = 0; LECTURE_Dt.Columns.Count >= 0; i++)
            //{
            //    UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //    UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("LECTURE")));
            //    UIDispatcher.Invoke(new Action(() => LECTURE_Dt = YOS.CAccessDB.getdt()));
            //}

            //가상테이블 리셋
            PARTNERS_filter_Dt.Clear();
            //가상테이블 생성 (컬럼 5개)
            PARTNERS_filter_Dt = new DataTable();
            PARTNERS_filter_Dt.Columns.Add("이름", Type.GetType("System.String"));
            PARTNERS_filter_Dt.Columns.Add("직책", Type.GetType("System.String"));
            PARTNERS_filter_Dt.Columns.Add("강좌명", Type.GetType("System.String"));
            PARTNERS_filter_Dt.Columns.Add("역할", Type.GetType("System.String"));
            PARTNERS_filter_Dt.Columns.Add("금액", Type.GetType("System.Decimal"));

            //선택한 DG_CO_PL의 인덱스로 이름하고 직책 셋팅?
            DataGridRow row = (DataGridRow)DG_CO_PL.ItemContainerGenerator.ContainerFromIndex(DG_CO_PL.SelectedIndex);
            string expression = "NAME = '" + ((TextBlock)(DG_CO_PL.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
            CellClickEventROW = PARTNERS_Dt.Select(expression);
            //LectureDT_cnt = LECTURE_Dt.Rows.Count;
            LectureDT_cnt = DS.Tables["LECTURE_Dt"].Rows.Count;

            for (int i = 0; i < LectureDT_cnt; i++)
            {
                if (DS.Tables["LECTURE_Dt"].Rows[i][14].ToString() == "10" && (DS.Tables["LECTURE_Dt"].Rows[i][6].ToString() == CellClickEventROW[0][1].ToString()))
            {
                    DataRow newRow = PARTNERS_filter_Dt.NewRow();

                    newRow[0] = CellClickEventROW[0][1];
                    newRow[1] = CellClickEventROW[0][2];
                    newRow[2] = DS.Tables["LECTURE_Dt"].Rows[i][1];
                    newRow[3] = "PM";
                    newRow[4] = DS.Tables["LECTURE_Dt"].Rows[i][13];

                    PARTNERS_filter_Dt.Rows.Add(newRow);
                }
            }

            //2. 소개자
            for (int i = 0; i < LectureDT_cnt; i++)
            {
                if (DS.Tables["LECTURE_Dt"].Rows[i][14].ToString() == "10" && (DS.Tables["LECTURE_Dt"].Rows[i][7].ToString() == CellClickEventROW[0][1].ToString()))
            {
                    DataRow newRow = PARTNERS_filter_Dt.NewRow();

                    newRow[0] = CellClickEventROW[0][1];
                    newRow[1] = CellClickEventROW[0][2];
                    newRow[2] = DS.Tables["LECTURE_Dt"].Rows[i][1];
                    newRow[3] = "소개자";
                    newRow[4] = DS.Tables["LECTURE_Dt"].Rows[i][13];

                    PARTNERS_filter_Dt.Rows.Add(newRow);
                }

            }

            //lecture table 검색결과로 값 넣기
            //1. PM
            //for (int i = 0; i < LectureDT_cnt; i++)
            //{
            //    if (LECTURE_Dt.Rows[i][14].ToString() == "10" && (LECTURE_Dt.Rows[i][6].ToString() == CellClickEventROW[0][1].ToString()) )
            //    {
            //        DataRow newRow = PARTNERS_filter_Dt.NewRow();

            //        newRow[0] = CellClickEventROW[0][1];
            //        newRow[1] = CellClickEventROW[0][2];
            //        newRow[2] = LECTURE_Dt.Rows[i][1];
            //        newRow[3] = "PM";
            //        newRow[4] = LECTURE_Dt.Rows[i][13];
            //    }
            //}

            ////2. 소개자
            //for (int i = 0; i < LectureDT_cnt; i++)
            //{
            //    if ( LECTURE_Dt.Rows[i][14].ToString() == "10" && (LECTURE_Dt.Rows[i][7].ToString() == CellClickEventROW[0][1].ToString()) )
            //    {
            //        DataRow newRow = PARTNERS_filter_Dt.NewRow();

            //        newRow[0] = CellClickEventROW[0][1];
            //        newRow[1] = CellClickEventROW[0][2];
            //        newRow[2] = LECTURE_Dt.Rows[i][1];
            //        newRow[3] = "소개자";
            //        newRow[4] = LECTURE_Dt.Rows[i][13];

            //    }

            //}

            //데이터 그리드에 가상테이블 연결
            DG_CO_P.ItemsSource = PARTNERS_filter_Dt.DefaultView;
        }

        private void DG_CO_PL_Loaded(object sender, RoutedEventArgs e)
        {          


        }

        private void DG_CO_PL_LayoutUpdated(object sender, EventArgs e)
        {
            if (PARTNERS_Dt.TableName.ToString() == "")
            {
                UIDispatcher.Invoke(new Action(() => PARTNERS_Dt = YOS.CAccessDB.getdt()));              
                UIDispatcher.Invoke(new Action(() => DG_CO_PL.ItemsSource = PARTNERS_Dt.DefaultView));//수신
            }
            //else
            //{
            //    if (YOS.CAccessDB.getdt().TableName.ToString() == "LECTURE")
            //    {
            //        if (a)
            //        {
            //            UIDispatcher.Invoke(new Action(() => LECTURE_Dt = YOS.CAccessDB.getdt()));
            //            a = false;
            //        }
            //    }
            //    else
            //    { 
            //        UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //        UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("LECTURE")));
            //    }

            //}
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
           // wUpdate();
        }
    }
}
