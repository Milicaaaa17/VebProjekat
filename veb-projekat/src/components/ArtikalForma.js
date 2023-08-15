import React, { useState } from 'react';
import './Forma.css';
import { kreirajArtikal } from '../services/AdminService';
import { Link } from 'react-router-dom';


const ArtikalForma = () => {
  const [naziv, setNaziv] = useState('');
  const [cijena, setCijena] = useState('');
  const [kolicina, setKolicina] = useState('');
  const [opis, setOpis] = useState('');
  const [fotografija, setFotografija] = useState(null);
  const [greske, setGreske] = useState([]);
  const [uspjesno, setUspjesno] = useState(false);

  const handleFormSubmit = async (event) => {
    event.preventDefault();
  
 
    if (!naziv || !cijena || !kolicina || !opis || !fotografija) {
      setGreske(['Molimo unesite sve potrebne podatke.']);
      setUspjesno(false);
      return;
    }

    if (!isNaN(naziv)) {
      setGreske(['Naziv ne moze biti broj']);
      setUspjesno(false);
      return;
    }

    if (cijena < 0) {
      setGreske(['Cijena ne moze biti negativni brojevi.']);
      setUspjesno(false);
      return;
    }

    if (kolicina < 0) {
      setGreske([' kolicina ne moze biti negativni brojevi.']);
      setUspjesno(false);
      return;
    }

    if (!fotografija) {
      alert('Molimo odaberite sliku.');
      return;
    }
  
    const formData = new FormData();
    formData.append('naziv', naziv);
    formData.append('cijena', cijena);
    formData.append('kolicina', kolicina);
    formData.append('opis', opis);
    formData.append('fotografija', fotografija);
    

try {
  await kreirajArtikal(formData);

  setNaziv('');
  setCijena('');
  setKolicina('');
  setOpis('');
  setFotografija('');

  setUspjesno(true);
  setGreske([]);
} catch (error) {
  console.log('Greska:', error);
    setGreske([error.message]);
    setUspjesno(false);
}
};

  return (
    <div className="forma-container">
      <h2>Unos novog artikla</h2>
      <form onSubmit={handleFormSubmit}>
        <div>
          <label>Naziv:</label>
          <input type="text" value={naziv} onChange={(e) => setNaziv(e.target.value)}/>
        </div>
        <div>
          <label>Cijena:</label>
          <input type="number" step="0.01" value={cijena}  onChange={(e) => setCijena(e.target.value)}
          />
        </div>
        <div>
          <label>Količina:</label>
          <input type="number" value={kolicina} onChange={(e) => setKolicina(e.target.value)}
          />
        </div>
        <div>
          <label>Opis:</label>
          <textarea value={opis}  onChange={(e) => setOpis(e.target.value)}></textarea>
        </div>
        <div>
          <label>Fotografija:</label>
          <input type="file" accept="image/*" onChange={(e) => setFotografija(e.target.files[0])}
          />
        </div>
        {greske.length > 0 && (
          <div className="error-container">
            <ul>
              {greske.map((greska, index) => (
                <li key={index}>{greska}</li>
              ))}
            </ul>
          </div>
        )}
          
        {uspjesno && (
          <div className="success-container">
            <p>Uspjesno ste dodali novi artikal</p>
          </div>
        )}

        <button type="submit">Dodaj artikal</button>
      </form>
      <Link to ="/dashboard"> Nazad </Link>
    </div>
  );
};

export default ArtikalForma;
