import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Pocetna from './components/Pocetna';
import Login from './components/Login';
import Registracija from './components/Registracija';
import Dashboard from './components/Dashboard';
import Admin from './components/Admin';
import Artikal from './components/Artikal';
import ArtikalForma from './components/ArtikalForma';
import Profil from './components/Profil';
import Navigacija from './components/Navigacija';
import Verifikacija from './components/Verifikacija';
import CekanjeVerifikacije from './components/CekanjeVerifikacije';


function App() {
  return (
    <BrowserRouter>
      <Routes>
      <Route path="/" element={<Pocetna />} />
        <Route path="/login" element={<Login />} />
        <Route path="/registracija" element={<Registracija />} />
        <Route path="/profil/:id" element={<Profil />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/korisnici" element={<Admin />} />
        <Route path="/artikli" element={<Artikal />} />
        <Route path="/dodajArtikal" element={<ArtikalForma />} />
        <Route path="/navigacija" element={<Navigacija />} />
        <Route path="/verifikacija" element={<Verifikacija />} />
        <Route path="/cekanjeVerifikacije" element={<CekanjeVerifikacije />} />

      
        
      </Routes>
    </BrowserRouter>
  );
}

export default App;
