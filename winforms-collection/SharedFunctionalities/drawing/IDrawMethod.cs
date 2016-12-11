using System.Drawing;

namespace SharedFunctionalities.drawing {
    public interface IDrawMethod {
        /// <summary>
        /// draw what we want.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="wholeComponent"></param>
        /// <param name="clippingRect"></param>
        void Draw( Graphics g,ref Rectangle wholeComponent,ref Rectangle clippingRect );
        /// <summary>
        /// tells the system if we will be opauge, / are there any transperant things.
        /// </summary>
        /// <returns>true if we have any pixels with alpha < max </returns>
        bool IsTransperant();

        /// <summary>
        /// tells the system if we will occupy the rectangle.
        /// </summary>
        /// <returns>true if we will be drawing to the whole rectangle (eventually fill it).</returns>
        bool WillFillRectangleOut();

        /// <summary>
        /// simply tells if we allow caching (might not be used).
        /// </summary>
        /// <returns>true if caching is allowed.</returns>
        bool MayCacheLayer();

        /// <summary>
        /// if the last draw is invalid.
        /// </summary>
        /// <returns>true if it is invalid</returns>
        bool IsCacheInvalid();

        /// <summary>
        /// if we should draw this
        /// </summary>
        /// <returns></returns>
        bool MayDraw();
        /// <summary>
        /// if we modify the size in draw, then we should also do it here.
        /// </summary>
        /// <param name="newSize"></param>
        void ModifySize( ref Rectangle newSize );
    }
}
