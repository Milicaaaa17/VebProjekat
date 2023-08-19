import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {registrujKorisnika} from '../services/RegistracijaService';


const Registracija = () => {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [email, setEmail] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [ponoviLozinku, setPonoviLozinku] = useState('');
  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [datumRodjenja, setDatumRodjenja] = useState('');
  const [adresa, setAdresa] = useState('');
  const [tip, setTip] = useState('');

  const [slika, setSlika] = useState(null);
  const [greske, setGreske] = useState([]);
  const [uspjesnaRegistracija, setUspjesnaRegistracija] = useState(false);



  const navigate = useNavigate();

    const enumeracija = {
    Kupac: 1,
    Prodavac: 2,
  }

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!korisnickoIme || !ime || !prezime || !email || !lozinka || !ponoviLozinku || !adresa || !datumRodjenja) {
      setGreske(['Molimo unesite sve potrebne podatke']);
      setUspjesnaRegistracija(false);
      return;
    }

    if (lozinka !== ponoviLozinku) {
      setGreske(['Lozinka i ponovljena lozinka se ne podudaraju']);
      setUspjesnaRegistracija(false);
      return;
    }

    if (lozinka.length  < 8 ) {
      setGreske(['Lozinka mora imati barem 8 karaktera']);
      setUspjesnaRegistracija(false);
      return;
    }
     // Validate birthdate
  const today = new Date();
  const selectedDate = new Date(datumRodjenja);
  if (selectedDate > today) {
    setGreske(['Datum rođenja ne može biti u budućnosti']);
    setUspjesnaRegistracija(false);
    return;
  }
    if (!slika) {
      setGreske(['Morate odabrati fotografiju']);
      setUspjesnaRegistracija(false);
      return;
    }

    if (!isNaN(ime) || !isNaN(prezime)) {
      setGreske(['Ime i prezime ne mogu biti brojevi']);
      setUspjesnaRegistracija(false);
      return;
    }

    if (!tip) {
      setGreske(['Morate odabrati ulogu']);
      setUspjesnaRegistracija(false);
      return;
    }

    const enumVrijednost = enumeracija[tip]; 
    const formData = new FormData();
    formData.append('korisnickoIme', korisnickoIme);
    formData.append('email', email);
    formData.append('lozinka', lozinka);
    formData.append('ponoviLozinku', ponoviLozinku);
    formData.append('ime', ime);
    formData.append('prezime', prezime);
    formData.append('datumRodjenja', datumRodjenja);
    formData.append('adresa', adresa);
    formData.append('tip', enumVrijednost);
    formData.append('slika', slika);
   

  
    try {
      const response = await registrujKorisnika(formData);
      console.log('Odgovor od servera nakon registracije:', response); 
      navigate('/login');
      if (response && response.uspjesnaRegistracija) {
        setUspjesnaRegistracija(true);
        setKorisnickoIme('');
        setEmail('');
        setLozinka('');
        setPonoviLozinku('');
        setIme('');
        setPrezime('');
        setDatumRodjenja('');
        setAdresa('');
        setTip('');
        setSlika(null);
        setGreske([]);
        console.log('Registracija uspješna:', uspjesnaRegistracija);
        navigate('/login');
      } else {
        setUspjesnaRegistracija(false);
        if (response && response.errors) {
          setGreske(response.errors);
        } else {
          setGreske('Greška prilikom registracije');
        }
      }
    } catch (error) {
      console.error('Greška prilikom registracije:', error);
      setUspjesnaRegistracija(false);
      setGreske(['Korisničko ime ili email već postoje']);
    }
};
  return (
    <div className="forma-container">
      <h2>Registracija</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Korisničko ime:</label>
          <input type="text" value={korisnickoIme} onChange={(e) => setKorisnickoIme(e.target.value)} />
        </div>
        <div>
          <label>Email:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
        <div>
          <label>Lozinka:</label>
          <input type="password" value={lozinka} onChange={(e) => setLozinka(e.target.value)} />
        </div>
        <div>
          <label>Ponovi lozinku:</label>
          <input type="password" value={ponoviLozinku} onChange={(e) => setPonoviLozinku(e.target.value)} />
        </div>
        <div>
          <label>Ime:</label>
          <input type="text" value={ime} onChange={(e) => setIme(e.target.value)} />
        </div>
        <div>
          <label>Prezime:</label>
          <input type="text" value={prezime} onChange={(e) => setPrezime(e.target.value)} />
        </div>
        <div>
          <label>Datum rođenja:</label>
          <input type="date" value={datumRodjenja} onChange={(e) => setDatumRodjenja(e.target.value)} />
        </div>
        <div>
          <label>Adresa:</label>
          <input type="text" value={adresa} onChange={(e) => setAdresa(e.target.value)} />
        </div>
        <div>
        <label>Tip:</label>
        <select name="tip" value={tip} onChange={(e) => setTip(e.target.value)}>
          <option value="">Odaberi tip</option>
         
          <option value="Kupac">Kupac</option>
          <option value="Prodavac">Prodavac</option>
        </select>
        </div>
      
        <div>
          <label>Slika:</label>
          <input type="file" accept="image/*" onChange={(e) => setSlika(e.target.files[0])} />
        </div>
        
        {Array.isArray(greske) && greske.length > 0 && (
          <div className="error-container">
            <ul>
              {greske.map((greska, index) => (
              <li key={index}>{greska}</li>
               ))}
           </ul>
      </div>
        )}

{uspjesnaRegistracija && (
  <div className="success-container">
    <p>Registracija uspjesna!</p>
  </div>
)}
{uspjesnaRegistracija && navigate('/login')}
   
        <button type="submit">Registracija</button>
      </form>
      <Link to="/login">Imate nalog? Prijavite se!</Link>
    </div>
  );
};

export default Registracija;
