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
    //delegate DataTable GetDataTable();
    class CAccessDB
    {
        static DataSet Ac_ds = new DataSet();
        static StringReader xmlSR;

        static public void MyHandler(string message)
        {
            MessageBox.Show(message);
        }      

        static public void odpconn(string message)
        {            
            xmlSR = new StringReader(message);            
            Ac_ds.ReadXml(xmlSR, XmlReadMode.ReadSchema);            
        }

        static public DataSet getds()
        {
            return Ac_ds;
        }        
    }
}
