using System.Windows.Forms;

namespace winforms_collection.drawParts {
    public class RenderThreadExample : Control {

        //private ConcurrentQueue<PaintEventArgs> data = new ConcurrentQueue<PaintEventArgs>();

        //Thread render;

        public RenderThreadExample() {
            //render = new Thread( new ThreadStart( () => { } ) );
        }


        protected override void OnPaint( PaintEventArgs e ) {
            //base.OnPaint( e );
            //data.put( e );

        }




    }
}
