import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Pocetna from './components/Pocetna';
import Login from './components/Login';
import Registracija from './components/Registracija';
import Dashboard from './components/Dashboard';
import Admin from './components/Admin';
import Artikal from './components/Artikal';
import Porudzbina from './components/Porudzbina';
import ArtikalForma from './components/ArtikalForma';
import PorudzbinaForma from './components/PorudzbinaForma';
import Profil from './components/Profil';
import Navigacija from './components/Navigacija';
import Verifikacija from './components/Verifikacija';
import CekanjeVerifikacije from './components/CekanjeVerifikacije';
import PrethodnePorudzbine from './components/PrethodnePorudzine';
import OtkaziPorudzbinu from './components/OtkaziPorudzbinu';
import MojePorudzbine from './components/MojePorudzbine';
import NovePorudzbine from './components/NovePorudzbine';
import Detalji from './components/Detalji';



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
        <Route path="/porudzbine" element={<Porudzbina />} />
        <Route path="/dodajArtikal" element={<ArtikalForma />} />
        <Route path="/dodajPorudzbinu" element={<PorudzbinaForma />} />
        <Route path="/navigacija" element={<Navigacija />} />
        <Route path="/verifikacija" element={<Verifikacija />} />
        <Route path="/cekanjeVerifikacije" element={<CekanjeVerifikacije />} />
        <Route path="/prethodnePorudzbine/:id" element={<PrethodnePorudzbine />} />
        <Route path="/otkaziPorudzbinu/:id" element={<OtkaziPorudzbinu />} />
        <Route path="/mojePorudzbine/:id" element={<MojePorudzbine />} />
        <Route path="/novePorudzbine/:id" element={<NovePorudzbine />} />
        <Route path="/detalji/:id" element={<Detalji />} />
       
        
      </Routes>
    </BrowserRouter>
  );
}

export default App;
