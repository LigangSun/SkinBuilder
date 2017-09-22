namespace ZLIS.SkinBuilder
{
    partial class SkinCalendar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ZLIS.SkinBuilder.DrawTool drawTool1 = new ZLIS.SkinBuilder.DrawTool();
            this.dayView = new ZLIS.SkinBuilder.DayView();
            this.SuspendLayout();
            // 
            // dayView
            // 
            drawTool1.DayView = this.dayView;
            this.dayView.ActiveTool = drawTool1;
            this.dayView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dayView.Location = new System.Drawing.Point(0, 0);
            this.dayView.Name = "dayView";
            this.dayView.SelectionEnd = new System.DateTime(((long)(0)));
            this.dayView.SelectionStart = new System.DateTime(((long)(0)));
            this.dayView.Size = new System.Drawing.Size(383, 349);
            this.dayView.StartDate = new System.DateTime(((long)(0)));
            this.dayView.TabIndex = 0;
            this.dayView.Text = "dayView1";
            // 
            // SkinCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dayView);
            this.Name = "SkinCalendar";
            this.Size = new System.Drawing.Size(383, 349);
            this.ResumeLayout(false);

        }

        #endregion

        private ZLIS.SkinBuilder.DayView dayView;
    }
}
