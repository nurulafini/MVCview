using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LatihanCRUDMVC.Controller;
using LatihanCRUDMVC.Model.Entity;

namespace LatihanCRUDMVC.View
{
    public partial class FrmMahasiswa : Form
    {
        //deklarasi objek controller
        private MahasiswaController _controller;

        //deklarasi private variabel/field _addData
        private bool _addData;

        //constructor
        public FrmMahasiswa()
        {
            InitializeComponent();
            InisialisasiListView();

            //membuat objek controller
            _controller = new MahasiswaController();

            LoadDataMahasiswa();

            btnSimpan.Location = btnTambah.Location;
            btnBatal.Location = btnPerbaiki.Location;
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = System.Windows.Forms.View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("NPM", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 220, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Alamat", 280, HorizontalAlignment.Center);
        }

        private void LoadDataMahasiswa()
        {
            lvwMahasiswa.Items.Clear();

            var list = _controller.GetAll();

            foreach(var mhs in list)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.npm);
                item.SubItems.Add(mhs.nama);
                item.SubItems.Add(mhs.alamat);

                lvwMahasiswa.Items.Add(item);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            _addData = true;

            mskNPM.Clear();
            txtNama.Clear();
            txtAlamat.Clear();

            mskNPM.Focus();

            btnTambah.Visible = false;
            btnPerbaiki.Visible = false;
            btnHapus.Visible = false;

            btnSimpan.Visible = true;
            btnBatal.Visible = true;
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            btnTambah.Visible = true;
            btnPerbaiki.Visible = true;
            btnHapus.Visible = true;

            btnSimpan.Visible = false;
            btnBatal.Visible = false;

            mskNPM.Clear();
            txtNama.Clear();
            txtAlamat.Clear();
        }

        private void btnPerbaiki_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {

                _addData = false;
                var index = lvwMahasiswa.SelectedIndices[0];

                var item = lvwMahasiswa.Items[index];
                mskNPM.Text = item.SubItems[1].Text;
                txtNama.Text = item.SubItems[2].Text;
                txtAlamat.Text = item.SubItems[3].Text;

                mskNPM.Focus();

                btnTambah.Visible = false;
                btnPerbaiki.Visible = false;
                btnHapus.Visible = false;

                btnSimpan.Visible = true;
                btnBatal.Visible = true;
            }
            else
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var mhs = new Mahasiswa();

            mhs.npm = mskNPM.Text;
            mhs.nama = txtNama.Text;
            mhs.alamat = txtAlamat.Text;

            var result = 0;

            if (_addData == true)
                result = _controller.Save(mhs);
            else
                result = _controller.Update(mhs);

            if (result > 0)
            {
                mskNPM.Clear();
                txtNama.Clear();
                txtAlamat.Clear();

                mskNPM.Focus();

                btnTambah.Visible = true;
                btnPerbaiki.Visible = true;
                btnHapus.Visible = true;

                btnSimpan.Visible = false;
                btnBatal.Visible = false;

                LoadDataMahasiswa();
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (konfirmasi == DialogResult.Yes)
                {

                    var index = lvwMahasiswa.SelectedIndices[0];
                    var item = lvwMahasiswa.Items[index];

                    var mhs = new Mahasiswa();
                    mhs.npm = item.SubItems[1].Text;

                    var result = _controller.Delete(mhs);
                    if (result > 0) LoadDataMahasiswa();
                }
            }
            else
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {

        }
    }
}
