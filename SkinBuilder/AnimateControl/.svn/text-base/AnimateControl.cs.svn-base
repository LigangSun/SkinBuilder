using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ZLIS.SkinBuilder
{
    public partial class AnimateControl : Control
    {
        private List<AnimateFrame> frameList = new List<AnimateFrame>();
        private int frameIndex = 0;

        public List<AnimateFrame> FrameList
        {
            get { return this.frameList; }
        }

        public AnimateControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
        }

        public virtual void Start()
        {
            if (this.frameList.Count == 0)
                return;

            if (this.animateThread.IsBusy)
                return;

            this.frameIndex = 0;
            this.animateThread.RunWorkerAsync();
        }

        public virtual void Stop()
        {
            if (this.animateThread.IsBusy)
            {
                this.animateThread.CancelAsync();
                Thread.Sleep(10);
                this.frameIndex = 0;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.InvokePaintBackground(this, e);

            if (this.frameList.Count == 0)
            {
                base.OnPaint(e);
                return;
            }

            if (this.frameIndex >= this.frameList.Count)
                return;

            this.DrawFrame(e.Graphics, this.frameList[this.frameIndex]);
        }

        protected virtual void DrawFrame(Graphics g, AnimateFrame frame)
        {
            if (frame.Image == null)
                return;

            g.DrawImage(frame.Image, this.ClientRectangle, 0, 0, frame.Image.Width, frame.Image.Height, GraphicsUnit.Pixel);
        }

        private void animateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (!worker.CancellationPending)
            {
                AnimateFrame frame = this.frameList[this.frameIndex];
                if (frame != null)
                {
                    for (int i = 0; i < frame.MilliSecond / 10 + 1; i++)
                    {
                        if (worker.CancellationPending)
                            break;

                        Thread.Sleep(10);
                    }

                    worker.ReportProgress(0);
                }

                this.frameIndex++;
                if (this.frameIndex >= this.frameList.Count)
                    this.frameIndex = 0;

                Thread.Sleep(1);
            }
        }

        private void animateThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invalidate();
        }
    }

    public class AnimateFrame
    {
        private Image image = null;
        private int ms = 100;

        public Image Image 
        {
            set { this.image = value; }
            get { return this.image; }
        }

        public int MilliSecond
        {
            set { this.ms = value; }
            get { return this.ms; }
        }

        public AnimateFrame(Image image, int ms)
        {
            this.image = image;
            this.ms = ms;
        }
    }
}
