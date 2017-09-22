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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Health121.WinFormsUtility.Drawing
{
	public static class GdiPlusEx
	{
		/// <summary>
		/// Alignment of the column text within the column header.
		/// </summary>
		public enum Alignment
		{
			Left,
			Center,
			Right
		}

		public enum TextSplitting
		{
			SingleLineEllipsis,
			MultiLine
		}

		public enum Ampersands
		{
			Display,
			MakeShortcut
		}

		public static void DrawString( Graphics g, string text, Font font, System.Drawing.Color color, Rectangle rect, TextSplitting textSplitting, Ampersands ampersands )
		{
			DrawString( g, text, font, color, rect, Alignment.Left, textSplitting, ampersands );
		}

		public static void DrawString( Graphics g, string text, Font font, System.Drawing.Color color, Rectangle rect, Alignment alignment, TextSplitting textSplitting, Ampersands ampersands )
		{
			if( g == null )
			{
				throw new ArgumentNullException( "g" );
			}
			if( text == null )
			{
				throw new ArgumentNullException( "text" );
			}
			if( font == null )
			{
				throw new ArgumentNullException( "font" );
			}

			if( ampersands == Ampersands.Display )
			{
				text = text.Replace( "&", "&&" );
			}

			float[] txValues = g.Transform.Elements;
			IntPtr hClipRgn = g.Clip.GetHrgn( g );
			IntPtr hDC = g.GetHdc();

			Utility.Win32.Gdi.SelectClipRgn( hDC, hClipRgn );

			int oldGraphicsMode = Utility.Win32.Gdi.SetGraphicsMode( hDC, 2 );
			Utility.Win32.XFORM oldXForm = new Utility.Win32.XFORM();

			Utility.Win32.Gdi.GetWorldTransform( hDC, ref oldXForm );

			Utility.Win32.XFORM newXForm = new Utility.Win32.XFORM();

			newXForm.eM11 = txValues[0];
			newXForm.eM12 = txValues[1];
			newXForm.eM21 = txValues[2];
			newXForm.eM22 = txValues[3];
			newXForm.eDx = txValues[4];
			newXForm.eDy = txValues[5];

			Utility.Win32.Gdi.SetWorldTransform( hDC, ref newXForm );

			try
			{
				IntPtr hFont = font.ToHfont();
				IntPtr hOldFont = Utility.Win32.Gdi.SelectObject( hDC, hFont );

				try
				{
					Utility.Win32.Common.RECT r = new Utility.Win32.Common.RECT( rect );
					Utility.Win32.User.DrawTextFlags uFormat;

					switch( textSplitting )
					{
						case TextSplitting.SingleLineEllipsis:
							uFormat
								= Utility.Win32.User.DrawTextFlags.DT_WORD_ELLIPSIS
								| Utility.Win32.User.DrawTextFlags.DT_END_ELLIPSIS;
							break;
						case TextSplitting.MultiLine:
							uFormat
								= Utility.Win32.User.DrawTextFlags.DT_WORDBREAK;
							break;
						default:
							throw new InvalidOperationException();
					}

					switch( alignment )
					{
						case Alignment.Left:
							break;
						case Alignment.Center:
							uFormat
								= Utility.Win32.User.DrawTextFlags.DT_CENTER;
							break;
						case Alignment.Right:
							uFormat
								= Utility.Win32.User.DrawTextFlags.DT_RIGHT;
							break;
						default:
							throw new InvalidOperationException();
					}

					uint bgr = (uint) ((color.B << 16) | (color.G << 8) | (color.R));
					uint oldColor = Utility.Win32.Gdi.SetTextColor( hDC, bgr );

					try
					{
						Utility.Win32.BackgroundMode oldBackgroundMode = Utility.Win32.Gdi.SetBkMode( hDC, Utility.Win32.BackgroundMode.TRANSPARENT );

						try
						{
							Utility.Win32.User.DrawText( hDC, text, text.Length, ref r, uFormat );
						}
						finally
						{
							Utility.Win32.Gdi.SetBkMode( hDC, oldBackgroundMode );
						}
					}
					finally
					{
						Utility.Win32.Gdi.SetTextColor( hDC, oldColor );
					}
				}
				finally
				{
					Utility.Win32.Gdi.SelectObject( hDC, hOldFont );
					Utility.Win32.Gdi.DeleteObject( hFont );
				}
			}
			finally
			{
				if( oldGraphicsMode == 1 )
				{
					oldXForm.eM11 = 1;
					oldXForm.eM12 = 0;
					oldXForm.eM21 = 0;
					oldXForm.eM22 = 1;
					oldXForm.eDx = 0;
					oldXForm.eDx = 0;
				}

				Utility.Win32.Gdi.SetWorldTransform( hDC, ref oldXForm );
				Utility.Win32.Gdi.SetGraphicsMode( hDC, oldGraphicsMode );

				g.ReleaseHdc( hDC );

				if( hClipRgn != IntPtr.Zero )
				{
					g.Clip.ReleaseHrgn( hClipRgn );
				}
			}
		}

		public static Size MeasureString( Graphics g, string text, Font font, int width )
		{
			Size size;
			TextDetails td = new TextDetails( text, font, width );

			if( _mapTextSizes.TryGetValue( td, out size ) )
			{
				return size;
			}

			IntPtr hDC = g.GetHdc();

			try
			{
				IntPtr hFont = font.ToHfont();

				try
				{
					IntPtr hOldFont = Utility.Win32.Gdi.SelectObject( hDC, hFont );

					try
					{
						Rectangle rect = new Rectangle( 0, 0, width, 0 );
						Utility.Win32.Common.RECT r = new Utility.Win32.Common.RECT( rect );
						Utility.Win32.User.DrawTextFlags uFormat = Utility.Win32.User.DrawTextFlags.DT_WORDBREAK | Utility.Win32.User.DrawTextFlags.DT_CALCRECT;

						Utility.Win32.User.DrawText( hDC, text, text.Length, ref r, uFormat );

						size = new Size( r.Right, r.Bottom );

						_mapTextSizes[td] = size;

						return size;
					}
					finally
					{
						Utility.Win32.Gdi.SelectObject( hDC, hOldFont );
					}
				}
				finally
				{
					Utility.Win32.Gdi.DeleteObject( hFont );
				}
			}
			finally
			{
				g.ReleaseHdc( hDC );
			}
		}

		public static void DrawRoundRect( Graphics g, Pen p, Rectangle rect, int radius )
		{
			DrawRoundRect( g, p, rect.X, rect.Y, rect.Width, rect.Height, radius );
		}

		public static void DrawRoundRect( Graphics g, Pen p, int x, int y, int width, int height, int radius )
		{
			if( width <= 0 || height <= 0 )
			{
				return;
			}

			radius = Math.Min( radius, height / 2 - 1 );
			radius = Math.Min( radius, width / 2 - 1 );

			if( radius <= 0 )
			{
				g.DrawRectangle( p, x, y, width, height );
				return;
			}

			using( GraphicsPath gp = new GraphicsPath() )
			{
				gp.AddLine( x + radius, y, x + width - (radius * 2), y );
				gp.AddArc( x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90 );
				gp.AddLine( x + width, y + radius, x + width, y + height - (radius * 2) );
				gp.AddArc( x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90 );
				gp.AddLine( x + width - (radius * 2), y + height, x + radius, y + height );
				gp.AddArc( x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90 );
				gp.AddLine( x, y + height - (radius * 2), x, y + radius );
				gp.AddArc( x, y, radius * 2, radius * 2, 180, 90 );
				gp.CloseFigure();

				g.DrawPath( p, gp );
			}
		}

		public static void FillRoundRect( Graphics g, Brush b, Rectangle rect, int radius )
		{
			FillRoundRect( g, b, rect.X, rect.Y, rect.Width, rect.Height, radius );
		}

		public static void FillRoundRect( Graphics g, Brush b, int x, int y, int width, int height, int radius )
		{
			if( width <= 0 || height <= 0 )
			{
				return;
			}

			radius = Math.Min( radius, height / 2 );
			radius = Math.Min( radius, width / 2 );

			if( radius == 0 )
			{
				g.FillRectangle( b, x, y, width, height );
				return;
			}

			using( GraphicsPath gp = new GraphicsPath() )
			{
				gp.AddLine( x + radius, y, x + width - (radius * 2), y );
				gp.AddArc( x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90 );
				gp.AddLine( x + width, y + radius, x + width, y + height - (radius * 2) );
				gp.AddArc( x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90 );
				gp.AddLine( x + width - (radius * 2), y + height, x + radius, y + height );
				gp.AddArc( x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90 );
				gp.AddLine( x, y + height - (radius * 2), x, y + radius );
				gp.AddArc( x, y, radius * 2, radius * 2, 180, 90 );
				gp.CloseFigure();

				g.FillPath( b, gp );
			}
		}

		public static IDisposable SaveState( Graphics g )
		{
			return new GraphicsStateDisposer( g );
		}

		#region GraphicsStateDisposer

		private sealed class GraphicsStateDisposer : IDisposable
		{
			internal GraphicsStateDisposer( Graphics g )
			{
				_g = g;
				_state = _g.Save();
			}

			#region IDisposable Members

			public void Dispose()
			{
				if( _g != null )
				{
					_g.Restore( _state );
					_g = null;
					_state = null;
				}
			}

			#endregion

			private Graphics _g;
			private GraphicsState _state;
		}

		#endregion

		#region TextDetails

		private sealed class TextDetails
		{
			internal TextDetails( string text, Font font, int width )
			{
				_text = text;
				_font = font;
				_width = width;
			}

			public override int GetHashCode()
			{
				return _text.GetHashCode() ^ _font.GetHashCode() ^ _width;
			}

			public override bool Equals( object obj )
			{
				TextDetails td = obj as TextDetails;

				if( td == null )
				{
					return false;
				}

				return _text == td._text && _font.Equals( td._font ) && _width == td._width;
			}

			private string _text;
			private Font _font;
			private int _width;
		}

		#endregion

		private static Dictionary<TextDetails, Size> _mapTextSizes = new Dictionary<TextDetails, Size>();
	}
}
