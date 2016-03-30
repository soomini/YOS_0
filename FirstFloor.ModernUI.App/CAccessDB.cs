using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IO;
using System.Windows;
using FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages;

namespace YOS
{
    class CAccessDB
    {
        static DataSet Ac_ds = new DataSet();
        static StringReader xmlSR;
        static DataTable Ac_dt = new DataTable();        

        static public void MyHandler(string message)
        {
            MessageBox.Show(message);
        }      

        static public void odpconn(string message)
        {         
            Ac_ds.Reset();
            Ac_ds = new DataSet();
            xmlSR = new StringReader(message);            
            Ac_ds.ReadXml(xmlSR, XmlReadMode.ReadSchema);
            Ac_dt = Ac_ds.Tables[0];      
        }        
        static public DataTable getdt()
        {         
            return Ac_dt;
        }

        static public DataSet getds()
        {            
            return Ac_ds;
        }

    }
}
