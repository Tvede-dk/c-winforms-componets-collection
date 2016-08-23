using System.Drawing;

public static class RectangleExtensions {
    public static Rectangle InnerPart( this Rectangle rec, int differenceInEveryDirection ) {
        return InnerPart( rec, differenceInEveryDirection, differenceInEveryDirection, differenceInEveryDirection * 2, differenceInEveryDirection * 2 );
    }

    public static Rectangle CalculateBorder( this Rectangle rec, int borderSize ) {
        return InnerPart( rec, borderSize / 2, borderSize / 2, borderSize, borderSize );
    }
    public static Rectangle CalculateInsideBorder( this Rectangle rec, int borderSize ) {
        return InnerPart( rec, borderSize, borderSize, borderSize * 2, borderSize * 2 );
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
