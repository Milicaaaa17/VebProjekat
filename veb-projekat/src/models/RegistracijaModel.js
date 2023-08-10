export class RegistracijaModel  {
    constructor(korisnickoIme, ime, prezime, email, lozinka, ponoviLozinku, adresa, datumRodjenja, uloga, slika) {
     this.korisnickoIme = korisnickoIme;
     this.ime = ime;
     this.prezime = prezime;
     this.email = email;
     this.lozinka = lozinka;
     this.ponoviLozinku = ponoviLozinku;
     this.address = adresa;
     this.datumRodjenja = datumRodjenja;
     this.tip = tip;
     this.slika = slika;
    }
 }
export default RegistracijaModel;