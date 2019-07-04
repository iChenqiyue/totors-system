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
    public delegate void UpDateHandle2(string state,string time,string payment_state); //定义委托  
     
    public partial class Tutors_management : Form
    {
        public UpDateHandle2 UpDate2;

        string parent_num1, print_num1, tutors_num1, tutors_name1, state1, time1, principal1,
            ID, ID2, payyes1, remarks1, tempstate, payment_state1;
        bool flag = false,flag2=false;
        CommDB mydb = new CommDB();//

        private void cbo_tutors_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        /* private void cbo_state_SelectedIndexChanged(object sender, EventArgs e)
         {
             cbo_tutors_state.SelectedItem = cbo_state.SelectedItem;

         }*/

        private void cbo_tutors_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag2 == true)
            {
                tutors_num1 = cbo_tutors_num.Text.Trim();
                if (tutors_num1 == "")
                {
                    txt_tutors_name.Text = "";
                    return;
                }
                else
                {
                    string mysql = "SELECT 姓名 FROM [Tutors] WHERE 教员编号 = '" + tutors_num1 + "'";
                    tutors_name1 = mydb.Returnafield(mysql);
                    mysql = string.Format("SELECT * FROM [Parents-Tutors] WHERE 打印编号='{0}' AND 教员编号='{1}'", print_num1, tutors_num1);
                    int i = mydb.Rownum(mysql);
                    if (i > 0)
                    {
                        mysql = string.Format("SELECT ID FROM [Parents-Tutors] WHERE ID="
                            + "(SELECT MAX(ID) FROM [Parents-Tutors] WHERE 打印编号='{0}' AND 教员编号='{1}')", print_num1, tutors_num1);
                        ID = mydb.Returnafield(mysql);
                        txt_tutors_name.Text = tutors_name1;
                        mysql = "SELECT 经办状态 FROM [Parents-Tutors] WHERE ID= '" + ID + "'";
                        state1 = mydb.Returnafield(mysql);
                        string[] state = state1.Split(' ');
                        cbo_state.SelectedItem = state.Last();
                        tempstate = state.Last();
                        mysql = "SELECT 经办人 FROM [Parents-Tutors] WHERE ID= '" + ID + "'";
                        principal1 = mydb.Returnafield(mysql);
                        mysql = "SELECT 经办时间 FROM [Parents-Tutors] WHERE ID= '" + ID + "'";
                        time1 = mydb.Returnafield(mysql);
                        mysql = "SELECT 处理备注 FROM [Parents-Tutors] WHERE ID= '" + ID + "'";
                        remarks1 = mydb.Returnafield(mysql);
                        txt_remark.Text = remarks1;
                        flag = true;//记录存在
                    }
                }
            }
        }

        private void dt_time_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_inquire_Click(object sender, EventArgs e)
        {
            Students_Inquire myform = new Students_Inquire(2);
            myform.UpDate3 += new UpDateHandle3(myform_UpDate3);
            myform.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            myform.ShowDialog();
        }
        void myform_UpDate3(string num,string name)/////这里要改！！！
        {
            cbo_tutors_num.Text = num;
            txt_tutors_name.Text = name;
        }
        private void cbo_principal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public Tutors_management(string parent_num, string print_num,string payyes,string payment_state)
        {
            InitializeComponent();
            parent_num1 = parent_num;
            print_num1 = print_num;            
            payyes1 = payyes;
            payment_state1 = payment_state;
        }
        public Tutors_management(string parent_num, string print_num, string payyes, string tutors_num, string tutors_name, 
            string state, string remarks,string tutors_state)
        {
            InitializeComponent();
            parent_num1 = parent_num;
            print_num1 = print_num;
            payyes1 = payyes;
            tutors_num1 = tutors_num;
            tutors_name1 = tutors_name;
            state1 = state;
            remarks1 = remarks;
            cbo_tutors_state.Text = tutors_state;
        }
        private void Tutors_management_Load(object sender, EventArgs e)
        {
            CmbClass cmbc = new CmbClass();
            cmbc.cboDataBind(cbo_principal, "SELECT distinct 姓名 FROM [Workers]", "Workers", "姓名");
            cbo_principal.SelectedItem = null;
            txt_printnum.Text = print_num1;
            string mysql = string.Format("SELECT distinct 教员编号 FROM [Parents-Tutors] WHERE 家长编号='{0}' " +
                "AND 打印编号='{1}'", parent_num1, print_num1);
            cmbc.cboDataBind(cbo_tutors_num, mysql, "Parents-Tutors", "教员编号");
            if (tutors_num1 != "")
                cbo_tutors_num.Text = tutors_num1;
            if (tutors_name1 != "")
                txt_tutors_name.Text = tutors_name1;
            if (state1 != "")
                cbo_state.SelectedItem = state1;
            if (remarks1 != "")
                txt_remark.Text = remarks1;
            flag2 = true;
            if (payyes1 == "是")
                rdo_yes.Checked = true;
            if (payyes1 == "否")
                rdo_no.Checked = true;
        }

        private void btn_cannel_Click(object sender, EventArgs e)
        {
          //  DialogResult result = MessageBox.Show("确认退出？", "操作提示",
            //    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
           // if (result == DialogResult.OK)
                this.Close();
           // else
             //   return;
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            
            if (cbo_principal.Text == "")
            {
                MessageBox.Show("请选择经办人！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (rdo_yes.Checked)
                payyes1 = "是";
            if (rdo_no.Checked)
                payyes1 = "否";
            remarks1 = txt_remark.Text.Trim();

            DialogResult result = MessageBox.Show("确认办理？", "操作提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                
                    state1 += " " + cbo_state.Text;
                    principal1 += " " + cbo_principal.Text;
                    time1 += " " + dt_time.Value.ToString("yyyy/MM/dd");
             /*   if (payment_state1 == "待缴" && cbo_tutors_state.Text == "换人")
                {
                    payment_state1 = "无需缴纳";
                }
                if (payment_state1 == "待缴" && cbo_tutors_state.Text == "不请")
                {
                    payment_state1 = "无需缴纳";
                }*/
                    string mysql = string.Format("UPDATE [Parents] SET 家教状态='{0}',经办时间='{1}',缴费状态='{2}' WHERE " +
                    "ID='{3}'", cbo_tutors_state.Text, dt_time.Value.ToString("yyyy/MM/dd"),payment_state1,ID);
                try
                {
                    mydb.ExecuteNonQuery(mysql);
                    MessageBox.Show("办理成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    mysql = string.Format("INSERT INTO [Responsibility] (家长编号,打印编号,经办状态,经办时间,经办人,处理备注) " +
                        "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                        parent_num1, print_num1, cbo_state.Text, dt_time.Value.ToString("yyyy/MM/dd"), cbo_principal.Text,
                        txt_remark.Text.Trim());
                    mydb.ExecuteNonQuery(mysql);

                    //      mysql = "SELECT * FROM [Parents-Tutors] WHERE ID= '" + ID + "'";
                    //   int i = mydb.Rownum(mysql);
                    //   if (i > 0)
                    // {

                    Integral itg = new Integral();
                    itg.Add(cbo_principal.Text, cbo_state.Text);
                    
                        if (((tempstate=="接入"||tempstate == "换人" || tempstate == "不请" || tempstate == "重请")
                        && (cbo_state.Text == "已接" || cbo_state.Text == "待接"))||flag==false)
                        {
                            state1 = cbo_state.Text;

                            time1 = dt_time.Value.ToString("yyyy/MM/dd");

                            principal1 = cbo_principal.Text;

                            mysql = string.Format("INSERT INTO [Parents-Tutors] (家长编号,打印编号,经办状态,经办时间,经办人," +
                            "处理备注,教员编号,教员姓名) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", parent_num1, print_num1, state1,
                            time1, principal1, txt_remark.Text.Trim(), cbo_tutors_num.Text.Trim(),txt_tutors_name.Text.Trim());
                        
                            
                    }
                        else if(flag==true)
                        {

                            mysql = string.Format("UPDATE [Parents-Tutors] SET 经办状态='{0}',经办时间='{1}',经办人='{2}',处理备注='{3}'" +
                                "WHERE ID='{4}'", state1, time1, principal1, txt_remark.Text.Trim(), ID);////////这里更新要根据选择的教员编号的最新条！！！！！！
                        }
                    else { }
                    mydb.ExecuteNonQuery(mysql);
                    //      }

                    /*    else
                        {
                            mysql = string.Format("INSERT INTO [Relationship] (家长编号,打印编号,经办状态,经办时间,经办人," +
                                "处理备注,教员编号) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", parent_num1, print_num1, state1,
                                time1, principal1, txt_remark.·Text.Trim(), txt_tutors_num.Text.Trim());
                        }*/  
                    mysql = string.Format("SELECT ID FROM [Tutors-Parents] WHERE 教员编号='{0}' AND 打印编号='{1}' "
                    , cbo_tutors_num.Text.Trim(), print_num1);
                    int  i = mydb.Rownum(mysql);
                    if (i > 0)
                    {
                        mysql = string.Format("SELECT ID FROM [Tutors-Parents] WHERE ID= " +
                    "(SELECT MAX(ID) FROM [Tutors-Parents] WHERE 教员编号='{0}' AND 打印编号='{1}') "
                , cbo_tutors_num.Text.Trim(), print_num1);
                        ID2 = mydb.Returnafield(mysql);
                        mysql = string.Format("UPDATE [Tutors-Parents] SET 家长编号='{0}',打印编号='{1}',经办状态='{2}',经办时间='{3}',经办人='{4}',处理备注='{5}'" +
                                "WHERE ID='{6}'", parent_num1,print_num1, state1, time1, principal1, txt_remark.Text.Trim(), ID2);////这里有问题！！！！！数据溢出！！！！！

                    }
                    else
                    {
                        mysql = string.Format("INSERT INTO [Tutors-Parents] (家长编号,打印编号,经办状态,经办时间,经办人," +
                        "处理备注,教员编号) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", parent_num1, print_num1, state1,
                        time1, principal1, txt_remark.Text.Trim(),cbo_tutors_num.Text.Trim());
                    }
                    mydb.ExecuteNonQuery(mysql);
                    this.Close();
                    //                  UpDate2(cbo_state.Text,state1,time1);
                    UpDate2(cbo_tutors_state.SelectedItem.ToString(), dt_time.Value.ToString("yyyy/MM/dd"),payment_state1);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "操作数据库出错！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void txt_tutors_num_Leave(object sender, EventArgs e)
        {

        }

        private void txt_student_num_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (e.KeyChar == 13)
            {
                string mysql = "SELECT * FROM [Tutors] WHERE 教员编号='" + txt_tutors_num.Text.Trim() + " AND 是否黑名单='是'";
                int i = mydb.Rownum(mysql);
                if (i > 0)
                {
                    MessageBox.Show("此人已被列入黑名单！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_tutors_num.Focus();
                    return;
                }
                else
                {
                    mysql = "SELECT 姓名 FROM [Tutors] WHERE 教员编号='" + txt_tutors_num.Text.Trim() + "'";
                    txt_tutors_name.Text = mydb.Returnafield(mysql);
                }
            }*/
        }
    }
}
