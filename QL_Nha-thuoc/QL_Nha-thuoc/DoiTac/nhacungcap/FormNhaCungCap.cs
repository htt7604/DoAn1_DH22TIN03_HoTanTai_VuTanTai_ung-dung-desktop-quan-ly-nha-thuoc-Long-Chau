﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Nha_thuoc.DoiTac
{
    public partial class FormNhaCungCap : Form
    {
        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        //load comboboxLoctheo
        private void comboBoxLocTheo_load()
        {
            comboBoxLocTheo.Items.Add("Mã NCC");
            comboBoxLocTheo.Items.Add("Tên NCC");
            comboBoxLocTheo.SelectedIndex = 0; // mặc định là "Mã KH"

        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
        }
    }
}
