using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Health121.Utility.Win32
{
	[CLSCompliant( false )]
	public static class User
	{
		[Flags]
		public enum DrawTextFlags : uint
		{
			DT_TOP = 0x0,
			DT_LEFT = 0x0,
			DT_CENTER = 0x1,
			DT_RIGHT = 0x2,
			DT_VCENTER = 0x4,
			DT_BOTTOM = 0x8,
			DT_WORDBREAK = 0x10,
			DT_SINGLELINE = 0x20,
			DT_EXPANDTABS = 0x40,
			DT_TABSTOP = 0x80,
			DT_NOCLIP = 0x100,
			DT_EXTERNALLEADING = 0x200,
			DT_CALCRECT = 0x400,
			DT_NOPREFIX = 0x800,
			DT_INTERNAL = 0x1000,
			DT_EDITCONTROL = 0x2000,
			DT_PATH_ELLIPSIS = 0x4000,
			DT_END_ELLIPSIS = 0x8000,
			DT_MODIFYSTRING = 0x10000,
			DT_RTLREADING = 0x20000,
			DT_WORD_ELLIPSIS = 0x40000,
			DT_NOFULLWIDTHCHARBREAK = 0x80000,
			DT_HIDEPREFIX = 0x100000,
			DT_PREFIXONLY = 0x200000
		}

        public enum WindowsLongType : int
        {
            GWL_WNDPROC      =   -4,
            GWL_HINSTANCE    =   -6,
            GWL_HWNDPARENT   =   -8,
            GWL_STYLE        =   -16,
            GWL_EXSTYLE      =   -20,
            GWL_USERDATA     =   -21,
            GWL_ID           =   -12,

            DWL_MSGRESULT    =   0,
            DWL_DLGPROC      =   4,
            DWL_USER         =   8,
        }

		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public extern static bool DestroyIcon( IntPtr handle );

		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public extern static int DrawText( IntPtr hDC, string lpString, int nCount, ref Common.RECT lpRect, DrawTextFlags uFormat );

		[DllImport( "user32.dll" )]
		public static extern int DrawTextEx( IntPtr hdc, StringBuilder lpchText, int cchText, ref Common.RECT lprc, Win32.User.DrawTextFlags dwDTFormat, ref DRAWTEXTPARAMS lpDTParams );
		[DllImport( "user32.dll" )]
		public static extern int DrawTextEx( IntPtr hdc, StringBuilder lpchText, int cchText, ref Common.RECT lprc, Win32.User.DrawTextFlags dwDTFormat, IntPtr lpDTParams );

		[DllImport( "user32.dll" )]
		public extern static IntPtr SetActiveWindow( IntPtr handle );

		[DllImport( "user32.dll" )]
		public static extern uint RegisterWindowMessage( string lpString );

		[DllImport( "user32.dll" )]
        public static extern int SendMessage(IntPtr hwnd, Messages wMsg, Int32 wParam, Int32 lParam);

		[DllImport( "user32.dll", ExactSpelling = true, SetLastError = true )]
		public static extern bool UpdateLayeredWindow( IntPtr hwnd, IntPtr hdcDst, ref Common.POINT pptDst, ref Common.SIZE psize, IntPtr hdcSrc, ref Common.POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags );

		[DllImport( "user32.dll", ExactSpelling = true, SetLastError = true )]
		public static extern IntPtr GetDC( IntPtr hWnd );

		[DllImport( "user32.dll" )]
		public static extern void DisableProcessWindowsGhosting();

		[DllImport( "user32.dll" )]
		public static extern IntPtr GetDCEx( IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions );

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC( IntPtr hwnd );

		[DllImport( "user32.dll" )]
		public static extern Int32 SetWindowPos( IntPtr hWnd, IntPtr hWndAfter, Int32 x, Int32 y, Int32 cx, Int32 cy, UInt32 uFlags );

		[DllImport( "user32.dll" )]
		public static extern bool RedrawWindow( IntPtr hWnd, IntPtr rectUpdate, IntPtr hrgnUpdate, RedrawWindowOptions flags );

		[DllImport( "user32.dll" )]
		public static extern bool RedrawWindow( IntPtr hWnd, ref Common.RECT rectUpdate, IntPtr hrgnUpdate, RedrawWindowOptions flags );

		[DllImport( "user32.dll" )]
		public static extern bool PeekMessage( ref System.Windows.Forms.Message msg, IntPtr hwnd, int msgMin, int msgMax, int remove );

		[DllImport( "user32.dll" )]
		public static extern bool SetLayeredWindowAttributes( IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags );

		[DllImport( "user32.dll", SetLastError = true )]
		public static extern bool PostMessage( IntPtr hWnd, Utility.Win32.Messages Msg, IntPtr wParam, IntPtr lParam );

		[DllImport( "user32.dll" )]
		public static extern uint SendInput( uint nInputs, INPUT[] pInputs, int cbSize );

		[DllImport( "user32.dll" )]
		public static extern IntPtr GetMessageExtraInfo();

		[DllImport( "user32.dll" )]
		public static extern bool SetWindowPos( IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosOptions uFlags );

		[DllImport( "user32.dll" )]
        public static extern ShowWindowOptions ShowWindow(IntPtr hWnd, ShowWindowOptions nCmdShow);

		[DllImport( "user32.dll" )]
		public static extern bool GetWindowPlacement( IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl );

		[DllImport( "user32.dll" )]
		public static extern bool GetClientRect( IntPtr hWnd, out Common.RECT lpRect );

		[DllImport( "user32.dll" )]
		public static extern bool GetWindowRect( IntPtr hWnd, out Common.RECT lpRect );

		[DllImport( "user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi )]
		public static extern IntPtr GetFocus();

        [DllImport( "user32.dll" )]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, WindowsLongType index, IntPtr newLong);

        [DllImport( "user32.dll" )]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, WindowsLongType index);

        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

	}
}
