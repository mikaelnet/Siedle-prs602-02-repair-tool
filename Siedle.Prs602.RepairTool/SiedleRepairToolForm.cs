using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Siedle.Prs602.RepairTool
{
    public partial class SiedleRepairToolForm : Form
    {
        private SiedleDatabaseManager _databaseManager;

        public SiedleRepairToolForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var writer = new StringWriter();
            try
            {
                _databaseManager = new SiedleDatabaseManager(writer);
                var file1 = new FileInfo(project1TextBox.Text);
                var file2 = new FileInfo(project2TextBox.Text);
                var file3 = new FileInfo(project3TextBox.Text);

                _databaseManager.LoadDocuments(file1, file2, file3);
                if (sanitizeInputCheckbox.Checked)
                {
                    var isCorrupt = _databaseManager.SanitizeInput();
                    LogTextBox.Text = writer.ToString();
                    if (isCorrupt)
                        return;
                }
                if (fixDescriptionTextsCheckbox.Checked)
                {
                    _databaseManager.FixDescriptionTexts();
                    LogTextBox.Text = writer.ToString();
                }
                if (createMissingCardsCheckbox.Checked)
                {
                    _databaseManager.CreateMissingCards();
                    LogTextBox.Text = writer.ToString();
                }
                if (findNumberingHolesCheckbox.Checked)
                {
                    _databaseManager.FindNumberingHoles();
                    LogTextBox.Text = writer.ToString();
                }
                if (testFlagsCheckbox.Checked)
                {
                    _databaseManager.TestFlagsValidity();
                    LogTextBox.Text = writer.ToString();
                }
                if (commitCheckbox.Checked)
                {
                    _databaseManager.CommitChanges();
                    LogTextBox.Text = writer.ToString();
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex);
                LogTextBox.Text = writer.ToString();
            }
        }

        private void project1Button_Click(object sender, EventArgs e)
        {
            var result = OpenDatabaseDialog.ShowDialog(this);
            if (result == DialogResult.OK && File.Exists(OpenDatabaseDialog.FileName))
            {
                project1TextBox.Text = OpenDatabaseDialog.FileName;
            }
        }

        private void project2Button_Click(object sender, EventArgs e)
        {
            var result = OpenDatabaseDialog.ShowDialog(this);
            if (result == DialogResult.OK && File.Exists(OpenDatabaseDialog.FileName))
            {
                project2TextBox.Text = OpenDatabaseDialog.FileName;
            }
        }

        private void project3Button_Click(object sender, EventArgs e)
        {
            var result = OpenDatabaseDialog.ShowDialog(this);
            if (result == DialogResult.OK && File.Exists(OpenDatabaseDialog.FileName))
            {
                project3TextBox.Text = OpenDatabaseDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //var file1 = new FileInfo(project1TextBox.Text);
            //var file2 = new FileInfo(project2TextBox.Text);
            //var file3 = new FileInfo(project3TextBox.Text);

            var tagsForm = new EditTags();

            tagsForm.Show(this);
        }
    }
}
