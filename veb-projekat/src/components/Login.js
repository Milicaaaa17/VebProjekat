import React, { useState } from 'react';
import { prijaviKorisnika,prijavaPrekoGoogle } from '../services/LoginService';
import { Link, useNavigate } from 'react-router-dom';
import './Forma.css';
import { setAuthorizationHeader } from '../services/AuthService';
import { GoogleLogin } from '@react-oauth/google';
import { GoogleOAuthProvider } from '@react-oauth/google';
const Login = () => {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [email, setEmail] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [greske, setGreske] = useState([]);
  const [uspjesno, setUspjesno] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!korisnickoIme || !email || !lozinka) {
      setGreske(['Unesite sve podatke']);
      setUspjesno(false);
      return;
    }

    if (lozinka.length  < 8 ) {
      setGreske(['Lozinka mora imati barem 8 karaktera']);
      setUspjesno(false);
      return;
    }

    try {
      const podaci = {
        korisnickoIme,
        email,
        lozinka,
      };

      const { token } = await prijaviKorisnika(podaci);

      setKorisnickoIme('');
      setEmail('');
      setLozinka('');

      localStorage.setItem('token', token);
      setAuthorizationHeader(token);

      console.log('Prijava uspešna! Token:', token);
      setUspjesno(true);
      setGreske([]);
      navigate('/dashboard');
    } catch (error) {
      console.log('Greška prilikom prijave:', error);
      setGreske(['Niste ispravno popunili podatke']);
      if (error.response) {
        console.log('Greška od servera:', error.response.data.poruka);
        setGreske([error.response.data.poruka]);
      } else if (error.request) {
        console.log('Nema odgovora od servera.');
      } else {
        console.log('Greška pri slanju zahteva.');
      }
    }
  };
  const handlePrijava = async (data) => {
    try {

      const fd = new FormData();
   
      fd.append('googleToken', data.credential);
      console.log(data.credential);

      const token = await prijavaPrekoGoogle(fd);
      localStorage.setItem('token', token.data);
      setAuthorizationHeader(token.data);

      navigate('/dashboard');
     
    } catch (error) {
      console.log(error);
      setGreske([error.message]);
    }
  };
  
  const handleGoogleError = (error) => {
    console.log(error);
    setGreske([error.message]);
  };
  return (
    <div className="forma-container">
      <h2>Prijava</h2>
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
            <p>Prijava uspešna!</p>
          </div>
        )}
        <button type="submit">Prijavi se</button>
      </form>

      <GoogleOAuthProvider clientId="522310654948-8pcqa4pm64ks9tkccdk4qe3hef6eiqvi.apps.googleusercontent.com">
      <GoogleLogin onSuccess={handlePrijava} onError={handleGoogleError} 
      />
      </GoogleOAuthProvider>

      <br />
      <Link to="/registracija">Nemate nalog? Registruj se!</Link>
    </div>
  );
};

export default Login;
