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
    public partial class Today_Form : Form
    {
        public Today_Form()
        {
            InitializeComponent();
        }

        private void Today_Form_Load(object sender, EventArgs e)
        {
            CCTRS ctr = new CCTRS();
            ctr.ColorDataGridView(dgv_form);
            CommDB mydb = new CommDB();
            string mysql = "SELECT 打印编号,学员性别,年级科目,教员要求性别,教员要求地域 ,教员要求年级," +
                "教员要求专业,教员要求其他 as 其他教员要求,家教时间,接入时间," +
                "重要备注 FROM [Parents] WHERE 家教状态='接入' OR 家教状态='换人' OR 家教状态='重请' " +
                "ORDER BY 经办时间 DESC";
            DataSet mydataset = mydb.ExecuteQuery(mysql, "Parents");
            dgv_form.DataSource = mydataset.Tables["Parents"];
        }

        private void btn_cannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_form_Click(object sender, EventArgs e)
        {
            ExportToExcel ete = new ExportToExcel();
            ete.ExportExcel("default.xlsx", dgv_form);
        }
    }
}
