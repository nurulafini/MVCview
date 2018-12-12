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
    public class BukuRepository
    {
        // deklarsi objek DbContext
        private DbContext _context;

        // constructor
        public BukuRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method untuk menampilkan data buku berdasarkan isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public Buku GetByIsbn(string isbn)
        {
            // deklarasi objek buku
            Buku buku = null;

            var sql = @"select isbn, judul, thn_terbit, bahasa
                        from buku
                        where isbn = @isbn";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @isbn
                cmd.Parameters.AddWithValue("@isbn", isbn);

                // membuat objek dtr (data reader) menggunakan blok using, untuk menampung hasil perintah SELECT
                using (var dtr = cmd.ExecuteReader())
                {
                    // panggil method Read untuk mendapatkan baris dari hasil query
                    if (dtr.Read()) // jika data buku ditemukan
                    {
                        // membuat objek dari class buku
                        buku = new Buku();

                        // set nilai property objek buku
                        buku.isbn = dtr["isbn"].ToString();
                        buku.judul = Convert.ToString(dtr["judul"]);
                        buku.thn_terbit = dtr["thn_terbit"].ToString();
                        buku.bahasa = dtr["bahasa"].ToString();
                    }
                }
            }

            return buku;
        }

        /// <summary>
        /// Method untuk menampilkan data buku berdasarkan pencarian judul
        /// </summary>
        /// <param name="judul"></param>
        /// <returns></returns>
        public List<Buku> GetByJudul(string judul)
        {
            // membuat objek collection
            var list = new List<Buku>();

            var sql = @"select isbn, judul, thn_terbit, bahasa
                        from buku
                        where judul like @judul
                        order by judul";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @judul
                cmd.Parameters.AddWithValue("@judul", "%" + judul + "%");

                // membuat objek dtr (data reader) menggunakan blok using, untuk menampung hasil perintah SELECT
                using (var dtr = cmd.ExecuteReader())
                {
                    // panggil method Read untuk mendapatkan baris dari hasil query
                    while (dtr.Read())
                    {
                        // membuat objek dari class buku
                        var buku = new Buku();

                        // set nilai property objek buku
                        buku.isbn = dtr["isbn"].ToString();
                        buku.judul = Convert.ToString(dtr["judul"]);
                        buku.thn_terbit = dtr["thn_terbit"].ToString();
                        buku.bahasa = dtr["bahasa"].ToString();

                        // tambahkan objek buku ke dalam collection
                        list.Add(buku);
                    }
                }
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

            var sql = @"select isbn, judul, thn_terbit, bahasa
                        from buku
                        order by judul";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // membuat objek dtr (data reader) menggunakan blok using, untuk menampung hasil perintah SELECT
                using (var dtr = cmd.ExecuteReader())
                {
                    // panggil method Read untuk mendapatkan baris dari hasil query
                    while (dtr.Read())
                    {
                        // membuat objek dari class buku
                        var buku = new Buku();

                        // set nilai property objek buku
                        buku.isbn = dtr["isbn"].ToString();
                        buku.judul = Convert.ToString(dtr["judul"]);
                        buku.thn_terbit = dtr["thn_terbit"].ToString();
                        buku.bahasa = dtr["bahasa"].ToString();

                        // tambahkan objek buku ke dalam collection
                        list.Add(buku);
                    }
                }
            }

            return list;
        }

        public int Save(Buku obj)
        {
            var result = 0;

            var sql = @"insert into buku (isbn, judul, thn_terbit, bahasa)
                        values (@isbn, @judul, @thn_terbit, @bahasa)";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @isbn, @judul, @thn_terbit, @bahasa
                cmd.Parameters.AddWithValue("@isbn", obj.isbn);
                cmd.Parameters.AddWithValue("@judul", obj.judul);
                cmd.Parameters.AddWithValue("@thn_terbit", obj.thn_terbit);
                cmd.Parameters.AddWithValue("@bahasa", obj.bahasa);

                // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        public int Update(Buku obj)
        {
            var result = 0;

            var sql = @"update buku set judul = @judul, thn_terbit = @thn_terbit, bahasa = @bahasa
                        where isbn = @isbn";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @isbn, @judul, @thn_terbit, @bahasa                
                cmd.Parameters.AddWithValue("@judul", obj.judul);
                cmd.Parameters.AddWithValue("@thn_terbit", obj.thn_terbit);
                cmd.Parameters.AddWithValue("@bahasa", obj.bahasa);
                cmd.Parameters.AddWithValue("@isbn", obj.isbn);

                // jalankan perintah UPDATE dan tampung hasilnya ke dalam variabel result
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        public int Delete(Buku obj)
        {
            var result = 0;

            var sql = @"delete from buku
                        where isbn = @isbn";

            // membuat objek command menggunakan blok using
            using (var cmd = new OleDbCommand(sql, _context.Conn))
            {
                // set nilai parameter @isbn
                cmd.Parameters.AddWithValue("@isbn", obj.isbn);

                // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
    }
}
