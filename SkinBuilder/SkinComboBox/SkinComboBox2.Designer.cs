namespace ZLIS.SkinBuilder
{
    partial class SkinComboBox2
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
            this.textBox = new ZLIS.SkinBuilder.SkinTextBox();
            this.textPaanel = new System.Windows.Forms.Panel();
            this.textPaanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.ActiveColor = System.Drawing.Color.White;
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderColor = System.Drawing.Color.Black;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Margin = new System.Windows.Forms.Padding(0);
            this.textBox.Multiline = false;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(132, 14);
            this.textBox.TabIndex = 0;
            this.textBox.UseSystemPasswordChar = false;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.BackColorChanged += new System.EventHandler(this.textBox_BackColorChanged);
            // 
            // textPaanel
            // 
            this.textPaanel.BackColor = System.Drawing.Color.Transparent;
            this.textPaanel.Controls.Add(this.textBox);
            this.textPaanel.Location = new System.Drawing.Point(3, 3);
            this.textPaanel.Name = "textPaanel";
            this.textPaanel.Size = new System.Drawing.Size(220, 24);
            this.textPaanel.TabIndex = 1;
            // 
            // SkinComboBox2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textPaanel);
            this.Name = "SkinComboBox2";
            this.Size = new System.Drawing.Size(320, 191);
            this.textPaanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SkinTextBox textBox;
        private System.Windows.Forms.Panel textPaanel;
    }
}
