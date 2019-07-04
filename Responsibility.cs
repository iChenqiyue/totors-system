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
    public partial class Responsibility : Form
    {
        public Responsibility()
        {
            InitializeComponent();
        }
        string mystr = "SELECT 家长编号,打印编号,经办状态,经办时间,经办人,处理备注 FROM [Responsibility]";
        private void Responsibility_Load(object sender, EventArgs e)
        {
            CmbDataGridview cmbd = new CmbDataGridview();
            CCTRS ctr = new CCTRS();
            ctr.ColorDataGridView(dgv_responsibility);
            cmbd.bind(dgv_responsibility, mystr,"Responsibility");
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            if (txt_parentnum.Text.Trim() == "" && txt_print_num.Text.Trim() == "" && txt_time.Text.Trim() == "")
            {
                CmbDataGridview cmbd = new CmbDataGridview();
                cmbd.bind(dgv_responsibility, mystr,"Responsibility");
                return;
            }
            CommDB mydb = new CommDB();
            string mysql = "", tempstr = "";
            if (txt_parentnum.Text != "")
                tempstr = "家长编号 Like '%" + txt_parentnum.Text.Trim() + "%'";
            if (txt_print_num.Text != "")
            {
                if (tempstr == "")
                    tempstr = "打印编号 Like '%" + txt_print_num.Text.Trim() + "%'";
                else
                    tempstr += " AND 打印编号 Like '%" + txt_print_num.Text.Trim() + "%'";
            }
            if (txt_time.Text != "")
            {
                if (tempstr == "")
                    tempstr = "经办时间 Like '%" + txt_time.Text.Trim() + "%'";
                else
                    tempstr += " AND 经办时间 Like '%" + txt_time.Text.Trim() + "%'";
            }
            if (tempstr == "")
                mysql = "SELECT 家长编号,打印编号,经办状态,经办时间,经办人,处理备注 FROM [Responsibility]";
            else
                mysql += "SELECT 家长编号,打印编号,经办状态,经办时间,经办人,处理备注 FROM [Responsibility] WHERE " + tempstr;
            DataSet mydataset = mydb.ExecuteQuery(mysql, "Responsibility");
            dgv_responsibility.DataSource = mydataset.Tables["Responsibility"];
        }

        private void btn_cannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_time_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
