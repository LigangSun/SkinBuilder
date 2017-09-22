using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZLIS.SkinBuilder
{
    public delegate void DrawingEventHandler(Object sender, Graphics g, Rectangle drawRect);

    interface ISkinBase
    {
        ControlState State();
        Bitmap GetBitmap(ControlState state);
        ImageObject GetImageObject();
    }
}
