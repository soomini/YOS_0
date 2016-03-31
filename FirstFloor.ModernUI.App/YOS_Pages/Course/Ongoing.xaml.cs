using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

using System.Collections.Generic;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course
{

    public partial class Ongoing : UserControl
    {
        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        //private string strOraConn = "Data Source=MYORACLE;User Id=dba_soo;Password=tnalsl";
        //private OracleConnection Con = new OracleConnection();
        private DataSet LECTURE_DS = new DataSet("LECTURE_DS");
        private DataSet LECTUREO_DS = new DataSet("LECTUREO_DS");
        private DataSet PARTNERS_DS = new DataSet("PARTNERS_DS");
        private OracleCommandBuilder oraBuilder;
        private OracleCommandBuilder oraBuilder2;
        private OracleCommandBuilder oraBuilder3;
        private OracleDataAdapter Adpt;
        private OracleDataAdapter Adpt2;
        private OracleDataAdapter Adpt3;

        private Dictionary<string, int> LECTUREL = new Dictionary<string, int>();
        private Dictionary<string, string> PL = new Dictionary<string, string>();
        private Dictionary<string, int> PLlinker = new Dictionary<string, int>();
        public object SelectedItem { get; set; }


        public Ongoing()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            //SELECT * FROM LECTURE

            wUpdate();

            #endregion
        }

        private void wUpdate()
        {

            Adpt2 = new OracleDataAdapter("SELECT * FROM PARTNERS", strOraConn);
            oraBuilder2 = new OracleCommandBuilder(Adpt2);
            Adpt2.Fill(PARTNERS_DS, "PARTNERS_dt");
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            PL.Clear();
            PLlinker.Clear();
            for (int i = 0; i < PARTNERS_dt.Rows.Count; i++)
            {
                PL.Add(PARTNERS_dt.Rows[i].ItemArray[0].ToString(), PARTNERS_dt.Rows[i].ItemArray[1].ToString());
                PLlinker.Add(PARTNERS_dt.Rows[i].ItemArray[0].ToString(), i);
            }

            Adpt3 = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);
            oraBuilder3 = new OracleCommandBuilder(Adpt3);
            Adpt3.Fill(LECTUREO_DS, "LECTUREO_dt");
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];

            LECTUREL.Clear();
            for (int i = 0; i < LECTUREO_dt.Rows.Count; i++)
            {
                LECTUREL.Add(LECTUREO_dt.Rows[i].ItemArray[0].ToString(), i);
            }


            Adpt = new OracleDataAdapter("SELECT l.LECTURENAME, l.PURPOSECATEGORY, l.INSTITUTIONCATEGORY, l.TARGETCATEGORY, l.SUBJECTCATEGORY, p1.ID \"NC1\", p1.NAME \"N1\", p2.ID \"NC2\", p2.NAME \"N2\", l.LECPLACE, l.STARTDATE, l.CLOSEDATE, l.LECTURETIME, l.LECTUREFEE, l.COMPLETERATE FROM LECTURE l, PARTNERS p1, PARTNERS p2 WHERE l.PROJMANAGER = p1.ID AND l.RECOMMENDER = p2.ID", strOraConn);
            //Adpt = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);
            DataTable PERSON_dt = LECTURE_DS.Tables["LECTURE_dt"];
            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DGLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            CBOXLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DGLEC.CanUserAddRows = false;

            for (int i = 0; i < DGLEC.Items.Count; i++)
            {

            }
        }

        private void CommonDialog_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ModernDialog
            {
                Title = "Common dialog",
                Content = new YOS_Content.AddCost()
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };

            dlg.ShowDialog();
        }

        private void btn_Registration_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            if (DGLEC.SelectedIndex == -1)
            {

                LECTURE_dt.Rows.Add(tbx_Course.Text, cbb_CategoryOfPurpose.Text, cbb_CategoryOfInstitution.Text, cbb_CategoryOfTarget.Text, cbb_CateroryOfSubject.Text, tbx_PMName.Text, tbx_RecommanderName.Text, tbx_Place.Text, dp_StartDate.Text, dp_EndDate.Text, tbx_Time.Text, tbx_TotalMoney.Text);

                try
                {
                    Adpt.Update(LECTURE_DS, "LECTURE_dt");
                    MessageBox.Show("추가가 완료되었습니다.");
                }
                catch (Exception ex)
                {
                    LECTURE_dt.Rows.RemoveAt(LECTURE_dt.Rows.Count - 1);
                    MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
                }
            }

            string Record = "";
            foreach (DataRow R in LECTURE_dt.Rows)
            {
                switch (R.RowState)
                {
                    case DataRowState.Added:
                        Record = string.Format("추가: {0}", Convert.ToString(R["LECTURENAME"]));
                        MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                        break;

                    case DataRowState.Deleted:
                        Record = string.Format("삭제: {0}", Convert.ToString(R["LECTURENAME", DataRowVersion.Original]));
                        MessageBox.Show($"데이터가 삭제되었습니다. {Record}");
                        break;
                }

                bool rowupdated = false;
                foreach (DataColumn C in LECTURE_dt.Columns)
                {
                    if (!R[C, DataRowVersion.Original].Equals(R[C, DataRowVersion.Current]))
                    {
                        rowupdated = true;

                        Record = string.Format("수정: {0}", Convert.ToString(R["LECTURENAME"]));
                        MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                    }
                }

                if (rowupdated == true)
                {

                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[1] = R.ItemArray[1];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[2] = R.ItemArray[2];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[3] = R.ItemArray[3];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[4] = R.ItemArray[4];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[5] = R.ItemArray[5];

                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[6] = R.ItemArray[7];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[7] = R.ItemArray[9];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[8] = R.ItemArray[10];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[9] = R.ItemArray[11];
                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[10] = R.ItemArray[12];

                    LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]].ItemArray[11] = R.ItemArray[13];

                    PARTNERS_dt.Rows[PLlinker[R.ItemArray[5].ToString()]].ItemArray[1] = R.ItemArray[6].ToString();
                    PARTNERS_dt.Rows[PLlinker[R.ItemArray[7].ToString()]].ItemArray[1] = R.ItemArray[8].ToString();
                }
            }

            //try
            //{

            

                Adpt2.Update(PARTNERS_DS, "PARTNERS_dt");
                Adpt3.Update(LECTUREO_DS, "LECTUREO_dt");
            PARTNERS_dt.AcceptChanges();
            LECTUREO_dt.AcceptChanges();

            //PARTNERS_dt.Clear();
            //LECTUREO_dt.Clear();
            //LECTURE_dt.Clear();

            //wUpdate();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("에러 발생: " + ex.ToString());
            //}

            try
            {
                LECTURE_dt.AcceptChanges();
            }
            catch
            {

            }
            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));
        }

        private void CBOXLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGLEC.SelectedIndex = CBOXLEC.SelectedIndex;
        }

        private void DGLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_Registration.Content = "업데이트";

            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            //tbN1.Text = PL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[5].ToString()];
            //tbN2.Text = PL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[7].ToString()];
        }

        public void StackPannel_control_init()
        {
            tbx_Course.Text = null;
            cbb_CategoryOfPurpose.Text = null;
            cbb_CategoryOfInstitution.Text = null;
            cbb_CategoryOfTarget.Text = null;
            cbb_CateroryOfSubject.Text = null;
            tbx_PMName.Text = null;
            tbx_RecommanderName.Text = null;
            tbx_Place.Text = null;
            dp_StartDate.Text = null;
            dp_EndDate.Text = null;
            tbx_Time.Text = null;
            tbx_TotalMoney.Text = null;
            txtCompleteRatio.Text = null;

        }

        private void btn_Init_Click(object sender, RoutedEventArgs e)
        {
            DGLEC.SelectedIndex = -1;
            StackPannel_control_init();
            btn_Registration.Content = "등록";
        }

        private void tbN1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            if (DGLEC.SelectedIndex != -1)
            {
                PARTNERS_dt.Rows[PLlinker[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[5].ToString()]].ItemArray[1] = tbN1.Text;
            }
        }

        private void tbN2_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            if (DGLEC.SelectedIndex != -1)
            {
                PARTNERS_dt.Rows[PLlinker[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[7].ToString()]].ItemArray[1] = tbN2.Text;
            }
        }
    }
}
