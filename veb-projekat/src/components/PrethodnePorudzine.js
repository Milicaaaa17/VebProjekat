import React, { useEffect, useState } from 'react';
import { getPrethodnePorudzbine } from '../services/PorudzbinaService';
import './Tabela.css';
import { Link, useNavigate} from 'react-router-dom';
import jwtDecode from 'jwt-decode';


const PrethodnePorudzbine = () => {
  const [porudzbine1, setPorudzbine1] = useState([]);
  const token = localStorage.getItem('token');
  const decodedToken = jwtDecode(token);
  const id = decodedToken['Id'];
  const navigate = useNavigate();
 
  useEffect(() => {
    const token = localStorage.getItem('token');
    const decodedToken = jwtDecode(token);
    const id = decodedToken['Id'];
    const get = async () => {
        
      const kupacId = id;
      const resp1 = await getPrethodnePorudzbine(kupacId);
      setPorudzbine1(resp1);
      
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
    navigate(`/detalji/${porudzbinaId}`);
  };
  
  return (
      <div className="tabela-container">
      <h2>Prethodne porudzbine kupca</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Komentar</th>
            <th>Ukupna Cijena</th>
            <th>Cijena Dostave</th>
            <th>Adresa dostave</th>
            <th>Vrijeme dostave</th>
            <th>Status porudžbine</th>
            <th>Opcije</th>
            
          </tr>
        </thead>
        <tbody>
          {porudzbine1.map((porudzbina1) => (
            <tr key={porudzbina1.idPorudzbine}>
              <td>{porudzbina1.idPorudzbine}</td>
              <td>{porudzbina1.komentar}</td>
             
              <td>{porudzbina1.adresaDostave}</td>
              <td>{porudzbina1.vrijemeDostave}</td>
              <td>{mapirajStatus(porudzbina1.status)}</td>
              <td>
                <button onClick={() => prikaziDetaljePorudzbine(porudzbina1.idPorudzbine)}>Detalji</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Link to={`/otkaziPorudzbinu/${id}`}>otkaziPorudzbinu</Link>
      <Link to="/dashboard">Nazad</Link>
    </div>
    
  );
};

export default PrethodnePorudzbine;

