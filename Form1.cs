using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_DIEM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Tao chuoi ket noi va cac bien
        string ketnoi = "Data Source=.\\sqlexpress;Initial Catalog=QL_DIEM;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn;
        SqlCommand cmd;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load du lieu tu database len datagridview
            conn = new SqlConnection(ketnoi);
            conn.Open();

            string sql = "SELECT * FROM DIEM";
            SqlDataAdapter daDiem = new SqlDataAdapter(sql, conn);
            DataTable dtDiem = new DataTable();
            daDiem.Fill(dtDiem);
            dgvDiem.DataSource = dtDiem;
        }

        // Xoa du lieu trong control va focus vao ma sv
        private void btnReset_Click(object sender, EventArgs e)
        {
            Clear(cbID);
            Clear(cbMH);
            tbDiem.Clear();
            tbTen.Clear();
            cbID.Focus();
        }

        // Tao phuong thuc clear cho combobox
        public void Clear(ComboBox e)
        {
            e.SelectedIndex = -1;
            e.Text = string.Empty;
        }

        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDiem.Rows[e.RowIndex];

                cbID.Text = row.Cells["Mã SV"].Value.ToString().Trim();
                tbTen.Text = row.Cells["Họ Tên"].Value.ToString().Trim();
                cbMH.Text = row.Cells["Môn Học"].Value.ToString().Trim();
                tbDiem.Text = row.Cells["Điểm"].Value.ToString().Trim();
            }
        }

        // Phuong thuc reload csdl
        public void rf()
        {
            using (conn = new SqlConnection(ketnoi))
            {
                conn.Open();

                string sql = "SELECT * FROM DIEM";
                SqlDataAdapter daDiem = new SqlDataAdapter(sql, conn);
                DataTable dtDiem = new DataTable();
                daDiem.Fill(dtDiem);
                dgvDiem.DataSource = dtDiem;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // tao cac bien tuong ung
            string maSv = cbID.Text.Trim();
            string tenSv = tbTen.Text.Trim();
            string monHoc = cbMH.Text.Trim();
            string diem = tbDiem.Text.Trim();

            try
            {
                using (conn = new SqlConnection(ketnoi))
                {
                    conn.Open();
                    string add = "INSERT INTO DIEM ([Mã SV], [Họ Tên], [Môn Học], Điểm) VALUES (@maSv, @tenSv, @monHoc, @diem)";

                    using (cmd = new SqlCommand(add, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSv", maSv);
                        cmd.Parameters.AddWithValue("@tenSv", tenSv);
                        cmd.Parameters.AddWithValue("@monHoc", monHoc);
                        cmd.Parameters.AddWithValue("@diem", diem);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Them thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            rf();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Da xay ra loi: " + ex.Message, "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            // tao cac bien tuong ung
            string maSv = cbID.Text.Trim();
            string tenSv = tbTen.Text.Trim();
            string monHoc = cbMH.Text.Trim();
            string diem = tbDiem.Text.Trim();

            try
            {
                using (conn = new SqlConnection(ketnoi))
                {
                    conn.Open();
                    string del = "DELETE FROM DIEM WHERE [Mã SV] = @maSv";

                    using (cmd = new SqlCommand(del, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSv", maSv);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Xoa thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            rf();
                            Clear(cbID);
                            Clear(cbMH);
                            tbDiem.Clear();
                            tbTen.Clear();
                            cbID.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Da xay ra loi: " + ex.Message, "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // tao cac bien tuong ung
            string maSv = cbID.Text.Trim();
            string tenSv = tbTen.Text.Trim();
            string monHoc = cbMH.Text.Trim();
            string diem = tbDiem.Text.Trim();

            try
            {
                using (conn = new SqlConnection(ketnoi))
                {
                    conn.Open();
                    string del = "UPDATE DIEM SET [Mã SV] = @maSv, [Họ Tên] = @tenSv, [Môn Học] = @monHoc, Điểm = @diem";

                    using (cmd = new SqlCommand(del, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSv", maSv);
                        cmd.Parameters.AddWithValue("@tenSv", tenSv);
                        cmd.Parameters.AddWithValue("@monHoc", monHoc);
                        cmd.Parameters.AddWithValue("@diem", diem);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sua thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            rf();
                            Clear(cbID);
                            Clear(cbMH);
                            tbDiem.Clear();
                            tbTen.Clear();
                            cbID.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Da xay ra loi: " + ex.Message, "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
