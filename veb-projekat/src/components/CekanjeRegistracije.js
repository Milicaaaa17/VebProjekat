import React, { useEffect, useState } from 'react';
import { getKorisniciKojiCekajuOdobrenje, odobrenaRegistracija, odbijenaRegistracija } from '../services/AdminService';

const CekanjeVerifikacije = () => {
  const [korisnici, setKorisnici] = useState([]);
  

  useEffect(() =>
  {
      const get = async()=>
      {
          const resp = await getKorisniciKojiCekajuOdobrenje();
          console.log(resp);
          setKorisnici(resp.data);
      }
      get();
  }, []);


  

  const mapirajUlogu = (uloga) => {
    switch (uloga) {
      case 0:
        return 'Administrator';
      case 1:
        return 'Kupac';
      case 2:
        return 'Prodavac';
      default:
        return '';
    }
  };

  const mapirajStatus = (status) => {
    switch (status) {
      case 0:
        return 'UObradi';
      case 1:
        return 'Odobren';
      case 2:
        return 'Odbijen';
      default:
        return '';
    }
  };

  const handleOdbij = async (id) => {
    try {
      const response = await odbijenaRegistracija(id);
      console.log(response); 
      setKorisnici(korisnici.filter((korisnik) => korisnik.id !== id));
    } catch (error) {
      console.error(error);
    }
  };

  const handlePrihvati = async (id) => {
    try {
      const response = await odobrenaRegistracija(id);
      console.log(response);
      setKorisnici(korisnici.filter((korisnik) => korisnik.id !== id));
    } catch (error) {
      console.error(error);
    }
  };


  return (
    <div className="tabela-container">
      <h2>Cekanje odobrenja registracije</h2>
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
            <th>Akcije</th>
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
              <td>{korisnik.odobren ? "da" : "ne" }</td>
              <td>{mapirajStatus(korisnik.statusVerifikacije)}</td>
              <td>
                <button onClick={() => handleOdbij(korisnik.id)}>Odbij</button>
                <button onClick={() => handlePrihvati(korisnik.id)}>Prihvati</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default CekanjeVerifikacije;
