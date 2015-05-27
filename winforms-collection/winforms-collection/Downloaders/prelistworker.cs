using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows.Forms.VisualStyles;
using Windows_organizer.Properties;

namespace Windows_organizer.Downloaders {
    class Prelistworker {
        public static void saveList( String fileName , List<DownloadableObjs> objs ) {
            Stream stream = null;
            try {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream( fileName , FileMode.Create , FileAccess.Write , FileShare.None );
                formatter.Serialize( stream , objs );
            } finally {
                if ( stream != null ) {
                    stream.Close();
                }
            }
        }

        public static List<DownloadableObjs> fetchList( String fileName ) {
            Stream stream = null;
            List<DownloadableObjs> objs = null;
            try {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream( fileName , FileMode.Create , FileAccess.Write , FileShare.None );
                objs = (List<DownloadableObjs>)formatter.Deserialize( stream );
            } finally {
                if ( stream != null ) {
                    stream.Close();
                }
            }
            return objs;
        }


        public static void testMdf()
        {
            
            var db = new DataContext(Settings.Default.prelistConnectionString );
          //  var table =  db.GetTable<DownloadableObjs>();
            db.Log = Console.Out;
            var query =from app in db.GetTable<DownloadableObjs>() 
                where app.name == "chrome"
                select app;
            foreach ( var item in query ) {
                Console.WriteLine( item.programUrl);
            } 
            Console.WriteLine("yolo done");

        }

    }
}
