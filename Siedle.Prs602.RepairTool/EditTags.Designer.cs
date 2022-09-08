namespace Siedle.Prs602.RepairTool
{
    partial class EditTags
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
            this.components = new System.ComponentModel.Container();
            this.tagsGridView = new System.Windows.Forms.DataGridView();
            this.saveButton = new System.Windows.Forms.Button();
            this.cardContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aktiveraAllaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makuleraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tagsGridView)).BeginInit();
            this.cardContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagsGridView
            // 
            this.tagsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tagsGridView.Location = new System.Drawing.Point(12, 12);
            this.tagsGridView.Name = "tagsGridView";
            this.tagsGridView.Size = new System.Drawing.Size(776, 427);
            this.tagsGridView.TabIndex = 2;
            this.tagsGridView.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.tagsGridView_CellContextMenuStripNeeded);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveButton.Location = new System.Drawing.Point(12, 446);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cardContextMenuStrip
            // 
            this.cardContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aktiveraAllaToolStripMenuItem,
            this.makuleraToolStripMenuItem});
            this.cardContextMenuStrip.Name = "cardContextMenuStrip";
            this.cardContextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // aktiveraAllaToolStripMenuItem
            // 
            this.aktiveraAllaToolStripMenuItem.Name = "aktiveraAllaToolStripMenuItem";
            this.aktiveraAllaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aktiveraAllaToolStripMenuItem.Text = "Aktivera alla";
            this.aktiveraAllaToolStripMenuItem.Click += new System.EventHandler(this.activeCardToolStripMenuItem_Click);
            // 
            // makuleraToolStripMenuItem
            // 
            this.makuleraToolStripMenuItem.Name = "makuleraToolStripMenuItem";
            this.makuleraToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.makuleraToolStripMenuItem.Text = "Makulera";
            this.makuleraToolStripMenuItem.Click += new System.EventHandler(this.cancelCardToolStripMenuItem_Click);
            // 
            // EditTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tagsGridView);
            this.Name = "EditTags";
            this.Text = "EditTags";
            ((System.ComponentModel.ISupportInitialize)(this.tagsGridView)).EndInit();
            this.cardContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView tagsGridView;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ContextMenuStrip cardContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem makuleraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aktiveraAllaToolStripMenuItem;
    }
}