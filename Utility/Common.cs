/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 Health121 Ltd.  All Rights Reserved.
//
// http://www.Health121.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace Health121.Utility.Win32.Common
{
	[Serializable, StructLayout( LayoutKind.Sequential )]
	public struct RECT
	{
		public RECT( Rectangle rect )
		{
			Left = rect.Left;
			Top = rect.Top;
			Right = rect.Right;
			Bottom = rect.Bottom;
		}

		public Rectangle Rect
		{
			get
			{
				return new Rectangle( Left, Top, Right - Left, Bottom - Top );
			}
		}

		public Point Location
		{
			get
			{
				return new Point( Left, Top );
			}
		}

		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	[StructLayout( LayoutKind.Sequential )]
	public struct POINT
	{

		public POINT( Int32 x, Int32 y )
		{
			X = x;
			Y = y;
		}

		public Int32 X;
		public Int32 Y;
	}


	[StructLayout( LayoutKind.Sequential )]
	public struct SIZE
	{
		public SIZE( Int32 cx, Int32 cy )
		{
			CX = cx;
			CY = cy;
		}

		public Int32 CX;
		public Int32 CY;
	}

    public class Helper 
    {
        public static int RGB(byte R, byte G, byte B) { return ((B * 256) + G) * 256 + R; }
        public static int ARGB(byte A, byte R, byte G, byte B) { return (((B * 256) + G) * 256 + R) * 256 + A; }

        public static byte RGBRed(int colrRGB) { return (byte)(colrRGB & 0x000000FF); }
        public static byte RGBGreen(int colrRGB) { return (byte)((colrRGB >> 8) & 0x000000FF); }
        public static byte RGBBlue(int colrRGB) { return (byte)((colrRGB >> 16) & 0x000000FF); }
        public static int LoWrd(uint value) { return (int)(value & 0xFFFF); }
        public static int HiWrd(uint value) { return (int)(value >> 16); }
    }
}
