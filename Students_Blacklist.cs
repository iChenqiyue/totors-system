using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TJ_Tutors_Management_System
{
    public partial class Students_Blacklist : Form
    {
        public Students_Blacklist()
        {
            InitializeComponent();
        }

        private void Students_Blacklist_Load(object sender, EventArgs e)
        {
            CmbDataGridview cmbd = new CmbDataGridview();
            CCTRS ctr = new CCTRS();
            ctr.ColorDataGridView(dgv_blacklist);
            CommDB mydb = new CommDB();
            string mysql = "SELECT * FROM [Tutors] WHERE 是否黑名单='是'";
            DataSet mydataset = mydb.ExecuteQuery(mysql, "Tutors");
            dgv_blacklist.DataSource = mydataset.Tables["Tutors"];
            dgv_blacklist.Columns[0].Visible = false;
        }

        private void btn_cannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            if (dgv_blacklist.SelectedRows.Count > 0)
            {
                string tutors_num = dgv_blacklist.SelectedRows[0].Cells[1].Value.ToString();
                string tutors_name = dgv_blacklist.SelectedRows[0].Cells[2].Value.ToString();
                string student_num = dgv_blacklist.SelectedRows[0].Cells[3].Value.ToString();
                string grade_subject = dgv_blacklist.SelectedRows[0].Cells[4].Value.ToString();
                string tutors_degree = dgv_blacklist.SelectedRows[0].Cells[5].Value.ToString();
                string sex = dgv_blacklist.SelectedRows[0].Cells[6].Value.ToString();
                string phone = dgv_blacklist.SelectedRows[0].Cells[7].Value.ToString();
                string place = dgv_blacklist.SelectedRows[0].Cells[8].Value.ToString();
                string blacklist = dgv_blacklist.SelectedRows[0].Cells[9].Value.ToString();
                string imprisonment = dgv_blacklist.SelectedRows[0].Cells[10].Value.ToString();
                string remarks = dgv_blacklist.SelectedRows[0].Cells[11].Value.ToString();
                Students_check myform = new Students_check(tutors_num, tutors_name, student_num, grade_subject,
                    tutors_degree, sex, phone, place, blacklist, imprisonment, remarks);
                myform.Owner = this;
                myform.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                myform.Show();
            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv_blacklist.SelectedRows.Count > 0)
            {
                string tutors_num = dgv_blacklist.SelectedRows[0].Cells[1].Value.ToString();
                string tutors_name = dgv_blacklist.SelectedRows[0].Cells[2].Value.ToString();
                string student_num = dgv_blacklist.SelectedRows[0].Cells[3].Value.ToString();
                string grade_subject = dgv_blacklist.SelectedRows[0].Cells[4].Value.ToString();
                string tutors_degree = dgv_blacklist.SelectedRows[0].Cells[5].Value.ToString();
                string sex = dgv_blacklist.SelectedRows[0].Cells[6].Value.ToString();
                string phone = dgv_blacklist.SelectedRows[0].Cells[7].Value.ToString();
                string place = dgv_blacklist.SelectedRows[0].Cells[8].Value.ToString();
                string blacklist = dgv_blacklist.SelectedRows[0].Cells[9].Value.ToString();
                string imprisonment = dgv_blacklist.SelectedRows[0].Cells[10].Value.ToString();
                string remarks = dgv_blacklist.SelectedRows[0].Cells[11].Value.ToString();
                Students_check myform = new Students_check(tutors_num, tutors_name, student_num, grade_subject,
                    tutors_degree, sex, phone, place, blacklist, imprisonment, remarks);
                myform.Owner = this;
                myform.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                myform.Show();
            }
        }

        private void dgv_blacklist_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                dgv_blacklist.SelectedRows[0].Selected = false;
                dgv_blacklist.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
