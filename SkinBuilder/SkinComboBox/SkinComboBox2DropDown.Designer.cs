namespace ZLIS.SkinBuilder
{
    partial class SkinComboBox2DropDown
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
            this.comboBox2ListBox = new ZLIS.SkinBuilder.ComboBox2ListBox();
            this.SuspendLayout();
            // 
            // comboBox2ListBox
            // 
            this.comboBox2ListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBox2ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox2ListBox.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2ListBox.ItemHeight = 24;
            this.comboBox2ListBox.Location = new System.Drawing.Point(0, 0);
            this.comboBox2ListBox.Name = "comboBox2ListBox";
            this.comboBox2ListBox.Size = new System.Drawing.Size(246, 303);
            this.comboBox2ListBox.TabIndex = 0;
            // 
            // SkinComboBox2DropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox2ListBox);
            this.Name = "SkinComboBox2DropDown";
            this.Size = new System.Drawing.Size(246, 303);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox2ListBox comboBox2ListBox;

    }
}
