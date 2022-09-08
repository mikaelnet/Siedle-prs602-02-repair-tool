using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siedle.Prs602.RepairTool.Model;

namespace Siedle.Prs602.RepairTool
{
    public partial class EditTags : Form
    {
        private ProjectWrapper _projectWrapper;
        //private BindingSource tagsBindingSource = new BindingSource();

        public EditTags()
        {
            InitializeComponent();

            BuildGrid();
        }

        public void BuildGrid()
        {
            var projectFiles = new[]
            {
                @"..\..\..\Siedle Project 1.xml",
                @"..\..\..\Siedle Project 2.xml",
                @"..\..\..\Siedle Project 3.xml"
            };
            _projectWrapper = new ProjectWrapper(projectFiles.Select(f => new FileInfo(f)));
            var messages = _projectWrapper.LoadAll().ToList();
            
            

            tagsGridView.AutoGenerateColumns = false;
            tagsGridView.DataSource = _projectWrapper.AllCards;
            tagsGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.Index),
                Name = "Id",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            tagsGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.CardNumber),
                Name = "RFID"
            });

            var descriptionColumn = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.Description),
                Name = "Description",
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            tagsGridView.Columns.Add(descriptionColumn);

            tagsGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.PrintedNumber),
                Name = "Number",
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true
            });

            tagsGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.BelongsTo),
                Name = "Belongs to",
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true
            });

            for (int i = 0; i < 8*3; i++)
            {
                if (!_projectWrapper.IsDoorEnabled(i))
                    continue;

                var checkbox = new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = CardWrapper.GetDoorPropertyName(i),
                    Name = _projectWrapper.GetDoorName(i),
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                tagsGridView.Columns.Add(checkbox);
            }

            for (int i = 0; i < 4 * 3; i++)
            {
                if (!_projectWrapper.IsSluiceEnabled(i))
                    continue;

                var checkbox = new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = CardWrapper.GetSluicePropertyName(i),
                    Name = _projectWrapper.GetSluiceName(i)
                };
                tagsGridView.Columns.Add(checkbox);
            }

            tagsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            tagsGridView.ColumnHeadersHeight = 50;
            tagsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

            tagsGridView.CellPainting += new DataGridViewCellPaintingEventHandler(tagsGridView_CellPainting);

            tagsGridView.ColumnHeaderMouseClick += TagsGridView_ColumnHeaderMouseClick;
        }

        private void TagsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tagsGridView.Columns[e.ColumnIndex].Name == "Description")
            {
                _projectWrapper.SortDescription();
            }
        }

        void tagsGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var gridView = sender as DataGridView;
            if (gridView == null)
                return;

            // Check that we're in a header cell
            if (e.RowIndex != -1 || e.ColumnIndex < 5)
                return;

            e.PaintBackground(e.ClipBounds, true);
            var rect = tagsGridView.GetColumnDisplayRectangle(e.ColumnIndex, true);
            var titleSize = TextRenderer.MeasureText(e.Value.ToString(), e.CellStyle.Font);
            if (tagsGridView.ColumnHeadersHeight < titleSize.Width)
            {
                tagsGridView.ColumnHeadersHeight = titleSize.Width;
            }

            e.Graphics.TranslateTransform(0, titleSize.Width);
            e.Graphics.RotateTransform(-90.0f);

            e.Graphics.DrawString(e.Value.ToString(), this.Font, Brushes.Black, 
                new PointF(rect.Y - (tagsGridView.ColumnHeadersHeight - titleSize.Width), rect.X));

            e.Graphics.RotateTransform(90.0F);
            e.Graphics.TranslateTransform(0, -titleSize.Width);
            e.Handled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            foreach (var project in _projectWrapper.Projects)
            {
                project.Save();
            }
        }

       
    }

}
