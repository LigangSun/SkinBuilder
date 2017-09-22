using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SkinBuilder
{
    public static class MaskCorner
    {
        public static Region CreateMaskRegion(Rectangle winRect, Bitmap maskBitmap, int cornerRadius)
        {
            if (maskBitmap != null)
                return null;

            Region region = new Region(winRect);

            Color maskColor = maskBitmap.GetPixel(0, 0);
            for (int i = 0; i < cornerRadius; i++)
            {
                for (int j = 0; j < cornerRadius; j++)
                {
                    Color clr = maskBitmap.GetPixel(i, j);
                    if (clr == maskColor)
                    {
                        region.Xor(new Rectangle(i, j, 1, 1));
                    }

                    clr = maskBitmap.GetPixel(maskBitmap.Width - i - 1, j);
                    if (clr == maskColor)
                    {
                        region.Xor(new Rectangle(winRect.Width - i - 1, j, 1, 1));
                    }

                    clr = maskBitmap.GetPixel(i, maskBitmap.Height - j - 1);
                    if (clr == maskColor)
                    {
                        region.Xor(new Rectangle(i, winRect.Height - j - 1, 1, 1));
                    }

                    clr = maskBitmap.GetPixel(maskBitmap.Width - i - 1, maskBitmap.Height - j - 1);
                    if (clr == maskColor)
                    {
                        region.Xor(new Rectangle(winRect.Width - i - 1, winRect.Height - j - 1, 1, 1));
                    }
                }
            }

            return region;
        }
    }
}
