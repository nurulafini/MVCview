using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using LatihanCRUDMVC.Model.Entity;
using LatihanCRUDMVC.Model.Repository;
using LatihanCRUDMVC.Model.Context;

namespace LatihanCRUDMVC.Controller
{
    public class BukuController
    {
        // deklarasi objek Repository untuk menjalankan operasi CRUD
        private BukuRepository _repository;

        /// <summary>
        /// Method untuk menampilkan data buku berdasarkan isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public Buku GetByIsbn(string isbn)
        {
            // deklarasi objek buku
            Buku buku = null;

            // membuat objek context menggunakan blok using
            using (var context = new DbContext())
            {
                _repository = new BukuRepository(context);
                buku = _repository.GetByIsbn(isbn);
            }

            return buku;
        }

        /// <summary>
        /// Method untuk menampilkan data buku berdasarkan judul
        /// </summary>
        /// <param name="judul"></param>
        /// <returns></returns>
        public List<Buku> GetByJudul(string judul)
        {
            // membuat objek collection
            var list = new List<Buku>();

            // membuat objek context menggunakan blok using
            using (var context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new BukuRepository(context);

                // panggil method GetByJudul yang ada di dalam class repository
                list = _repository.GetByJudul(judul);
            }            

            return list;
        }

        /// <summary>
        /// Method untuk menampilkan semua data buku
        /// </summary>
        /// <returns></returns>
        public List<Buku> GetAll()
        {
            // membuat objek collection
            var list = new List<Buku>();

            // membuat objek context menggunakan blok using
            using (var context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new BukuRepository(context);

                // panggil method GetAll yang ada di dalam class repository
                list = _repository.GetAll();
            }

            return list;
        }

        public int Save(Buku obj)
        {
            var result = 0;

            // cek nilai isbn yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(obj.isbn))
            {
                MessageBox.Show("ISBN harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek nilai nama yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(obj.judul))
            {
                MessageBox.Show("Judul harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (var context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new BukuRepository(context);

                // panggil method Save yang ada di dalam class repository
                result = _repository.Save(obj);
            }

            if (result > 0)
            {
                MessageBox.Show("Data buku berhasil disimpan !", "Informasi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data buku gagal disimpan !!!", "Peringatan", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Update(Buku obj)
        {
            var result = 0;

            // cek nilai isbn yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(obj.isbn))
            {
                MessageBox.Show("ISBN harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek nilai nama yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(obj.judul))
            {
                MessageBox.Show("ISBN harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (var context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new BukuRepository(context);

                // panggil method Update yang ada di dalam class repository
                result = _repository.Update(obj);
            }

            if (result > 0)
            {
                MessageBox.Show("Data buku berhasil diupdate !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data buku gagal diupdate !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Delete(Buku obj)
        {
            var result = 0;

            // cek nilai isbn yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(obj.isbn))
            {
                MessageBox.Show("ISBN harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (var context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new BukuRepository(context);

                // panggil method Delete yang ada di dalam class repository
                result = _repository.Delete(obj);
            }

            if (result > 0)
            {
                MessageBox.Show("Data buku berhasil dihapus !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data buku gagal dihapus !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }
    }
}
