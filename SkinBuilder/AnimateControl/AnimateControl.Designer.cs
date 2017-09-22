namespace ZLIS.SkinBuilder
{
    partial class AnimateControl
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
            this.animateThread = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // animateThread
            // 
            this.animateThread.WorkerReportsProgress = true;
            this.animateThread.WorkerSupportsCancellation = true;
            this.animateThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.animateThread_DoWork);
            this.animateThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.animateThread_ProgressChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker animateThread;
    }
}
