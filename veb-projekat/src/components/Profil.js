import React, { useEffect, useState } from 'react';
import { getKorisnikId, azurirajKorisnika, getStatusProcesaVerifikacije } from '../services/KorisnikProfilService';
import { Link, useParams } from 'react-router-dom';
import './Tabela.css';
import './Forma.css';



const Profil = () => {
    const { id } = useParams();
    const [korisnik, setKorisnik] = useState(null);
   


    const [korisnickoIme, setKorisnickoIme] = useState('');
    const [email, setEmail] = useState('');
    const [ime, setIme] = useState('');
    const [prezime, setPrezime] = useState('');
    const [lozinka, setLozinka] = useState('');
    const [ponovljenaLozinka, setPonovljenaLozinka] = useState('');
    const [adresa, setAdresa] = useState('');
    const [datumRodjenja, setDatumRodjenja] = useState('');
    const [slika, setSlika] = useState(null);
    const [greske, setGreske] = useState([]);
    const [uspjesno, setUspjesno] = useState(false);
    const [statusVerifikacije, setStatusVerifikacije] = useState('');
    

    useEffect(() => {
      const get = async () => {
        try {
          const resp = await getKorisnikId(id);
          console.log(resp);
          setKorisnik(resp);
        } catch (error) {
          console.error(error);
        }
      };
  
      get();
    }, [id]);

    const azuriranje = async () => {
      try {
        const resp = await getKorisnikId(id);
        console.log(resp);
        setKorisnik(resp);
      } catch (error) {
        console.error(error);
      }
    };
   

    useEffect(() => {
      const getStatus = async () => {
        try {
          const status = await getStatusProcesaVerifikacije(id);
          setStatusVerifikacije(status);
        } catch (error) {
          console.error(error);
        }
      };
  
      getStatus();
    }, [id]);

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

  const handleChange = (event) => {
    const { name, value } = event.target;
    switch (name) {
      case 'korisnickoIme':
        setKorisnickoIme(value);
        break;
      case 'email':
        setEmail(value);
        break;
      case 'ime':
        setIme(value);
        break;
      case 'prezime':
        setPrezime(value);
        break;
      case 'lozinka':
        setLozinka(value);
        break;
      case 'ponovljenaLozinka':
        setPonovljenaLozinka(value);
        break;
      case 'adresa':
        setAdresa(value);
        break;
      case 'datumRodjenja':
        setDatumRodjenja(value);
        break;
      default:
        break;
    }
  };
  

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!korisnickoIme || !ime || !prezime || !email || !lozinka || !ponovljenaLozinka || !adresa || !datumRodjenja  || !slika ) {
      setGreske(['Molimo unesite sve potrebne podatke']);
      setUspjesno(false);
      return;
    }

    if (lozinka !== ponovljenaLozinka) {
      setGreske(['Lozinka i ponovljena lozinka se ne podudaraju']);
      setUspjesno(false);
      return;
    }

    if (lozinka.length  < 8 ) {
      setGreske(['Lozinka mora imati barem 8 karaktera']);
      setUspjesno(false);
      return;
    }

    if (!isNaN(ime) || !isNaN(prezime)) {
      setGreske(['Ime i prezime ne mogu biti brojevi']);
      setUspjesno(false);
      return;
    }
   
    try {

    
      const formData = new FormData();
      formData.append('id', id);
      formData.append('korisnickoIme', korisnickoIme);
      formData.append('email', email);
      formData.append('lozinka', lozinka);
      formData.append('ponovljenaLozinka', ponovljenaLozinka);
      formData.append('ime', ime);
      formData.append('prezime', prezime);
      formData.append('datumRodjenja', datumRodjenja);
      formData.append('adresa', adresa);
      formData.append('slika', slika);

   
      await azurirajKorisnika(id, formData);

      setKorisnik({
        ...korisnik,
        id,
        korisnickoIme,
        email,
        ime,
        prezime,
        adresa,
        datumRodjenja,
        slika,
      });

      
      setKorisnickoIme('');
      setEmail('');
      setLozinka('');
      setPonovljenaLozinka('');
      setIme('');
      setPrezime('');
      setDatumRodjenja('');
      setAdresa('');
      setSlika(null);
     
   
      setGreske([]);
      setUspjesno(true);

      azuriranje();
    } catch (error) {
      console.log('Greška:', error);
      setGreske(['Niste ispravno unijeli podatke, pokusajte ponovo!']);
      setUspjesno(false);
    }
  };

  
  console.log(korisnik);
  if (!korisnik) {
    return  <div>..</div>
  }
  return (
    <div className="tabela-container">
      <h2>Profil</h2>
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
            <th>Slika</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{korisnik.id}</td>
            <td>{korisnik.korisnickoIme}</td>
            <td>{korisnik.email}</td>
            <td>{korisnik.ime}</td>
            <td>{korisnik.prezime}</td>
            <td>{mapirajUlogu(korisnik.uloga)}</td>
            <td>{korisnik.adresa}</td>
            <td>{korisnik.datumRodjenja}</td>
            <td>
            {korisnik.slika && (
              <img
                src={`data:image/jpg;base64,${korisnik.slika}`}
                alt="Profilna slika"
                style={{ width: '80px', height: '80px' }}
              />
            )}
          </td>
          </tr>
        </tbody>
      </table>


      {statusVerifikacije && (
        <p style={{ backgroundColor: '#4caf50', padding: '10px', borderRadius: '5px' }}>
          Status verifikacije korisnika je: {statusVerifikacije}
        </p>
      )}

      
      <h2>Azuriranje podataka</h2>
    <div className="forma-container">
      <form onSubmit={handleSubmit}>
    
      <div>
        <label htmlFor="korisnickoIme">Korisniko Ime:</label>
        <input type="text" id="korisnickoIme" name="korisnickoIme" value={korisnickoIme} onChange={handleChange} />
      </div>
     
      <div>
        <label htmlFor="email">Email:</label>
        <input type="email" id="email" name="email" value={email} onChange={handleChange}/>
      </div>
     
      <div>
        <label htmlFor="ime">Ime:</label>
        <input type="text" id="ime" name="ime" value={ime} onChange={handleChange} />
      </div>
      <div>
        <label htmlFor="prezime">Prezime:</label>
        <input type="text" id="prezime" name="prezime" value={prezime} onChange={handleChange} />
      </div>
      <div>
          <label htmlFor="lozinka">Lozinka:</label>
          <input type="password" id="lozinka" name="lozinka" value={lozinka} onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="ponovljenaLozinka">Ponovite lozinku:</label>
          <input type="password" id="ponovljenaLozinka" name="ponovljenaLozinka" value={ponovljenaLozinka} onChange={handleChange} />
        </div>
      <div>
        <label htmlFor="adresa">Adresa:</label>
        <input type="text" id="adresa" name="adresa" value={adresa} onChange={handleChange} />
      </div>
      <div>
        <label htmlFor="datumRodjenja">Datum rođenja:</label>
        <input type="date" id="datumRodjenja" name="datumRodjenja" value={datumRodjenja} onChange={handleChange} />
      </div>
      <div>
          <label>Slika:</label>
          <input type="file" accept="image/*" onChange={(e) => setSlika(e.target.files[0])} />
        </div>
     
      <input type="hidden" name="id" value={id} />
    


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
            <p>Uspjesno azurirani podaci!</p>
          </div>
        )}
      <button type="submit">Azuriraj</button>
    </form>
    </div>
    <Link to="/dashboard">Nazad</Link>
    </div>
  );
  
};


export default Profil;

