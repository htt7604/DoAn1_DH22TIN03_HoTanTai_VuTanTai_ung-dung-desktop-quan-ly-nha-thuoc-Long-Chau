﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL_Nha_thuoc;
using QL_Nha_thuoc.NhanVien;

namespace QL_Nha_thuoc
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void danhSáchNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSNhanVien dsNhanVien = new DSNhanVien();
            dsNhanVien.TopLevel = false;
            dsNhanVien.FormBorderStyle = FormBorderStyle.None;
            dsNhanVien.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(dsNhanVien);
            dsNhanVien.Show();
        }

        private void thiếtLậpNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thietlap_NVcs thietlap_NVcs = new Thietlap_NVcs();
            thietlap_NVcs.TopLevel = false;
            thietlap_NVcs.FormBorderStyle = FormBorderStyle.None;
            thietlap_NVcs.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(thietlap_NVcs);
            thietlap_NVcs.Show();
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DanhMuc danhMuc = new DanhMuc();
            danhMuc.TopLevel = false;
            danhMuc.FormBorderStyle = FormBorderStyle.None;
            danhMuc.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(danhMuc);
            danhMuc.Show();
        }

        private void danhMụcThuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DanhMucThuoc danhMucThuoc = new DanhMucThuoc();
            danhMucThuoc.TopLevel = false;
            danhMucThuoc.FormBorderStyle = FormBorderStyle.None;
            danhMucThuoc.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(danhMucThuoc);
            danhMucThuoc.Show();
        }



    }
}
