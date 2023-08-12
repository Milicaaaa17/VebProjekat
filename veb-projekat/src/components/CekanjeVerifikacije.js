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

  const mapirajUlogu = (uloga) => {
    switch (uloga) {
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

  const handleOdbij = async (prodavacId) => {
    try {
      const response = await odbijRegistracijuProdavca(prodavacId);
      console.log(response); 
      setProdavci(prodavci.filter((prodavac) => prodavac.id !== prodavacId));
      azurirani();
    } catch (error) {
      console.error(error);
    }
  };

  const handlePrihvati = async (prodavacId) => {
    try {
      const response = await prihvatiRegistraciju(prodavacId);
      console.log(response);
      setProdavci(prodavci.filter((prodavac) => prodavac.id !== prodavacId));
      azurirani();
    } catch (error) {
      console.error(error);
    }
  };


  return (
    <div className="tabela-container">
      <h2>Verifikuj prodavce</h2>
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
