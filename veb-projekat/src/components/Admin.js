import React, { useEffect, useState } from 'react';
import { getSviKorisnici } from '../services/AdminService';
import './Tabela.css';
import { Link } from 'react-router-dom';


const Admin = () => {
  const [korisnici, setKorisnici] = useState([]);

 useEffect(() =>
{
    const get = async()=>
    {
        const resp = await getSviKorisnici();
        console.log(resp);
        setKorisnici(resp.data);
    }
    get();
}, []);
const mapirajUlogu = (tip) => {
  switch (tip) {
    case 0:
      return 'Administrator';
    case 1:
      return 'Prodavac';
    case 2:
      return 'Kupac';
    default:
      return '';
  }
};

  return (
    <div className="tabela-container">
      <h2> Svi Korisnici </h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Korisničko Ime</th>
            <th>Email</th>
            <th>Ime</th>
            <th>Prezime</th>
            <th>Adresa</th>
            <th>Datum Rodjenja</th>
            <th>Uloga</th>
            <th>Slika</th>
            
            
            
            
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
              
              <td>{korisnik.adresa}</td>
              <td>{korisnik.datumRodjenja}</td>
              <td>{mapirajUlogu(korisnik.tip)}</td>
              <td>
            {korisnik.slika && (
              <img
                src={`data:image/jpg;base64,${korisnik.slika}`}
                alt="Profilna slika"
                style={{ width: '80px', height: '80px' }}
              />
            )}
          </td>
             
            </tr>
          ))}
        </tbody>
      </table>
      <Link to ="/dashboard"> Nazad </Link>
    </div>
  );
};

export default Admin;
