import React, { useEffect, useState } from 'react';
import { getSvePorudzbineKupca, otkaziPorudzbinu } from '../services/PorudzbinaService';
import './Tabela.css';
import { Link, useNavigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

const OtkaziPorudzbinu = () => {
  const [porudzbine, setPorudzbine] = useState([]);
  const [poruka, setPoruka] = useState('');
  const navigate = useNavigate();

 

  useEffect(() => {
    const token = localStorage.getItem('token');
    const decodedToken = jwtDecode(token);
    const id = decodedToken['Id'];
    const get = async () => {
        
      const kupacId = id;
      const resp = await getSvePorudzbineKupca(kupacId);
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


  const handleOtkaziPorudzbinu = async (porudzbinaId) => {
    try {
      const response = await otkaziPorudzbinu(porudzbinaId);
      
      const token = localStorage.getItem('token');
      const decodedToken = jwtDecode(token);
      const id = decodedToken['Id'];
      const resp = await getSvePorudzbineKupca(id);
      setPorudzbine(resp);
      console.log(response);
     
      setPoruka(response);
    } catch (error) {
      setPoruka("Isteklo je vreme za otkazivanje porudzbine.");
    }
  };

  const prikaziDetaljePorudzbine = (porudzbinaId) => {
    navigate(`/detalji/${porudzbinaId}`);
  };

  return (
    <div className="tabela-container">
      <h2>Sve porudzbine kupca</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Komentar</th>
            <th>Adresa dostave</th>
            <th>Cijena dostave</th>
            <th>Ukupna cijena </th>
            <th>Vrijeme dostave</th>
            <th>Status porudžbine</th>
            <th>Opcije</th> 
            <th>Detalji</th> 
          </tr>
        </thead>
        <tbody>
          {porudzbine.map((porudzbina) => (
            <tr key={porudzbina.idPorudzbine}>
              <td>{porudzbina.idPorudzbine}</td>
              <td>{porudzbina.komentar}</td>
              <td>{porudzbina.adresaDostave}</td>
              <td>200</td>
              <td>{porudzbina.ukupnaCijena}</td>
              <td>{porudzbina.vrijemeDostave}</td>
              <td>{mapirajStatus(porudzbina.status)}</td>
              <td>
                {porudzbina.status !== 4 && ( 
                  <button onClick={() => handleOtkaziPorudzbinu(porudzbina.idPorudzbine)}>Otkazi</button>
                )}
              </td>
              <td>
                <button onClick={() => prikaziDetaljePorudzbine(porudzbina.idPorudzbine)}>Detalji</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <p style={{ backgroundColor: '#4caf50', padding: '10px', borderRadius: '5px' }}>
        {poruka}
      </p>

      <Link to="/dashboard">Nazad</Link>
     
    </div>
    
  );
};

export default OtkaziPorudzbinu;

