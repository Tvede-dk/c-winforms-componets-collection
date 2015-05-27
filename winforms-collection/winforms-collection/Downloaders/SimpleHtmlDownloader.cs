using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Windows_organizer.interfaces;

namespace Windows_organizer.Downloaders {
    class SimpleHtmlDownloader {

        private readonly List<Installable> productList = new List<Installable>();


        public void addInstallable( Installable ins ) {
            productList.Add( ins );
        }

        public void loadFromFile( String file ) {
            IFormatter formatter = new BinaryFormatter();
            using ( Stream stream = new FileStream( file , FileMode.Open , FileAccess.Read , FileShare.Read ) ) {
                var obj = (List<Installable>)formatter.Deserialize( stream );
                productList.Clear();
                productList.AddRange( obj );
            }

        }
        //downloadAbleHtmlList
        public void saveToFile( String file ) {
            IFormatter formatter = new BinaryFormatter();
            using ( Stream stream = new FileStream( file , FileMode.Create , FileAccess.Write , FileShare.None ) ) {
                formatter.Serialize( stream , productList );
            }
        }

        public void RemoveAtIndex(int index)
        {
            productList.RemoveAt(index);
        }


        public List<Installable> getProducts() {
            return productList;
        }

        public Installable getProductAt( int selectedIndex )
        {
            return productList.ElementAt(selectedIndex);
        }
    }
}
