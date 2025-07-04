﻿using Microsoft.Data.SqlClient;
using QL_Nha_thuoc.HangHoa.Them;
using QL_Nha_thuoc.model;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static QL_Nha_thuoc.DanhMuc;

namespace QL_Nha_thuoc.HangHoa
{
    public partial class FormThemHanghoa : Form
    {
        public FormThemHanghoa(string loai)
        {
            InitializeComponent();
            this.Text = "Thêm " + loai;
        }

        // Load dữ liệu vào combobox Loại hàng
        private void LoadDataToComboBoxLoaiHanghoa()
        {
            try
            {
                using (SqlConnection conn = new CSDL().GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TEN_LOAI_HH, MA_LOAI_HH FROM LOAI_HANG";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        comboBoxLoaiHang.DataSource = table;
                        comboBoxLoaiHang.DisplayMember = "TEN_LOAI_HH";
                        comboBoxLoaiHang.ValueMember = "MA_LOAI_HH";
                        comboBoxLoaiHang.SelectedValue = "HH";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load loại hàng: " + ex.Message);
            }
        }

        // Load nhóm hàng theo loại hàng được chọn
        private void LoadDataToComboBoxNhomHanghoa()
        {
            if (comboBoxLoaiHang.SelectedItem is DataRowView selectedRow)
            {
                string maLoai = selectedRow["MA_LOAI_HH"].ToString();

                using (SqlConnection conn = new CSDL().GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT MA_NHOM_HH, TEN_NHOM 
                                     FROM NHOM_HANG 
                                     WHERE MA_LOAI_HH = @loaihang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@loaihang", maLoai);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comboBoxNhomHang.DataSource = dt;
                            comboBoxNhomHang.DisplayMember = "TEN_NHOM";
                            comboBoxNhomHang.ValueMember = "MA_NHOM_HH";
                            comboBoxNhomHang.SelectedIndex = -1;
                        }
                    }
                }
            }
        }

        private void comboBoxLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLoaiHang.SelectedIndex == -1)
            {
                comboBoxNhomHang.DataSource = null; // Xóa dữ liệu nhóm hàng khi không có loại hàng nào được chọn
                return;
            }
            LoadDataToComboBoxNhomHanghoa();
        }

        // Load dữ liệu hãng sản xuất vào combobox
        private void LoadDataToComboboxHangSX()
        {

            try
            {
                using (SqlConnection conn = new CSDL().GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TEN_HANG_SX, MA_HANG_SX FROM HANG_SAN_XUAT";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        comboBoxHangSX.DataSource = table;
                        comboBoxHangSX.DisplayMember = "TEN_HANG_SX";
                        comboBoxHangSX.ValueMember = "MA_HANG_SX";
                        comboBoxHangSX.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load hãng sản xuất: " + ex.Message);
            }
        }
        //load comobox don vi tinh
        private void LoadDataToComboboxDonViTinh()
        {
            try
            {
                using (SqlConnection conn = new CSDL().GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TEN_DON_VI_TINH, MA_DON_VI_TINH FROM DON_VI_TINH";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        comboBoxDonViTinh.DataSource = table;
                        comboBoxDonViTinh.DisplayMember = "TEN_DON_VI_TINH";
                        comboBoxDonViTinh.ValueMember = "MA_DON_VI_TINH";
                        comboBoxDonViTinh.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load đơn vị tính: " + ex.Message);
            }
        }


        // Load dữ liệu khi form khởi tạo
        private void FormThemHanghoa_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxLoaiHanghoa();
            LoadDataToComboboxHangSX();
            LoadDataToComboboxDonViTinh();
        }

        private void buttonBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonThemHangSX_Click(object sender, EventArgs e)
        {
            var formThem = new FormThemHangSX();
            formThem.StartPosition = FormStartPosition.CenterParent;

            formThem.DuLieuDaThayDoi += (s, ev) =>
            {
                // Load lại danh sách hãng SX
                LoadDataToComboboxHangSX();

                // Tìm tên hãng vừa thêm và chọn nó
                string tenMoi = formThem.TenHangSXMoi;
                if (!string.IsNullOrEmpty(tenMoi))
                {
                    int index = comboBoxHangSX.FindStringExact(tenMoi);
                    if (index >= 0)
                    {
                        comboBoxHangSX.SelectedIndex = index;
                    }
                }
            };

            formThem.ShowDialog();
        }

        private void textBoxTenHangHoa_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxTenHangHoa.Text.Trim();

            // Xóa các control cũ trong danh sách
            flowLayoutPanelListHangHoa.Controls.Clear();

            // Lấy danh sách hàng hóa phù hợp
            List<ClassHangHoa> danhSach = ClassHangHoa.LayThongTinHHfromDanhMuc(keyword);

            if (danhSach.Count > 0)
            {
                foreach (var hanghoa in danhSach)
                {
                    var item = new UserControlitemHangHoa();
                    item.SetData(hanghoa);

                    // Gắn sự kiện chọn hàng hóa (1 lần duy nhất)
                    item.HangHoaDuocChon += UserControl_HangHoaDuocChon;

                    flowLayoutPanelListHangHoa.Controls.Add(item);
                }
            }

            // Hiển thị hoặc ẩn danh sách
            flowLayoutPanelListHangHoa.Visible = danhSach.Count > 0;
        }

        // Hàm xử lý khi 1 hàng hóa được chọn
        private void UserControl_HangHoaDuocChon(object sender, ClassHangHoa hanghoa)
        {
            // Ẩn danh sách sau khi chọn

            // TODO: Gán dữ liệu vào các TextBox khác nếu cần
            textBoxTenHangHoa.Text = hanghoa.TenHangHoa;
            textBoxMaVach.Text = hanghoa.MaVach;

            string thuMucAnh = @"C:\Users\hotan\OneDrive\Tài liệu\GitHub\DoAn1_DH22TIN03_HoTanTai_VuTanTai_ung-dung-desktop-quan-ly-nha-thuoc-Long-Chau\QL_Nha-thuoc\QL_Nha-thuoc\Hinh_anh_hang_hoa";

            if (!string.IsNullOrWhiteSpace(hanghoa.HinhAnh))
            {
                string duongDan = Path.Combine(thuMucAnh, hanghoa.HinhAnh);
                try
                {
                    if (File.Exists(duongDan))
                    {
                        using (var fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read))
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                            pictureBoxHangHoa.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBoxHangHoa.Image = Properties.Resources._default;
                    }
                }
                catch (Exception ex)
                {
                    pictureBoxHangHoa.Image = Properties.Resources._default;
                    Debug.WriteLine("Lỗi khi tải ảnh: " + ex.Message);
                }
            }
            else
            {
                pictureBoxHangHoa.Image = Properties.Resources._default;
            }



            flowLayoutPanelListHangHoa.Visible = false;
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {
            //kiem tra dau vao 


            // 2. Kiểm tra và chuyển đổi số liệu giá bán - giá vốn
            decimal giaBan = 0, giaVon = 0;
            if (!decimal.TryParse(textBoxGiaBan.Text.Trim(), out giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxGiaVon.Text.Trim(), out giaVon) || giaVon < 0)
            {
                MessageBox.Show("Giá vốn không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxDonViTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxNhomHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //tao hang hoa moi 
            string maHH = ClassHangHoa.TaoMaHangHoaTuDong();
            ClassHangHoa hangHoa = new ClassHangHoa
            {
                MaHangHoa = maHH,
                MaVach = textBoxMaVach.Text,
                MaDonViTinh = comboBoxDonViTinh.SelectedValue.ToString(),
                TenHangHoa = textBoxTenHangHoa.Text,
                MaLoaiHangHoa = comboBoxLoaiHang.SelectedValue.ToString(),
                MaNhomHang = comboBoxNhomHang.SelectedValue.ToString(),
                MaHangSX = comboBoxHangSX.SelectedValue.ToString(),
                QuyCachDongGoi = textBoxQuyCachDongGoi.Text,
                GhiChu = textBoxGhiChu.Text,
                TinhTrang = "Đang kinh doanh",
                GiaBan = giaBan,
                GiaVon = giaVon
            };
            // 4. Gọi hàm thêm thuốc vào CSDL
            bool kq = ClassHangHoa.ThemHangHoaMoi(hangHoa);

            // 5. Thông báo kết quả
            if (kq)
            {
                MessageBox.Show("Thêm hang hoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form

            }
            else
            {
                MessageBox.Show("Không thể thêm hang hoa. Vui lòng kiểm tra lại thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void buttonXoaHangSX_Click(object sender, EventArgs e)
        {
            //lay thuong tin hang sx dang chon
            int maHangSX = Convert.ToInt32(comboBoxHangSX.SelectedValue);

            if (ClassHangSanXuat.XoaHangSX(maHangSX))
            {
                //thong bao xoa thanh cong
                MessageBox.Show("Xóa hãng sản xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //load lai danh sach hang sx
                LoadDataToComboboxHangSX();
            }
            else
            {
                //thong bao xoa that bai
                MessageBox.Show("Không thể xóa hãng sản xuất. Vui lòng kiểm tra lại thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        //khi kich nut luu. thi kiem tra / tao ma hang/ them /them don gia






    }
}
