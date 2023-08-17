import React, { useEffect, useState } from 'react';
import { getMojePorudzbineProdavac } from '../services/PorudzbinaService';
import './Tabela.css';
import { Link, useNavigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

const MojePorudzbine = () => {
  const [porudzbine, setPorudzbine] = useState([]);
  const navigate = useNavigate();

 
  useEffect(() => {
    const token = localStorage.getItem('token');
    const decodedToken = jwtDecode(token);
    const id = decodedToken['Id'];
    const get = async () => {
        
      const prodavacId = id;
      const resp = await getMojePorudzbineProdavac(prodavacId);
      setPorudzbine(resp);
      
    };
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
    navigate(`/detaljiProdavca/${porudzbinaId}`);
  };

  return (
      <div className="tabela-container">
      <h2>Moje porudzbine</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Komentar</th>
            <th>Adresa dostave</th>
            <th>Vrijeme dostave</th>
            <th>Status porudzbine</th>
            <th>Opcije</th>
          </tr>
        </thead>
        <tbody>
          {porudzbine.map((porudzbina) => (
            <tr key={porudzbina.idPorudzbine}>
              <td>{porudzbina.idPorudzbine}</td>
              <td>{porudzbina.komentar}</td>
              <td>{porudzbina.adresaDostave}</td>
              <td>{porudzbina.vrijemeDostave}</td>
              <td>{mapirajStatus(porudzbina.status)}</td>
              <td>
                <button onClick={() => prikaziDetaljePorudzbine(porudzbina.idPorudzbine)}>Detalji</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      
      <Link to="/dashboard">Nazad</Link>
    </div>
    
  );
};

export default MojePorudzbine;

