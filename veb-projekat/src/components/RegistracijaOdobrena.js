import React, { useEffect, useState } from 'react';
import { getKorisniciKojiSuOdobreni } from '../services/AdminService';

const Verifikacija = () => {
  const [korisnici, setKorisnici] = useState([]);

  useEffect(() =>
  {
      const get = async()=>
      {
          const resp = await getKorisniciKojiSuOdobreni();
          console.log(resp);
          setKorisnici(resp.data);
      }
      get();
  }, []);


  const mapirajUlogu = (uloga) => {
    switch (uloga) {
      case 0:
        return 'Admin';
      case 1:
        return 'Prodavac';
      case 2:
        return 'Kupac';
      default:
        return '';
    }
  };

  const mapirajStatus = (status) => {
    switch (status) {
      case 0:
        return 'Cekanje';
      case 1:
        return 'Prihvacen';
      case 2:
        return 'Odbijen';
      default:
        return '';
    }
  };
  return (
    <div className="tabela-container">
      <h2>Odobreni korisnici </h2>
      <table>
        <thead>
        <tr>
            <th>ID</th>
            <th>Korisničko Ime</th>
            <th>Email</th>
            <th>Ime</th>
            <th>Prezime</th>
            <th>Uloga</th>
            <th>Adresa</th>
            <th>Datum Rodjenja</th>
            <th>Status verifikacije</th>
            <th>Odobren</th>
          </tr>
        </thead>
        <tbody>
        {korisnici.map((korisnik) => (
            <tr key={korisnik.id}>
              <td>{korisnik.id}</td>
              <td>{korisnik.korisnickoIme}</td>
              <td>{korisnik.email}</td>
              <td>{korisnik.ime}</td>
              <td>{korisnik.prezime}</td>
              <td>{mapirajUlogu(korisnik.uloga)}</td>
              <td>{korisnik.adresa}</td>
              <td>{korisnik.datumRodjenja}</td>
              <td>{mapirajStatus(korisnik.statusVerifikacije)}</td>
              <td>{korisnik.odobren ? "da" : "ne"}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Verifikacija;
