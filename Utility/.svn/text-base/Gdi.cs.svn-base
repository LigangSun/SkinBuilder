/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 Health121 Ltd.  All Rights Reserved.
//
// http://www.Health121.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Health121.Utility.Win32
{
	public enum BackgroundMode : int
	{
		TRANSPARENT = 1,
		OPAQUE = 2
	}

	public enum TernaryRasterOperations
	{
		SRCCOPY = 0x00CC0020,
		SRCPAINT = 0x00EE0086,
		SRCAND = 0x008800C6,
		SRCINVERT = 0x00660046,
		SRCERASE = 0x00440328,
		NOTSRCCOPY = 0x00330008,
		NOTSRCERASE = 0x001100A6,
		MERGECOPY = 0x00C000CA,
		MERGEPAINT = 0x00BB0226,
		PATCOPY = 0x00F00021,
		PATPAINT = 0x00FB0A09,
		PATINVERT = 0x005A0049,
		DSTINVERT = 0x00550009,
		BLACKNESS = 0x00000042,
		WHITENESS = 0x00FF0062,
	};

	[Serializable, StructLayout( LayoutKind.Sequential )]
	public struct XFORM
	{
		public float eM11;
		public float eM12;
		public float eM21;
		public float eM22;
		public float eDx;
		public float eDy;
	}

	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	public struct BLENDFUNCTION
	{
		public byte BlendOp;
		public byte BlendFlags;
		public byte SourceConstantAlpha;
		public byte AlphaFormat;
	}

	[CLSCompliant( false )]
	[StructLayout( LayoutKind.Sequential )]
	public struct DRAWTEXTPARAMS
	{
		public uint cbSize;
		public int iTabLength;
		public int iLeftMargin;
		public int iRightMargin;
		public uint uiLengthDrawn;
	}

	[CLSCompliant( false )]
	public sealed class Gdi
	{
		[DllImport( "gdi32.dll" )]
		public extern static bool DeleteObject( IntPtr hObject );

		[DllImport( "gdi32.dll" )]
		public static extern int GetClipBox( IntPtr hdc, out Common.RECT lprc );

		[DllImport( "gdi32.dll" )]
		public static extern int GetClipRgn( IntPtr hdc, IntPtr hrgn );
		
		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static IntPtr SelectObject( IntPtr hdc, IntPtr hgdiobj );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static uint SetTextColor( IntPtr hdc, uint crColor );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static BackgroundMode SetBkMode( IntPtr hdc, BackgroundMode iBkMode );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static int SetWorldTransform( IntPtr hdc, ref XFORM lpXform );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static int GetWorldTransform( IntPtr hdc, ref XFORM lpXform );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static int SetGraphicsMode( IntPtr hdc, int iMode );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll", CharSet = CharSet.Auto )]
		public extern static IntPtr GetStockObject( int fnObject );

		[System.Runtime.InteropServices.DllImport( "gdi32.dll" )]
		public static extern int SelectClipRgn( IntPtr hdc, IntPtr hrgn );

		[DllImport( "gdi32.dll", ExactSpelling = true, SetLastError = true )]
		public static extern IntPtr CreateCompatibleDC( IntPtr hDC );

		[DllImport( "gdi32.dll", ExactSpelling = true, SetLastError = true )]
		public static extern bool DeleteDC( IntPtr hdc );

		[DllImport( "user32.dll", ExactSpelling = true )]
		public static extern int ReleaseDC( IntPtr hWnd, IntPtr hDC );

		[DllImport( "gdi32.dll" )]
		public static extern IntPtr CreateCompatibleBitmap( IntPtr hDC, int nWidth, int nHeight );

		[DllImport( "gdi32.dll" )]
		public static extern bool BitBlt( IntPtr hObject, int nXDest, int nYDest, int nWidth,
			 int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop );
	}
}
