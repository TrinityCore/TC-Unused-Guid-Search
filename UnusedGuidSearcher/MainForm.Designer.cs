namespace UnusedGuidSearcher
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TableComboBox = new System.Windows.Forms.ComboBox();
            this.RandomRadio = new System.Windows.Forms.RadioButton();
            this.ConsecutiveRadio = new System.Windows.Forms.RadioButton();
            this.RadioButtonsPanel = new System.Windows.Forms.Panel();
            this.GuidCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.GoButton = new System.Windows.Forms.Button();
            this.RadioButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GuidCountUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // TableComboBox
            // 
            this.TableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TableComboBox.FormattingEnabled = true;
            this.TableComboBox.Location = new System.Drawing.Point(12, 12);
            this.TableComboBox.Name = "TableComboBox";
            this.TableComboBox.Size = new System.Drawing.Size(187, 21);
            this.TableComboBox.TabIndex = 0;
            // 
            // RandomRadio
            // 
            this.RandomRadio.AutoSize = true;
            this.RandomRadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RandomRadio.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RandomRadio.Location = new System.Drawing.Point(3, 3);
            this.RandomRadio.Name = "RandomRadio";
            this.RandomRadio.Size = new System.Drawing.Size(64, 17);
            this.RandomRadio.TabIndex = 1;
            this.RandomRadio.TabStop = true;
            this.RandomRadio.Text = "Random";
            this.RandomRadio.UseVisualStyleBackColor = true;
            // 
            // ConsecutiveRadio
            // 
            this.ConsecutiveRadio.AutoSize = true;
            this.ConsecutiveRadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConsecutiveRadio.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ConsecutiveRadio.Location = new System.Drawing.Point(3, 26);
            this.ConsecutiveRadio.Name = "ConsecutiveRadio";
            this.ConsecutiveRadio.Size = new System.Drawing.Size(83, 17);
            this.ConsecutiveRadio.TabIndex = 2;
            this.ConsecutiveRadio.Text = "Consecutive";
            this.ConsecutiveRadio.UseVisualStyleBackColor = true;
            // 
            // RadioButtonsPanel
            // 
            this.RadioButtonsPanel.Controls.Add(this.RandomRadio);
            this.RadioButtonsPanel.Controls.Add(this.ConsecutiveRadio);
            this.RadioButtonsPanel.Location = new System.Drawing.Point(12, 51);
            this.RadioButtonsPanel.Name = "RadioButtonsPanel";
            this.RadioButtonsPanel.Size = new System.Drawing.Size(92, 48);
            this.RadioButtonsPanel.TabIndex = 5;
            // 
            // GuidCountUpDown
            // 
            this.GuidCountUpDown.Location = new System.Drawing.Point(110, 51);
            this.GuidCountUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.GuidCountUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GuidCountUpDown.Name = "GuidCountUpDown";
            this.GuidCountUpDown.Size = new System.Drawing.Size(89, 20);
            this.GuidCountUpDown.TabIndex = 3;
            this.GuidCountUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(110, 78);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(89, 23);
            this.GoButton.TabIndex = 6;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButtonClick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.GoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(211, 113);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.GuidCountUpDown);
            this.Controls.Add(this.RadioButtonsPanel);
            this.Controls.Add(this.TableComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "TC Unused Guid Search";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.RadioButtonsPanel.ResumeLayout(false);
            this.RadioButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GuidCountUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TableComboBox;
        private System.Windows.Forms.RadioButton RandomRadio;
        private System.Windows.Forms.RadioButton ConsecutiveRadio;
        private System.Windows.Forms.Panel RadioButtonsPanel;
        private System.Windows.Forms.NumericUpDown GuidCountUpDown;
        private System.Windows.Forms.Button GoButton;

    }
}

