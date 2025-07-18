﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QL_Nha_thuoc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var danhSach = new List<BangNoReport.NoItem>
            {
                new BangNoReport.NoItem { STT = 1, MaDoiTuong = "KH001", TenDoiTuong = "Nguyễn Văn A", SoTienNo = 2000000, GhiChu = "Chưa thanh toán" },
                new BangNoReport.NoItem { STT = 2, MaDoiTuong = "KH002", TenDoiTuong = "Trần Thị B", SoTienNo = 1500000, GhiChu = "" }
            };

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BaoCaoBangNo.pdf");

            try
            {
                BangNoReport.Generate(filePath, danhSach);
                MessageBox.Show("Xuất báo cáo thành công:\n" + filePath);
                System.Diagnostics.Process.Start("explorer", filePath); // Mở file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo:\n" + ex.Message);
            }
        }
    }
}
