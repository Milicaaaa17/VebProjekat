import React, { useEffect, useState } from 'react';
import {  azurirajArtikal, obrisiArtikal, getSviArtikliProdavac } from '../services/ArtikalService';
import './Tabela.css';
import './Forma.css';
import { Link } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

const Artikal = () => {
  const [artikli, setArtikli] = useState([]);

  const [idArtikla, setid] = useState('');
    const [naziv, setNaziv] = useState('');
    const [cijena, setCijena] = useState('');
    const [kolicina, setKolicina] = useState('');
    const [opis, setOpis] = useState('');
    const [greske, setGreske] = useState([]);
    const [fotografija, setFotografija] = useState(null);
    const [uspjesno, setUspjesno] = useState(false);
    
    useEffect(() => {
      const token = localStorage.getItem('token');
      const decodedToken = jwtDecode(token);
      const prodavacid = decodedToken.id;

      const getArtikli = async () => {
        try {
          const artikliProdavca = await getSviArtikliProdavac(prodavacid);
          setArtikli(artikliProdavca);
        } catch (error) {
          console.log('Greška prilikom dohvata artikala:', error);
        }
      };
      getArtikli();
    }, []);


  const handleChange = (event) => {
    const { name, value } = event.target;
    switch (name) {
      case 'idArtikla':
        setid(value);
        break;
      case 'naziv':
        setNaziv(value);
        break;
      case 'cijena':
        setCijena(value);
        break;
      case 'kolicina':
        setKolicina(value);
        break;
      case 'opis':
        setOpis(value);
        break;
      default:
        break;
    }
  };
  
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!naziv || !cijena || !kolicina || !opis || !fotografija) {
      setGreske(['Molimo unesite sve potrebne podatke.']);
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

    
    if (!isNaN(naziv)) {
      setGreske(['Naziv ne moze biti broj']);
      setUspjesno(false);
      return;
    }

    try {

      const formData = new FormData();
      formData.append('idArtikla', idArtikla);
      formData.append('naziv', naziv);
      formData.append('cijena', cijena);
      formData.append('kolicina', kolicina);
      formData.append('opis', opis);
      formData.append('fotografija', fotografija);


      await azurirajArtikal(idArtikla, formData);

      const azuriraniArtikli = artikli.map((artikal) => {
        if (artikal.idArtikla === idArtikla) {
          return {
            ...artikal,
            idArtikla,
            naziv,
            cijena,
            kolicina,
            opis,
            fotografija,
          };
        }
        return artikal;
      });

      setArtikli(azuriraniArtikli);

      setid('');
      setNaziv('');
      setCijena('');
      setKolicina('');
      setOpis('');
      setFotografija(null);
      setGreske([]);
      setUspjesno(true);

      const token = localStorage.getItem('token');
      const decodedToken = jwtDecode(token);
      const prodavacid = decodedToken.id;
      const resp = await getSviArtikliProdavac(prodavacid);
      setArtikli(resp);

    } catch (error) {
      console.log('Greška:', error);
      setGreske(['Greska priliko azuriranja']);
      setUspjesno(false);
    }
  };

  const handleBrisi = async (id) => {
    try {
      await obrisiArtikal(id);
      const filtriraniArtikli = artikli.filter((artikal) => artikal.idArtikla !== id);
      setArtikli(filtriraniArtikli);
    } catch (error) {
      console.log('Greska prilikom brisanja:', error);
    }
  };

  return (
    <div className="tabela-container">
      <h2>Svi artikli</h2>
      <table>
        <thead>
          <tr>
            <th>id</th>
            <th>Naziv</th>
            <th>Cijena</th>
            <th>Količina</th>
            <th>Opis</th>
            <th>Slika</th>
            <th>Opcije</th>
          </tr>
        </thead>
        <tbody>
          {artikli && artikli.map((artikal) => (
            <tr key={artikal.idArtikla}>
              <td>{artikal.idArtikla}</td>
              <td>{artikal.naziv}</td>
              <td>{artikal.cijena}</td>
              <td>{artikal.kolicina}</td>
              <td>{artikal.opis}</td>
              <td>
            {artikal.fotografija && (
              <img
                src={`data:image/jpg;base64,${artikal.fotografija}`}
                alt="fotografija"
                style={{ width: '80px', height: '80px' }}
              />
            )}
          </td>
              <td>
                <button onClick={() => handleBrisi(artikal.idArtikla)}>Obriši</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
        
      <h2>Azuriranje artikala</h2>
      <div className="forma-container">
      <form onSubmit={handleSubmit}>
      <div>
          <label htmlFor="idArtikla">id:</label>
          <input type="text" id="idArtikla" name="idArtikla" value={idArtikla} onChange={handleChange}/>
          </div>
        <div>
          <label htmlFor="naziv">Naziv:</label>
          <input type="text" id="naziv" name="naziv" value={naziv} onChange={handleChange}/>
        </div>
        <div>
          <label htmlFor="cijena">Cijena:</label>
          <input type="number" id="cijena" name="cijena" value={cijena} onChange={handleChange}/>
        </div>
        <div>
          <label htmlFor="kolicina">Količina:</label>
          <input type="number" id="kolicina" name="kolicina" value={kolicina} onChange={handleChange}/>
        </div>
        <div>
          <label htmlFor="opis">Opis:</label>
          <textarea id="opis" name="opis" value={opis} onChange={handleChange}></textarea>
        </div>

        <div>
          <label>Slika:</label>
          <input type="file" accept="image/*" onChange={(e) => setFotografija(e.target.files[0])} />
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
            <p>Uspjesno azuriran artikal!</p>
          </div>
        )}
        <button type="submit">Azuriraj</button>
      </form>
      </div>

      <Link to ="/dashboard"> Nazad </Link>
    </div>
  );
};

export default Artikal;
