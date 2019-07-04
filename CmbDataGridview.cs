using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace TJ_Tutors_Management_System
{
    class CmbDataGridview
    {
        public string GetID(DataGridView dgv,string colName,string ID,DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int ClickIndex = e.RowIndex;
                if (dgv.Rows[ClickIndex].Cells[colName].EditedFormattedValue.ToString() == "True")
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (i != ClickIndex)
                            dgv.Rows[i].Cells[colName].Value = 0;
                    }

                    return dgv.Rows[ClickIndex].Cells[ID].Value.ToString();
                }
                else
                    return "false";
            }
            return "false";
        }
        public void bind(DataGridView dgv,string mystr,string tb)//绑定数据的自定义方法
        {           
            CommDB mydb = new CommDB();
            DataSet mydataset = mydb.ExecuteQuery(mystr, tb);
            dgv.DataSource = mydataset.Tables[tb];
        }
    }
}
