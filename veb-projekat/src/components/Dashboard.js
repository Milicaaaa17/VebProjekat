import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';
import './Dashboard.css';
import { odjaviKorisnika } from '../services/AuthService';
import { getKorisnikPoId } from '../services/AdminService';

const Dashboard = () => {
  const token = localStorage.getItem('token');
  const decodedToken = jwtDecode(token);
  const uloga = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  const id = decodedToken['Id'];
  const navigate = useNavigate();
  const [korisnik, setKorisnik] = useState(null);
  
  useEffect(() => {
    const getKorisnik = async () => {
      try {
        const response = await getKorisnikPoId(id);
        console.log(response);
       
        setKorisnik(response);
      } catch (error) {
        console.error('Greška prilikom dobijanja korisnika po ID-u:', error);
      }
    };
  
    if (uloga === 'Prodavac') {
      getKorisnik();
    }
  }, [id, uloga]);

 
  
  const handleLogout = () => {
    odjaviKorisnika();
    navigate('/');

  };

 
  return (
    <div className="dashboard-container">
      <ul className="dashboard-list">
        {uloga === 'Prodavac' &&  korisnik && korisnik.statusVerifikacije === 1 && korisnik.verifikovan &&(
          <>
           <div className="logout-container">
        <button className="logout-button" onClick={handleLogout}>Odjava</button>
      </div>
            <li className="dashboard-list-item">
              <Link to="/dodajArtikal" className="dashboard-link">Dodavanje artikla</Link>
            </li>
            <li className="dashboard-list-item">
              <Link to="/artikli" className="dashboard-link">Artikli</Link>
            </li>
            <li className="dashboard-list-item">
            <Link to= {`/profil/${id}`} className="dashboard-link">Profil</Link>
           </li>
           <li className="dashboard-list-item">
            <Link to= {`/mojePorudzbine/${id}`} className="dashboard-link">Moje porudzbine</Link>
           </li>
           <li className="dashboard-list-item">
            <Link to= {`/novePorudzbine/${id}`} className="dashboard-link">Nove porudzbine</Link>
           </li>
          </>
        )}
           {uloga === 'Prodavac' &&  korisnik && korisnik.statusVerifikacije === 0  &&(
          <>
           <h1>Vas profil nije aktivan! Sacekajte verifikaciju.</h1>
           <li className="dashboard-list-item">
            <Link to= {`/profil/${id}`} className="dashboard-link">Profil</Link>
           </li>
           <div className="logout-container">
        <button className="logout-button" onClick={handleLogout}>Odjava</button>
      </div>
          </>
        )}
        {uloga === 'Prodavac' &&  korisnik && korisnik.statusVerifikacije === 2  &&(
          <>
           <h1>Verifikacija odbijena! </h1>
           <li className="dashboard-list-item">
            <Link to= {`/profil/${id}`} className="dashboard-link">Profil</Link>
           </li>
            <Link to="/" className="dashboard-link">Pocetna</Link>
          </>
        )}
        {uloga === 'Kupac' && (
          <>
           <div className="logout-container">
        <button className="logout-button" onClick={handleLogout}>Odjava</button>
      </div>
            <li className="dashboard-list-item">
              <Link to="/dodajPorudzbinu" className="dashboard-link">Poruci</Link>
            </li>
            <li className="dashboard-list-item">
            <Link to= {`/prethodnePorudzbine/${id}`} className="dashboard-link">PrethodnePorudzbine</Link>
            </li>
            <li className="dashboard-list-item">
            <Link to= {`/profil/${id}`} className="dashboard-link">Profil</Link>
           </li>
          </>
        )}
        {uloga === 'Administrator' && (
          <>
           <div className="logout-container">
        <button className="logout-button" onClick={handleLogout}>Odjava</button>
           </div>
            <li className="dashboard-list-item">
              <Link to="/verifikacija" className="dashboard-link">Verifikacija</Link>
            </li>
            <li className="dashboard-list-item">
              <Link to="/korisnici" className="dashboard-link">Svi korisnici</Link>
            </li>
           <li className="dashboard-list-item">
            <Link to= {`/profil/${id}`} className="dashboard-link">Profil</Link>
           </li>
           <li className="dashboard-list-item">
              <Link to="/porudzbine" className="dashboard-link">Sve porudzbine</Link>
           </li>
           <li className="dashboard-list-item">
              <Link to="/cekanjeVerifikacije" className="dashboard-link">Verifikuj prodavce</Link>
            </li>
          </>
        )}
      </ul>
    </div>
  );
}

export default Dashboard;
