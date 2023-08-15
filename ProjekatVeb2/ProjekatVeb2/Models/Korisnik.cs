﻿using System.ComponentModel.DataAnnotations;

namespace ProjekatVeb2.Models
{
    public class Korisnik
    {
       
        public int Id { get; set; }
        public string KorisnickoIme { get; set; } 
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PonoviLozinku { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika Tip { get; set; }
        public StatusVerifikacije StatusVerifikacije { get; set; }
        public bool Verifikovan { get; set; }
        public byte[]? Slika { get; set; }
        public List<Artikal> Artikili { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
        
        


    }
}
