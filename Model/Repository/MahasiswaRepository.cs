using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using LatihanCRUDMVC.Model.Context;
using LatihanCRUDMVC.Model.Entity;

namespace LatihanCRUDMVC.Model.Repository
{
    /// <summary>
    /// Class repository berisi perintah untuk menjalankan operasi CRUD
    /// </summary>
    public class MahasiswaRepository
    {
        // deklarsi objek DbContext
        private DbContext _context;

        // constructor
        public MahasiswaRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method untuk menampilkan data mahasiswa berdasarkan npm
        /// </summary>
        /// <param name="npm"></param>
        /// <returns></returns>
        public Mahasiswa GetByNpm(string npm)
        {
            // deklarasi objek mahasiswa
            Mahasiswa mhs = null;

            var sql = @"select npm, nama, alamat 
                        from mahasiswa 
                        where npm = @npm";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @npm
                cmd.Parameters.AddWithValue("@npm", npm);

                // membuat objek dtr (data reader) menggunakan blok using, untuk menampung hasil perintah SELECT
                using (var dtr = cmd.ExecuteReader())
                {                    
                    // panggil method Read untuk mendapatkan baris dari hasil query
                    if (dtr.Read()) // jika data mahasiswa ditemukan
                    {
                        // membuat objek dari class Mahasiswa
                        mhs = new Mahasiswa();

                        // set nilai property objek mahasiswa
                        mhs.npm = dtr["npm"].ToString();
                        mhs.nama = Convert.ToString(dtr["nama"]);
                        mhs.alamat = dtr["alamat"].ToString();
                    }
                }
            }

            return mhs;
        }

        /// <summary>
        /// Method untuk menampilkan data mahasiwa berdasarkan pencarian nama
        /// </summary>
        /// <param name="nama"></param>
        /// <returns></returns>
        public List<Mahasiswa> GetByNama(string nama)
        {
            // membuat objek collection
            var list = new List<Mahasiswa>();

            var sql = @"select npm, nama, alamat 
                        from mahasiswa 
                        where nama like @nama
                        order by nama";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @nama
                cmd.Parameters.AddWithValue("@nama", "%" + nama + "%");

                // membuat objek dtr (data reader) menggunakan blok using, untuk menampung hasil perintah SELECT
                using (var dtr = cmd.ExecuteReader())
                {                    
                    // panggil method Read untuk mendapatkan baris dari hasil query
                    while (dtr.Read())
                    {
                        // membuat objek dari class Mahasiswa
                        var mhs = new Mahasiswa();

                        // set nilai property objek mahasiswa
                        mhs.npm = dtr["npm"].ToString();
                        mhs.nama = Convert.ToString(dtr["nama"]);
                        mhs.alamat = dtr["alamat"].ToString();

                        // tambahkan objek mahasiswa ke dalam collection
                        list.Add(mhs);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Method untuk menampilkan semua data mahasiwa
        /// </summary>
        /// <returns></returns>
        public List<Mahasiswa> GetAll()
        {
            // membuat objek collection
            var list = new List<Mahasiswa>();

            var sql = @"select npm, nama, alamat 
                        from mahasiswa 
                        order by nama";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // membuat objek dtr (data reader) menggunakan blok using, untuk menampung hasil perintah SELECT
                using (var dtr = cmd.ExecuteReader())
                {
                    // panggil method Read untuk mendapatkan baris dari hasil query
                    while (dtr.Read())
                    {
                        // membuat objek dari class Mahasiswa
                        var mhs = new Mahasiswa();

                        // set nilai property objek mahasiswa
                        mhs.npm = dtr["npm"].ToString();
                        mhs.nama = Convert.ToString(dtr["nama"]);
                        mhs.alamat = dtr["alamat"].ToString();

                        // tambahkan objek mahasiswa ke dalam collection
                        list.Add(mhs);
                    }
                }
            }

            return list;
        }

        public int Save(Mahasiswa obj)
        {
            var result = 0;

            var sql = @"insert into mahasiswa (npm, nama, alamat)
                        values (@npm, @nama, @alamat)";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @npm, @nama dan @alamat
                cmd.Parameters.AddWithValue("@npm", obj.npm);
                cmd.Parameters.AddWithValue("@nama", obj.nama);
                cmd.Parameters.AddWithValue("@alamat", obj.alamat);

                // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        public int Update(Mahasiswa obj)
        {
            var result = 0;

            var sql = @"update mahasiswa set nama = @nama, alamat = @alamat
                        where npm = @npm";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @npm, @nama dan @alamat
                cmd.Parameters.AddWithValue("@nama", obj.nama);
                cmd.Parameters.AddWithValue("@alamat", obj.alamat);
                cmd.Parameters.AddWithValue("@npm", obj.npm);

                // jalankan perintah UPDATE dan tampung hasilnya ke dalam variabel result
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        public int Delete(Mahasiswa obj)
        {
            var result = 0;

            var sql = @"delete from mahasiswa
                        where npm = @npm";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @npm
                cmd.Parameters.AddWithValue("@npm", obj.npm);

                // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                result = cmd.ExecuteNonQuery();                
            }

            return result;
        }
    }
}
