import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Registracija from './components/Registracija';
import Pocetna from './components/Pocetna';
import Login from './components/Login';

function App() {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<Pocetna />} />
        <Route path="/login" element={<Login />} />
        <Route path="/registracija" element={<Registracija />} />
      </Routes>
    </Router>
  );
}

export default App;
