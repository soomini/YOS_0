using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.IO;


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{
    public partial class CourseCategory : UserControl
    {
        static StringWriter stream = new StringWriter();
        public Dispatcher UIDispatcher = Application.Current.Dispatcher;

        private DataSet CATEGORY_DS = new DataSet("CATEGORY_DS");

        static DataTable PurposeCATEGORY_Dt = new DataTable();
        static DataTable PurposeCATEGORY_Dt_copy = new DataTable();
        static DataSet PurposeCATEGORY_Ds = new DataSet();

        static DataTable InstitutionCATEGORY_Dt = new DataTable();
        static DataTable InstitutionCATEGORY_Dt_copy = new DataTable();
        static DataSet InstitutionCATEGORY_Ds = new DataSet();

        static DataTable TargetCATEGORY_Dt = new DataTable();
        static DataTable TargetCATEGORY_Dt_copy = new DataTable();
        static DataSet TargetCATEGORY_Ds = new DataSet();

        static DataTable SubjectCATEGORY_Dt = new DataTable();
        static DataTable SubjectCATEGORY_Dt_copy = new DataTable();
        static DataSet SubjectCATEGORY_Ds = new DataSet();

        static string getmsave = null;
        static int Swap;

        public CourseCategory()
        {
            InitializeComponent();

        }

        private void Purpose_Checked(object sender, RoutedEventArgs e)
        {
            DGCat1.Visibility = Visibility.Visible;
            DGCat2.Visibility = Visibility.Hidden;
            DGCat3.Visibility = Visibility.Hidden;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Institution_Checked(object sender, RoutedEventArgs e)
        {
            DGCat2.Visibility = Visibility.Visible;
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat3.Visibility = Visibility.Hidden;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Target_Checked(object sender, RoutedEventArgs e)
        {
            DGCat3.Visibility = Visibility.Visible;
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Collapsed;
            DGCat4.Visibility = Visibility.Hidden;
        }

        private void Subject_Checked(object sender, RoutedEventArgs e)
        {
            DGCat4.Visibility = Visibility.Visible;
            DGCat1.Visibility = Visibility.Collapsed;
            DGCat2.Visibility = Visibility.Collapsed;
            DGCat3.Visibility = Visibility.Collapsed;
        }

        private void Whole_Checked(object sender, RoutedEventArgs e)
        {
            getmsave = CSampleClient.Program.getmsave();

            if (Swap == 1)
            {
                

                DGCat1.Visibility = Visibility.Visible;
                DGCat2.Visibility = Visibility.Visible;
                DGCat3.Visibility = Visibility.Visible;
                DGCat4.Visibility = Visibility.Visible;
            }
            else
            {
                Swap = 1;
            }
        }
        static bool aaa = true;
        static bool bbb = true;

        private void DGCat1_LayoutUpdated(object sender, EventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => DGCat1.ItemsSource = PurposeCATEGORY_Dt_copy.DefaultView));//수신
            DGCat1.ItemsSource = PurposeCATEGORY_Dt_copy.DefaultView;
        }
        private void DGCat2_LayoutUpdated(object sender, EventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => DGCat2.ItemsSource = InstitutionCATEGORY_Dt_copy.DefaultView));//수신
        }
        private void DGCat3_LayoutUpdated(object sender, EventArgs e)
        {
         //   UIDispatcher.Invoke(new Action(() => TargetCATEGORY_Dt = YOS.CAccessDB.getdt()));
          //  UIDispatcher.Invoke(new Action(() => DGCat3.ItemsSource = TargetCATEGORY_Dt.DefaultView));//수신
        }

        private void DGCat4_LayoutUpdated(object sender, EventArgs e)
        {
          //  UIDispatcher.Invoke(new Action(() => SubjectCATEGORY_Dt = YOS.CAccessDB.getdt()));
           // UIDispatcher.Invoke(new Action(() => DGCat4.ItemsSource = SubjectCATEGORY_Dt.DefaultView));//수신
        }


        private void DGCat1_Loaded(object sender, RoutedEventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PURPOSECATEGORY")));
            //if (aaa)
            //{
            //    UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt = YOS.CAccessDB.getdt()));
            //    UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt_copy = PurposeCATEGORY_Dt.Clone()));
            //    aaa = false;
            //}      

            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("PURPOSECATEGORY")));
            UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt = YOS.CAccessDB.getdt()));
            UIDispatcher.Invoke(new Action(() => PurposeCATEGORY_Dt_copy = PurposeCATEGORY_Dt.Clone()));
            UIDispatcher.Invoke(new Action(() => DGCat1.ItemsSource = PurposeCATEGORY_Dt_copy.DefaultView));//수신    
            
        }

        private void DGCat2_Loaded(object sender, RoutedEventArgs e)
        {
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("INSTITUTIONCATEGORY")));
            //UIDispatcher.Invoke(new Action(() => InstitutionCATEGORY_Dt = YOS.CAccessDB.getdt()));
            //InstitutionCATEGORY_Dt_copy = InstitutionCATEGORY_Dt.Copy();
           
        }

        private void DGCat3_Loaded(object sender, RoutedEventArgs e)
        {
          //  UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
          //  UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("TARGETCATEGORY")));
        }

        private void DGCat4_Loaded(object sender, RoutedEventArgs e)
        {
           // UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            //UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SendMessage("SUBJECTCATEGORY")));
        }
    }
}
