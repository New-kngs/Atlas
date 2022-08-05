using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasPOP
{
    public class popDataGridUtil
    {
        public static void SetInitGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //셀을 선택해도 행전체가 선택
            dgv.AllowUserToAddRows = false; //맨 마지막 줄에 *표시된 빈 줄 생성을 방지
            dgv.AutoGenerateColumns = false;//데이터소스의 컬럼으로 자동컬럼 생성을 방지 
            dgv.RowHeadersWidth = 50;
            dgv.RowTemplate.Height = 34;
            
            dgv.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            dgv.ClearSelection();

        }

        public static void clickSetInitGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //셀을 선택해도 행전체가 선택
            
            dgv.AllowUserToAddRows = false; //맨 마지막 줄에 *표시된 빈 줄 생성을 방지
            dgv.AutoGenerateColumns = false;//데이터소스의 컬럼으로 자동컬럼 생성을 방지 
            dgv.RowHeadersWidth = 50;
            dgv.RowTemplate.Height = 34;
            dgv.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.ClearSelection();
        }

        public static void AddGridTextBoxColumn(DataGridView dgv,
            string headerText,
            string propertyName,
            int colwidth = 100,
            DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft,
            bool visibility = true,
            bool fixedCol = false)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = propertyName;
            col.HeaderText = headerText;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col.DataPropertyName = propertyName;
            col.DefaultCellStyle.Alignment = align;
            col.Width = colwidth;
            col.Visible = visibility;
            col.ReadOnly = true; //그리드뷰에서 데이터수정 불가
            col.Frozen = fixedCol;
            dgv.Columns.Add(col);
        }
    }
}
