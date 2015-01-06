using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RectangleExtensions {
    public static Rectangle InnerPart( this Rectangle rec, int differenceInEveryDirection ) {
        return InnerPart( rec, differenceInEveryDirection, differenceInEveryDirection, differenceInEveryDirection * 2, differenceInEveryDirection * 2 );

    }
    /// <summary>
    /// Subtracks each component from the rectangle and returns the result.
    /// </summary>
    /// <param name="rec"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Rectangle InnerPart( this Rectangle rec, int x, int y, int width, int height ) {
        return new Rectangle( rec.X + x, rec.Y + y, rec.Width - width, rec.Height - height );

    }
}
