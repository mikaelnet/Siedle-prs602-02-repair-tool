namespace Siedle.Prs602.RepairTool
{
    partial class SiedleRepairToolForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.OpenDatabaseDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExitButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.fixDescriptionTextsCheckbox = new System.Windows.Forms.CheckBox();
            this.sanitizeInputCheckbox = new System.Windows.Forms.CheckBox();
            this.findNumberingHolesCheckbox = new System.Windows.Forms.CheckBox();
            this.createMissingCardsCheckbox = new System.Windows.Forms.CheckBox();
            this.commitCheckbox = new System.Windows.Forms.CheckBox();
            this.testFlagsCheckbox = new System.Windows.Forms.CheckBox();
            this.project1TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.project1Button = new System.Windows.Forms.Button();
            this.project2Button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.project2TextBox = new System.Windows.Forms.TextBox();
            this.project3Button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.project3TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(504, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ensure the Siedle programming software is closed before running this tool!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpenDatabaseDialog
            // 
            this.OpenDatabaseDialog.Filter = "Siedle Database|*.xml";
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.Location = new System.Drawing.Point(441, 321);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenButton.Location = new System.Drawing.Point(15, 321);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 3;
            this.OpenButton.Text = "Execute";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextBox.Location = new System.Drawing.Point(15, 89);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(501, 180);
            this.LogTextBox.TabIndex = 4;
            // 
            // fixDescriptionTextsCheckbox
            // 
            this.fixDescriptionTextsCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fixDescriptionTextsCheckbox.AutoSize = true;
            this.fixDescriptionTextsCheckbox.Checked = true;
            this.fixDescriptionTextsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fixDescriptionTextsCheckbox.Location = new System.Drawing.Point(15, 298);
            this.fixDescriptionTextsCheckbox.Name = "fixDescriptionTextsCheckbox";
            this.fixDescriptionTextsCheckbox.Size = new System.Drawing.Size(118, 17);
            this.fixDescriptionTextsCheckbox.TabIndex = 5;
            this.fixDescriptionTextsCheckbox.Text = "Fix description texts";
            this.fixDescriptionTextsCheckbox.UseVisualStyleBackColor = true;
            // 
            // trimTextsCheckbox
            // 
            this.sanitizeInputCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sanitizeInputCheckbox.AutoSize = true;
            this.sanitizeInputCheckbox.Checked = true;
            this.sanitizeInputCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sanitizeInputCheckbox.Location = new System.Drawing.Point(15, 275);
            this.sanitizeInputCheckbox.Name = "sanitizeInputCheckbox";
            this.sanitizeInputCheckbox.Size = new System.Drawing.Size(89, 17);
            this.sanitizeInputCheckbox.TabIndex = 6;
            this.sanitizeInputCheckbox.Text = "Sanitize input";
            this.sanitizeInputCheckbox.UseVisualStyleBackColor = true;
            // 
            // findNumberingHolesCheckbox
            // 
            this.findNumberingHolesCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.findNumberingHolesCheckbox.AutoSize = true;
            this.findNumberingHolesCheckbox.Checked = true;
            this.findNumberingHolesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.findNumberingHolesCheckbox.Location = new System.Drawing.Point(151, 298);
            this.findNumberingHolesCheckbox.Name = "findNumberingHolesCheckbox";
            this.findNumberingHolesCheckbox.Size = new System.Drawing.Size(126, 17);
            this.findNumberingHolesCheckbox.TabIndex = 7;
            this.findNumberingHolesCheckbox.Text = "Find numbering holes";
            this.findNumberingHolesCheckbox.UseVisualStyleBackColor = true;
            // 
            // createMissingCardsCheckbox
            // 
            this.createMissingCardsCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createMissingCardsCheckbox.AutoSize = true;
            this.createMissingCardsCheckbox.Checked = true;
            this.createMissingCardsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createMissingCardsCheckbox.Location = new System.Drawing.Point(151, 276);
            this.createMissingCardsCheckbox.Name = "createMissingCardsCheckbox";
            this.createMissingCardsCheckbox.Size = new System.Drawing.Size(123, 17);
            this.createMissingCardsCheckbox.TabIndex = 8;
            this.createMissingCardsCheckbox.Text = "Create missing cards";
            this.createMissingCardsCheckbox.UseVisualStyleBackColor = true;
            // 
            // commitCheckbox
            // 
            this.commitCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.commitCheckbox.AutoSize = true;
            this.commitCheckbox.Location = new System.Drawing.Point(284, 298);
            this.commitCheckbox.Name = "commitCheckbox";
            this.commitCheckbox.Size = new System.Drawing.Size(104, 17);
            this.commitCheckbox.TabIndex = 9;
            this.commitCheckbox.Text = "Commit changes";
            this.commitCheckbox.UseVisualStyleBackColor = true;
            // 
            // testFlagsCheckbox
            // 
            this.testFlagsCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.testFlagsCheckbox.AutoSize = true;
            this.testFlagsCheckbox.Checked = true;
            this.testFlagsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.testFlagsCheckbox.Location = new System.Drawing.Point(284, 275);
            this.testFlagsCheckbox.Name = "testFlagsCheckbox";
            this.testFlagsCheckbox.Size = new System.Drawing.Size(72, 17);
            this.testFlagsCheckbox.TabIndex = 10;
            this.testFlagsCheckbox.Text = "Test flags";
            this.testFlagsCheckbox.UseVisualStyleBackColor = true;
            // 
            // project1TextBox
            // 
            this.project1TextBox.Location = new System.Drawing.Point(15, 63);
            this.project1TextBox.Name = "project1TextBox";
            this.project1TextBox.Size = new System.Drawing.Size(100, 20);
            this.project1TextBox.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Project 1";
            // 
            // project1Button
            // 
            this.project1Button.Location = new System.Drawing.Point(122, 63);
            this.project1Button.Name = "project1Button";
            this.project1Button.Size = new System.Drawing.Size(25, 20);
            this.project1Button.TabIndex = 13;
            this.project1Button.Text = "...";
            this.project1Button.UseVisualStyleBackColor = true;
            this.project1Button.Click += new System.EventHandler(this.project1Button_Click);
            // 
            // project2Button
            // 
            this.project2Button.Location = new System.Drawing.Point(281, 63);
            this.project2Button.Name = "project2Button";
            this.project2Button.Size = new System.Drawing.Size(25, 20);
            this.project2Button.TabIndex = 16;
            this.project2Button.Text = "...";
            this.project2Button.UseVisualStyleBackColor = true;
            this.project2Button.Click += new System.EventHandler(this.project2Button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Project 2";
            // 
            // project2TextBox
            // 
            this.project2TextBox.Location = new System.Drawing.Point(174, 63);
            this.project2TextBox.Name = "project2TextBox";
            this.project2TextBox.Size = new System.Drawing.Size(100, 20);
            this.project2TextBox.TabIndex = 14;
            // 
            // project3Button
            // 
            this.project3Button.Location = new System.Drawing.Point(439, 63);
            this.project3Button.Name = "project3Button";
            this.project3Button.Size = new System.Drawing.Size(25, 20);
            this.project3Button.TabIndex = 19;
            this.project3Button.Text = "...";
            this.project3Button.UseVisualStyleBackColor = true;
            this.project3Button.Click += new System.EventHandler(this.project3Button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Project 1";
            // 
            // project3TextBox
            // 
            this.project3TextBox.Location = new System.Drawing.Point(332, 63);
            this.project3TextBox.Name = "project3TextBox";
            this.project3TextBox.Size = new System.Drawing.Size(100, 20);
            this.project3TextBox.TabIndex = 17;
            // 
            // SiedleRepairToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 356);
            this.Controls.Add(this.project3Button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.project3TextBox);
            this.Controls.Add(this.project2Button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.project2TextBox);
            this.Controls.Add(this.project1Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.project1TextBox);
            this.Controls.Add(this.testFlagsCheckbox);
            this.Controls.Add(this.commitCheckbox);
            this.Controls.Add(this.createMissingCardsCheckbox);
            this.Controls.Add(this.findNumberingHolesCheckbox);
            this.Controls.Add(this.sanitizeInputCheckbox);
            this.Controls.Add(this.fixDescriptionTextsCheckbox);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.label1);
            this.Name = "SiedleRepairToolForm";
            this.Text = "Siedle PRS602-01 Repair tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OpenDatabaseDialog;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.CheckBox fixDescriptionTextsCheckbox;
        private System.Windows.Forms.CheckBox sanitizeInputCheckbox;
        private System.Windows.Forms.CheckBox findNumberingHolesCheckbox;
        private System.Windows.Forms.CheckBox createMissingCardsCheckbox;
        private System.Windows.Forms.CheckBox commitCheckbox;
        private System.Windows.Forms.CheckBox testFlagsCheckbox;
        private System.Windows.Forms.TextBox project1TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button project1Button;
        private System.Windows.Forms.Button project2Button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox project2TextBox;
        private System.Windows.Forms.Button project3Button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox project3TextBox;
    }
}

