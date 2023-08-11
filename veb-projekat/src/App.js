import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Pocetna from './components/Pocetna';
import Login from './components/Login';
import Registracija from './components/Registracija';
import Dashboard from './components/Dashboard';
import Profil from './components/Profil';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Pocetna />} />
        <Route path="/login" element={<Login />} />
        <Route path="/registracija" element={<Registracija />} />
        <Route path="/profil/:id" element={<Profil />} />
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
