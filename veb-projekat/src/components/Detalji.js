import React, { useEffect, useState } from 'react';
import { getSvePorudzbineKupca, getArtiklePorudzbine } from '../services/PorudzbinaService';
import { Link, useParams } from 'react-router-dom';
import PrethodnePorudzbine from './PrethodnePorudzine';



const Detalji = () => {
  const [artikli, setArtikli] = useState([]);
  const { id } = useParams(); 



  useEffect(() => {
    const get = async () => {
      try {
        const resp = await getArtiklePorudzbine(id); 
        console.log(resp);
        console.log(resp.data);
        setArtikli(resp); 
       
        
      } catch (error) {
        console.error('Greska prilikom dobavljanja artikala porudzbine:', error);
      }
    };
    get();
  }, [id]);


  return (
    <div className="tabela-container">
      <h2>Artikli koje ste narucili</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Naziv</th>
            <th>Cijena</th>
           
            <th>Opis</th>
            <th>Slika</th>
          </tr>
        </thead>
        <tbody>
        {artikli.map((artikal) => (
            <tr key={artikal.idArtikla}>
              <td>{artikal.idArtikla}</td>
              <td>{artikal.naziv}</td>
              <td>{artikal.cijena}</td>
              
              <td>{artikal.opis}</td>
              <td>
            {artikal.slika && (
              <img
                src={`data:image/jpg;base64,${artikal.slika}`}
                alt="slika"
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

export default Detalji;
