import React from 'react';
import { Link } from 'react-router-dom';
import { odjaviKorisnika } from '../services/AuthService';

const Navigacija = () => {
  const handleLogout = () => {
    odjaviKorisnika(); 
  };

  return (
    <nav>
      <ul>
        <li>
          <Link to="/">Početna</Link>
        </li>
        <li>
          <Link to="/dashboard">Dashboard</Link>
        </li>
        <li>
          <button onClick={handleLogout}>Odjava</button>
        </li>
      </ul>
    </nav>
  );
};

export default Navigacija;
