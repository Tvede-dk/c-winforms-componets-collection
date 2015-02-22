using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers.backgrounds {
	public class GridBackground : BaseDraw {


		#region property SpaceBetween
		private int _SpaceBetween = 15 * 5;


		public int SpaceBetween {
			get { return _SpaceBetween; }
			set { _SpaceBetween = value; invalidate(); }
		}
		#endregion



		#region property lineSize
		private int _lineSize = 1;


		public int lineSize {
			get { return _lineSize; }
			set { _lineSize = value; }
		}
		#endregion

		public int translateX = 0;
		#region property LineColor
		private Color _LineColor = Color.LightSlateGray;


		public Color LineColor {
			get { return _LineColor; }
			set {
				_LineColor = value;
				invalidate();
			}
		}
		#endregion



		~GridBackground() {
			invalidate();
		}

		public override void doDraw( Graphics g , ref Rectangle wholeComponent , ref Rectangle clippingRect ) {
			Rectangle newRec = wholeComponent;
			int maxVal = Math.Max(wholeComponent.Width , wholeComponent.Height);
			if ((SpaceBetween/maxVal) <= 16)
			{
				//draw the lines ourself.
			}
			else
			{
				wholeComponent = drawUsingBitblt(g , newRec);	
			}
			
		}

		private Rectangle drawUsingBitblt( Graphics g , Rectangle wholeComponent ) {
			Bitmap pattern = createPattern();
			pattern.bitbltRepeat(g , wholeComponent.Width - translateX , wholeComponent.Height);
			pattern.Dispose();
			//g.TranslateClip( translateX, 0 );
			return wholeComponent;
		}

		private Bitmap createPattern() {
			#region a far, close, and standard distance mode based on simple benchmarking.
			int preFactor = 10;
			int divisionFactor = 20;
			if ( SpaceBetween > 150 ) {
				preFactor = 8;
			}
			if ( SpaceBetween < 20 ) {
				preFactor = 20;
			}
			#endregion
			int totalSize = (preFactor * SpaceBetween) - ((SpaceBetween * SpaceBetween) / divisionFactor);
			totalSize = totalSize - (totalSize % SpaceBetween);
			totalSize = Math.Max(totalSize , SpaceBetween * 1);
			Bitmap pattern = new Bitmap((int)((totalSize)) , (int)((totalSize)) , PixelFormat.Format32bppPArgb);
			using ( var gg = Graphics.FromImage(pattern) ) {
				Stopwatch sw = new Stopwatch();
				sw.Start();
				Pen blackPen = new Pen(Color.Black , lineSize);
				for ( int i = 0 ; i < totalSize / SpaceBetween ; i++ ) {
					gg.DrawLine(Pens.Black , 0 , SpaceBetween * i , pattern.Width , SpaceBetween * i);
				}
				for ( int i = 0 ; i < totalSize / SpaceBetween ; i++ ) {
					gg.DrawLine(Pens.Black , SpaceBetween * i , 0 , SpaceBetween * i , pattern.Height);
				}

				sw.Stop();
				//Console.WriteLine( "time for making pattern:" + sw.Elapsed.TotalMilliseconds );
			}

			return pattern;
		}

		public override void modifySize( ref Rectangle newSize ) {

		}
		public override void invalidate() {
			base.invalidate();
		}
	}
}
