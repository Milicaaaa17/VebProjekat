import React, { useEffect, useState } from 'react';
import { getProdavciKojiCekajuVerifikaciju, prihvatiRegistraciju, odbijRegistracijuProdavca } from '../services/AdminService';
import { Link } from 'react-router-dom';

const CekanjeVerifikacije = () => {
  const [prodavci, setProdavci] = useState([]);
  

  useEffect(() =>
  {
      const get = async()=>
      {
          const resp = await getProdavciKojiCekajuVerifikaciju();
          console.log(resp);
          setProdavci(resp.data);
      }
      get();
  }, []);


  const azurirani = async()=>
      {
          const resp = await getProdavciKojiCekajuVerifikaciju();
          console.log(resp);
          setProdavci(resp.data);
      }

  const mapirajUlogu = (tip) => {
    switch (tip) {
      case 0:
        return 'Admin';
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
        return 'Odobren';
      case 1:
        return 'Odbijen';
      case 2:
        return 'UObradi';
      default:
        return '';
    }
  };
  const handleOdbij = async (prodavacid) => {
    try {
      console.log('Odbij: prodavacid =', prodavacid); // Dodatni log
      const response = await odbijRegistracijuProdavca(prodavacid);
      console.log('Odbij odgovor:', response); // Dodatni log
      setProdavci(prodavci.filter((prodavac) => prodavac.id !== prodavacid));
      azurirani();
    } catch (error) {
      console.error('Greška prilikom odbijanja registracije prodavca:', error);
    }
  };
  
  const handlePrihvati = async (prodavacid) => {
    try {
        console.log('Prihvati: prodavacid =', prodavacid); // Dodatni log
      const response = await prihvatiRegistraciju(prodavacid);
      console.log('Prihvati odgovor:', response); // Dodatni log
      setProdavci(prodavci.filter((prodavac) => prodavac.id !== prodavacid));
      azurirani();
    } catch (error) {
      console.error('Greška prilikom prihvatanja registracije prodavca:', error);
    }
  };
  


  return (
    <div className="tabela-container">
      <h2>Verifikuj prodavce</h2>
      <table>
        <thead>
        <tr>
            <th>id</th>
            <th>Korisničko Ime</th>
            <th>Email</th>
            <th>Ime</th>
            <th>Prezime</th>
            <th>tip</th>
            <th>Adresa</th>
            <th>Datum Rodjenja</th>
            <th>Status verifikacije</th>
            <th>Slika</th>
            <th>Akcije</th>
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
              <td>{mapirajUlogu(prodavac.tip)}</td>
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
              <td>
                <button onClick={() => handleOdbij(prodavac.id)}>Odbij</button>
                <button onClick={() => handlePrihvati(prodavac.id)}>Prihvati</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to ="/dashboard"> Nazad </Link>
    </div>
  );
};

export default CekanjeVerifikacije;
