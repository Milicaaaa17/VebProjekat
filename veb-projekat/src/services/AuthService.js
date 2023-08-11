import axios from 'axios';

export const setAuthorizationHeader = (token) => {
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    delete axios.defaults.headers.common['Authorization'];
  }
};

export const odjaviKorisnika = () => {
    localStorage.removeItem('token'); 
    setAuthorizationHeader(null); 
  };

 


