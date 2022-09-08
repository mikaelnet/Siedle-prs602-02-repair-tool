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

        private DataGridViewTextBoxColumn _idColumn;
        private DataGridViewTextBoxColumn _rfidColumn;
        private DataGridViewTextBoxColumn _descriptionColumn;
        private DataGridViewTextBoxColumn _printedNumberColumn;
        private DataGridViewTextBoxColumn _belongsToColumn;

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

            _idColumn = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.Index),
                Name = "Id",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic,
            };
            _rfidColumn = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.CardNumber),
                Name = "RFID",
            };
            _descriptionColumn = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.Description),
                Name = "Description",
                SortMode = DataGridViewColumnSortMode.Automatic,
            };
            _printedNumberColumn = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.PrintedNumber),
                Name = "No",
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true,
                Width = 100,
            };
            _belongsToColumn = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nameof(CardWrapper.BelongsTo),
                Name = "Belongs to",
                SortMode = DataGridViewColumnSortMode.Automatic,
                ReadOnly = true,
                DividerWidth = 2,
            };

            tagsGridView.Columns.Add(_idColumn);
            tagsGridView.Columns.Add(_rfidColumn);
            tagsGridView.Columns.Add(_descriptionColumn);
            tagsGridView.Columns.Add(_printedNumberColumn);
            tagsGridView.Columns.Add(_belongsToColumn);

            _idColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
            _rfidColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
            _descriptionColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
            _printedNumberColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
            _belongsToColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;

            DataGridViewCheckBoxColumn checkbox = null;
            for (int i = 0; i < 8*3; i++)
            {
                if (!_projectWrapper.IsDoorEnabled(i))
                    continue;

                checkbox = new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = CardWrapper.GetDoorPropertyName(i),
                    Name = _projectWrapper.GetDoorName(i),
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        BackColor = Color.Beige, 
                    }
                };
                tagsGridView.Columns.Add(checkbox);
            }

            if (checkbox != null)
                checkbox.DividerWidth = 2;

            checkbox = null;
            for (int i = 0; i < 4 * 3; i++)
            {
                if (!_projectWrapper.IsSluiceEnabled(i))
                    continue;

                checkbox = new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = CardWrapper.GetSluicePropertyName(i),
                    Name = _projectWrapper.GetSluiceName(i),
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        BackColor = Color.AntiqueWhite,
                    }
                };
                tagsGridView.Columns.Add(checkbox);
            }

            tagsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            tagsGridView.ColumnHeadersHeight = 50;
            tagsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            tagsGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            /*foreach (DataGridViewColumn column in tagsGridView.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            }*/

            tagsGridView.CellPainting += new DataGridViewCellPaintingEventHandler(tagsGridView_CellPainting);

            tagsGridView.ColumnHeaderMouseClick += TagsGridView_ColumnHeaderMouseClick;
        }

        private void TagsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tagsGridView.Columns[e.ColumnIndex] == _idColumn)
            {
                _projectWrapper.SortId();
            }
            else if (tagsGridView.Columns[e.ColumnIndex] == _rfidColumn)
            {
                _projectWrapper.SortRfid();
            }
            else if (tagsGridView.Columns[e.ColumnIndex] == _descriptionColumn)
            {
                _projectWrapper.SortDescription();
            }
            else if (tagsGridView.Columns[e.ColumnIndex] == _printedNumberColumn)
            {
                _projectWrapper.SortPrintedNumber();
            }
            else if (tagsGridView.Columns[e.ColumnIndex] == _belongsToColumn)
            {
                _projectWrapper.SortBelongsTo();
            }
            else
            {
                return;
            }

            tagsGridView.Invalidate();
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

        private void activeCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in tagsGridView.SelectedRows)
            {
                var cardWrapper = row.DataBoundItem as CardWrapper;
                if (cardWrapper == null)
                    continue;

                cardWrapper.SetAllDoors(true);
            }
            tagsGridView.Invalidate();
        }

        private void cancelCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in tagsGridView.SelectedRows)
            {
                var cardWrapper = row.DataBoundItem as CardWrapper;
                if (cardWrapper == null)
                    continue;
                
                cardWrapper.SetAllDoors(false);
            }
            tagsGridView.Invalidate();
        }

        private void tagsGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            var gridView = sender as DataGridView;
            if (gridView == null)
                return;

            if (e.RowIndex < 0)
                return;

            if (gridView.SelectedRows.Count == 0)
                return;

            e.ContextMenuStrip = cardContextMenuStrip;
        }
    }

}
