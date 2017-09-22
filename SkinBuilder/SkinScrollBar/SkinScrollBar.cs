using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public enum ScrollBarType
    {
        Vertical,
        Horizontal
    }

    public partial class SkinScrollBar : Control
    {
        #region Enums

        enum PtInType
        {
            None,

            Btn1,
            Btn2,
            Thumb,
            BK
        }

        #endregion

        #region Members

        private ImageObject btn1ImgObj = new ImageObject();
        private ImageObject btn2ImgObj = new ImageObject();
        private ImageObject thumbImgObj = new ImageObject();
        private ImageObject bkImgObj = new ImageObject();

        private ControlState btn1State = ControlState.Normal;
        private ControlState btn2State = ControlState.Normal;
        private ControlState thumbState = ControlState.Normal;
        private ControlState bkState = ControlState.Normal;

        private bool mouseDown = false;

        private Point lastPoint = Point.Empty;

        private int scrollValue = 0;

        #endregion

        #region Properties

        public ImageObject Btn1ImgObj
        {
            get { return this.btn1ImgObj; }
            set { this.btn1ImgObj = value; }
        }

        public ImageObject Btn2ImgObj
        {
            get { return this.btn2ImgObj; }
            set { this.btn2ImgObj = value; }
        }

        public ImageObject ThumbImgObj
        {
            get { return this.thumbImgObj; }
            set { this.thumbImgObj = value; }
        }

        public ImageObject BkImgObj
        {
            get { return this.bkImgObj; }
            set { this.bkImgObj = value; }
        }

        public ScrollBarType ScrollBarType
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public SkinScrollBar()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.ResizeRedraw | 
                ControlStyles.UserMouse, true);
        }

        #endregion

        #region Mouse Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.ResetState();

            PtInType ptInType = this.PtInControlPart(new Point(e.X, e.Y));
            switch (ptInType)
            {
                case PtInType.Btn1:
                    this.btn1State = ControlState.Down;
                    break;
                case PtInType.Btn2:
                    this.btn2State = ControlState.Down;
                    break;
                case PtInType.Thumb:
                    this.thumbState = ControlState.Down;
                    break;
                case PtInType.BK:
                    this.bkState = ControlState.Down;
                    break;
            }

            this.lastPoint = new Point(e.X, e.Y);

            this.mouseDown = true;
            this.Capture = true;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            PtInType ptInType = this.PtInControlPart(new Point(e.X, e.Y));

            this.ResetState();

            ControlState state = ControlState.Highlight;
            if (this.mouseDown)
                state = ControlState.Down;

            switch (ptInType)
            {
                case PtInType.Btn1:
                    this.btn1State = state;
                    break;
                case PtInType.Btn2:
                    this.btn2State = state;
                    break;
                case PtInType.Thumb:
                    this.thumbState = state;
                    break;
                case PtInType.BK:
                    this.bkState = state;
                    break;
            }

            this.lastPoint = new Point(e.X, e.Y);

            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.ResetState();

            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.ResetState();

            PtInType ptInType = this.PtInControlPart(new Point(e.X, e.Y));
            switch (ptInType)
            {
                case PtInType.Btn1:
                    this.btn1State = ControlState.Highlight;
                    break;
                case PtInType.Btn2:
                    this.btn2State = ControlState.Highlight;
                    break;
                case PtInType.Thumb:
                    this.thumbState = ControlState.Highlight;
                    break;
                case PtInType.BK:
                    this.bkState = ControlState.Highlight;
                    break;
            }

            this.mouseDown = false;
            this.Capture = false;
            this.Invalidate();
        }

        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #endregion

        #region Help Functions

        private PtInType PtInControlPart(Point pt)
        {
            Rectangle btn1Rect = this.GetBtn1Rect();
            if (btn1Rect.Contains(pt))
                return PtInType.Btn1;

            Rectangle btn2Rect = this.GetBtn2Rect();
            if (btn2Rect.Contains(pt))
                return PtInType.Btn2;

            Rectangle thumbRect = this.GetThumbRect();
            if (thumbRect.Contains(pt))
                return PtInType.Thumb;

            Rectangle bkRect = this.GetBackgroundRect();
            if (bkRect.Contains(pt))
                return PtInType.BK;

            return PtInType.None;
        }

        private Rectangle GetBtn1Rect()
        {
            Rectangle btn1Rect = this.ClientRectangle;

            if (this.ScrollBarType == ScrollBarType.Vertical)   // |
            {
                btn1Rect.X = btn1Rect.Right - this.btn1ImgObj.Width;
            }
            else // -
            {
                btn1Rect.Y = btn1Rect.Bottom - this.btn1ImgObj.Height;
            }

            btn1Rect.Width = this.btn1ImgObj.Width;
            btn1Rect.Height = this.btn1ImgObj.Height;

            return btn1Rect;
        }

        private Rectangle GetBtn2Rect()
        {
            Rectangle btn2Rect = this.ClientRectangle;

            if (this.ScrollBarType == ScrollBarType.Vertical)   // |
            {
                btn2Rect.Y = btn2Rect.Bottom - this.btn2ImgObj.Height;
            }
            else // -
            {
                btn2Rect.X = btn2Rect.Right - this.btn2ImgObj.Width;
            }

            btn2Rect.Width = this.btn2ImgObj.Width;
            btn2Rect.Height = this.btn2ImgObj.Height;

            return btn2Rect;
        }

        private Rectangle GetThumbRect()
        {
            Rectangle thumbRect = this.ClientRectangle;

            if (this.ScrollBarType == ScrollBarType.Vertical)   // |
            {
                thumbRect.Y = this.GetBtn1Rect().Bottom;
                thumbRect.Height = this.thumbImgObj.Height;
            }
            else // -
            {
                thumbRect.X = thumbRect.Right;
                thumbRect.Width = this.thumbImgObj.Width;
            }

            return thumbRect;
        }

        private Rectangle GetBackgroundRect()
        {
            return this.ClientRectangle;
        }

        private void ResetState()
        {
            this.btn1State = ControlState.Normal;
            this.btn2State = ControlState.Normal;
            this.thumbState = ControlState.Normal;
            this.bkState = ControlState.Normal;
        }

        #endregion
    }
}
