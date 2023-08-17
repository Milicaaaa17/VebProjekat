
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { kreirajPorudzbinu, getVrijemeDostave } from '../services/PorudzbinaService';
import { getSviArtikli } from '../services/ArtikalService';


import './Forma.css';
import './Tabela.css';

const PorudzbinaForma = () => {
  const [komentar, setKomentar] = useState('');
  const [adresaDostave, setAdresaDostave] = useState('');
  const [stavke, setStavke] = useState([{ idArtikla: '', kolicina: 0 }]);
  const [greske, setGreske] = useState([]);
  const [uspjesno, setUspjesno] = useState(false);
  const [artikli, setArtikli] = useState([]);
  const [poruka, setPoruka] = useState('');



  useEffect(() => {
    const getArtikli = async () => {
      const response = await getSviArtikli();
      setArtikli(response.data);
    };

    getArtikli();
  }, []);

  const azurirani = async () => {
    const response = await getSviArtikli();
    setArtikli(response.data);
  };

  const handleFormSubmit = async (event) => {
    event.preventDefault();

    if (!komentar || !adresaDostave || stavke.length === 0) {
      setGreske(['Molimo unesite sve potrebne podatke.']);
      setUspjesno(false);
      return;
    }

    if (stavke.some((stavka) => stavka.kolicina <= 0)) {
      setGreske(['Količina mora biti veća od 0 za sve stavke.']);
      setUspjesno(false);
      return;
    }

    try {
      const novaPorudzbina = {
        komentar,
        adresaDostave,
        stavke,
      };

      

      const id = await kreirajPorudzbinu(novaPorudzbina);
      console.log(id);
      const vrijemeDostave = await getVrijemeDostave(id);

  
      setKomentar('');
      setAdresaDostave('');
      setStavke([{ idArtikla: '', kolicina: 0 }]);
      
  
      setUspjesno(true);
      setGreske([]);
      setPoruka(`Kreirali ste porudzbine. Vrijeme dostave je ${vrijemeDostave}.`);
      azurirani();

     
      
    } catch (error) {
      console.log('Greška:', error);
      setGreske([error.message]);
      setUspjesno(false);
    }
  };


  const handleChange = (event, index) => {
    const { name, value } = event.target;
    const noviStavke = [...stavke];
    noviStavke[index][name] = value;
    setStavke(noviStavke);
  };

  const handleDodajStavku = () => {
    setStavke([...stavke, { idArtikla: '', kolicina: 0 }]);
  };

  const handleObrisiStavku = (index) => {
    const noviStavke = [...stavke];
    noviStavke.splice(index, 1);
    setStavke(noviStavke);
  };

 

  return (
    <div className="forma-container">
      <h2>Unos nove porudžbine</h2>
      <form onSubmit={handleFormSubmit}>
        <div>
          <label>Komentar:</label>
          <input type="text" name="komentar" value={komentar} onChange={(event) => setKomentar(event.target.value)} />
        </div>
        <div>
          <label>Adresa dostave:</label>
          <input type="text" name="adresaDostave" value={adresaDostave} onChange={(event) => setAdresaDostave(event.target.value)} />
        </div>

        <h3>Stavke:</h3>
        {stavke.map((stavka, index) => (
          <div key={index}>
            <label>Artikli:</label>
            <select name="idArtikla" value={stavka.idArtikla} onChange={(event) => handleChange(event, index)}>
              <option value="">Odaberite artikal</option>
              {artikli.map((artikal) => (
                <option key={artikal.idArtikla} value={artikal.idArtikla}>
                  {artikal.naziv}
                </option>
              ))}
            </select>
            <label>Količina:</label>
            <input type="number" name="kolicina" value={stavka.kolicina} min="1" onChange={(event) => handleChange(event, index)}/>
            {index === stavke.length - 1 && (
              <button type="button" onClick={handleDodajStavku}>
                Dodaj stavku
              </button>
            )}
            {index !== 0 && (
              <button type="button" onClick={() => handleObrisiStavku(index)}>
                Obriši stavku
              </button>
            )}
          </div>
        ))}

        {greske.length > 0 && (
          <div className="error-container">
            <ul>
              {greske.map((greska, index) => (
                <li key={index}>{greska}</li>
              ))}
            </ul>
          </div>
        )}

        <button type="submit">Dodaj porudžbinu</button>
      </form>

    <div className="tabela-container">
      <h2>Svi artikli</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Naziv</th>
            <th>Cijena</th>
            <th>Količina</th>
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
              <td>{artikal.kolicina}</td>
              <td>{artikal.opis}</td>
              <td>
            {artikal.slika && (
              <img
                src={`data:image/jpg;base64,${artikal.slika}`}
                alt="slika"
                style={{ width: '30px', height: '30px' }}
              />
            )}
          </td>
             
            </tr>
          ))}
        </tbody>
      </table>
        {poruka && (
      <p style={{ backgroundColor: '#4caf50', padding: '10px', borderRadius: '5px' }}>
        {poruka}
      </p>
)}

      <Link to="/dashboard">Nazad</Link>
    </div>
    </div>
  );
};

export default PorudzbinaForma;

