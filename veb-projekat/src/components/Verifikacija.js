import React, { useEffect, useState } from 'react';
import { getVerifikovaniProdavci, getOdbijeniProdavci } from '../services/AdminService';
import { Link } from 'react-router-dom';

const Verifikacija = () => {
  const [prodavci, setProdavci] = useState([]);
  const [prodavci1, setProdavci1] = useState([]);

  useEffect(() =>
  {
      const get = async()=>
      {
          const resp = await getVerifikovaniProdavci();
          console.log(resp);
          setProdavci(resp.data);
      }
      get();
  }, []);

  useEffect(() =>
  {
      const get = async()=>
      {
          const resp = await getOdbijeniProdavci();
          console.log(resp);
          setProdavci1(resp.data);
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
      <h2>Verifikovani prodavci</h2>
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
            <th>Slika</th>
          </tr>
        </thead>
        <tbody>
        {prodavci.map((prodavac) => (
            <tr key={prodavac.id}>
              <td>{prodavac.id}</td>
              <td>{prodavac.korisnickoIme}</td>
              <td>{prodavac.email}</td>
              <td>{prodavac.ime}</td>
              <td>{prodavac.prezime}</td>
              <td>{mapirajUlogu(prodavac.uloga)}</td>
              <td>{prodavac.adresa}</td>
              <td>{prodavac.datumRodjenja}</td>
              <td>{mapirajStatus(prodavac.statusVerifikacije)}</td>
              <td>
            {prodavac.slika && (
              <img
                src={`data:image/jpg;base64,${prodavac.slika}`}
                alt="Profilna slika"
                style={{ width: '80px', height: '80px' }}
              />
            )}
          </td>
            </tr>
          ))}
        </tbody>
      </table>

      <h2>Prodavci cija je verifikacija odbijena</h2>
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
            <th>Slika</th>
          </tr>
        </thead>
        <tbody>
        {prodavci1.map((prodavac1) => (
            <tr key={prodavac1.id}>
              <td>{prodavac1.id}</td>
              <td>{prodavac1.korisnickoIme}</td>
              <td>{prodavac1.email}</td>
              <td>{prodavac1.ime}</td>
              <td>{prodavac1.prezime}</td>
              <td>{mapirajUlogu(prodavac1.uloga)}</td>
              <td>{prodavac1.adresa}</td>
              <td>{prodavac1.datumRodjenja}</td>
              <td>{mapirajStatus(prodavac1.statusVerifikacije)}</td>
              <td>
            {prodavac1.slika && (
              <img
                src={`data:image/jpg;base64,${prodavac1.slika}`}
                alt="Profilna slika"
                style={{ width: '80px', height: '80px' }}
              />
            )}
          </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to="/dashboard">Nazad</Link>
    </div>
  );
};

export default Verifikacija;
