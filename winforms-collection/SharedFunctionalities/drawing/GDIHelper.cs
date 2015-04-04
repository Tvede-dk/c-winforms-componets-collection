using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

public static class GDIHelper {
    private const uint SRCCOPY = 0x00CC0020;
    [SuppressUnmanagedCodeSecurityAttribute]
    [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
    public static extern IntPtr SelectObject(
        [In()] IntPtr hdc,
        [In()] IntPtr h);

    [SuppressUnmanagedCodeSecurityAttribute]
    [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject(
        [In()] IntPtr ho);

    [SuppressUnmanagedCodeSecurityAttribute]
    [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool BitBlt(
        [In()] IntPtr hdc, int x, int y, int cx, int cy,
        [In()] IntPtr hdcSrc, int x1, int y1, uint rop);


    //public static void bitblt( this Graphics grSrc, Graphics grDest, int width, int height ) {
    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();
    //    IntPtr hdcDest = IntPtr.Zero;
    //    IntPtr hdcSrc = IntPtr.Zero;
    //    IntPtr hOldObject = IntPtr.Zero;

    //    try {
    //        hdcDest = grDest.GetHdc();
    //        hdcSrc = grSrc.GetHdc();

    //        if ( !BitBlt( hdcDest, 0, 0, width, height,
    //            hdcSrc, 0, 0, 0x00CC0020U ) )
    //            throw new Win32Exception();
    //    } finally {
    //        if ( hOldObject != IntPtr.Zero ) SelectObject( hdcSrc, hOldObject );
    //        if ( hdcDest != IntPtr.Zero ) grDest.ReleaseHdc( hdcDest );
    //        if ( hdcSrc != IntPtr.Zero ) grSrc.ReleaseHdc( hdcSrc );
    //    }

    //    sw.Stop();
    //    Console.WriteLine( "bitblt:{0}", sw.Elapsed.TotalMilliseconds );
    //}
    [SuppressUnmanagedCodeSecurityAttribute]
    [SecurityPermission(SecurityAction.Assert, Flags =
       SecurityPermissionFlag.UnmanagedCode)]
    public static void bitbltRepeat(this Bitmap bmp, Graphics grDest, int desiredWith, int desiredHeight, int offsetX = 0, int offsetY = 0) {
        int height = bmp.Height;
        int width = bmp.Width;
        using (Graphics grSrc = Graphics.FromImage(bmp)) {
            IntPtr hdcDest = IntPtr.Zero;
            IntPtr hdcSrc = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldObject = IntPtr.Zero;
            try {
                hdcDest = grDest.GetHdc();
                hdcSrc = grSrc.GetHdc();
                hBitmap = bmp.GetHbitmap();
                hOldObject = SelectObject(hdcSrc, hBitmap);
                if (hOldObject == IntPtr.Zero)
                    throw new Win32Exception();
                for (int i = 0; i < Math.Ceiling(desiredHeight / (double)height) + 1; i++) {
                    for (int j = 0; j < Math.Ceiling(desiredWith / (double)width); j++) {
                        BitBlt(hdcDest, width * j, height * i, width, height,
                        hdcSrc, 0, 0, 0x00CC0020U);
                    }
                }
            } finally {
                if (hOldObject != IntPtr.Zero)
                    SelectObject(hdcSrc, hOldObject);
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
                if (hdcDest != IntPtr.Zero)
                    grDest.ReleaseHdc(hdcDest);
                if (hdcSrc != IntPtr.Zero)
                    grSrc.ReleaseHdc(hdcSrc);
            }
        }

    }



}
