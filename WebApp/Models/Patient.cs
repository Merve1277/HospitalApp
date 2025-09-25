using System;

namespace WebApp.Models
{
    public class Patient
    {
        public int Id { get; set; }         // Hasta ID (Primary Key olacak)
        public string Name { get; set; }    // Ad Soyad
        public int Age { get; set; }        // Yaş
        public string Disease { get; set; } // Hastalık / Şikayet
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Kayıt tarihi

        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
