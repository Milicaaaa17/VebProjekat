import React, { useEffect, useState } from 'react';
import { getSvePorudzbine } from '../services/PorudzbinaService';
import './Tabela.css';
import { Link, useNavigate } from 'react-router-dom';


const Porudzbina = () => {
  const [porudzbine, setPorudzbine] = useState([]);
  const navigate = useNavigate();
  
  useEffect(() =>
  {
      const get = async()=>
      {
          const resp = await getSvePorudzbine();
          console.log(resp);
          setPorudzbine(resp.data);
      }
      get();
  }, []);

  const mapirajStatus = (status) => {
    switch (status) {
      case 0:
        return 'Obrada';
      case 1:
        return 'Odobrena';
      case 2:
        return 'Odbijena';
      case 3:
        return 'Isporucena';
      case 4:
        return 'Otkazana';
      default:
        return '';
    }
  };

  const prikaziDetaljePorudzbine = (porudzbinaId) => {
  navigate(`/detalji/${porudzbinaId}`);
};


return (
    <div className="tabela-container">
      <h2>Sve porudžbine</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Komentar</th>
            <th>Cijena</th>
            <th>Cijena dostave</th>
            <th>Adresa dostave</th>
            <th>Vrijeme dostave</th>
            <th>Status porudzbine</th>
            <th>Id kupca</th>
            <th>Opcije</th>
          </tr>
        </thead>
        <tbody>
          {porudzbine.map((porudzbina) => (
            <tr key={porudzbina.idPorudzbine}>
              <td>{porudzbina.idPorudzbine}</td>
              <td>{porudzbina.komentar}</td>
              <td>{porudzbina.ukupnaCijena}</td>
              <td>{porudzbina.cijenaDostave}</td>
              <td>{porudzbina.adresaDostave}</td>
              <td>{porudzbina.vrijemeDostave}</td>
              <td>{mapirajStatus(porudzbina.status)}</td>
              <td>{porudzbina.korisnikId}</td>
              <td>
                <button onClick={() => prikaziDetaljePorudzbine(porudzbina.idPorudzbine)}>Detalji</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to ="/dashboard"> Nazad </Link>
    </div>
  );
};

export default Porudzbina;
