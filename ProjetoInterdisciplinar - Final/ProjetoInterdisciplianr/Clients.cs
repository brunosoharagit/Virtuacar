﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoInterdisciplianr
{
    public partial class Clients : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;


        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        public Clients()
        {
            InitializeComponent();
        }

        void GridFill()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
            conn.Open();

            MySqlDataAdapter data = new MySqlDataAdapter("ClientViewAll", conn);
            data.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable table = new DataTable();
            data.Fill(table);

            dgvClients.DataSource = table;
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            GridFill();
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClientForm_Click(object sender, EventArgs e)
        {
            ClientForm cform = new ClientForm();
            cform.ShowDialog();
        }
    }
}
