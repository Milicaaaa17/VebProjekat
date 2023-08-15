import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

export const getSviKorisnici = async () => {
  const token = localStorage.getItem('token');
  axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
 return await axios.get(`${baseUrl}/admin/sviKorisnici`);
};

export const kreirajArtikal = async (artikal) => {
    try {
      const response = await axios.post(`${baseUrl}/artikal`, artikal);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greška prilikom kreiranja artikla.');
    }
  };
 
  export const getVerifikovaniProdavci = async () => {
    const token = localStorage.getItem('token');
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    return await axios.get(`${baseUrl}/admin/sviverifikovaniprodavci`);
    
  };

  export const getOdbijeniProdavci = async () => {
    const token = localStorage.getItem('token');
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    return await axios.get(`${baseUrl}/admin/sviodbijeniprodavci`);
  };

  export const getKorisniciKojiSuOdobreni = async () => {
    const token = localStorage.getItem('token');
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    return await axios.get(`${baseUrl}/admin/svikorisnicicijajeregistracijaodobrena`);
   
  };

  export const getProdavciKojiCekajuVerifikaciju = async () => {
    const token = localStorage.getItem('token');
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    return await axios.get(`${baseUrl}/admin/sviprodavcikojicekajuverifikaciju`);
  };

  export const getKorisniciKojiCekajuOdobrenje = async () => {
    const token = localStorage.getItem('token');
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    return await axios.get(`${baseUrl}/admin/registracije`);
  };


  export const prihvatiRegistraciju = async (id) => {
    try {
      const token = localStorage.getItem('token');
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      const url = `${baseUrl}/admin/registracije/${id}/prihvati-verifikaciju`;
      console.log('Prihvatanje registracije:', url); // Dodatni log
      const response = await axios.post(url);
      console.log('Prihvatanje odgovor:', response.data); // Dodatni log
      return response.data;
    } catch (error) {
      console.error('Greška prilikom prihvatanja registracije prodavca:', error);
      throw new Error('Greška prilikom prihvatanja registracije prodavca.');
    }
  };
  
  export const odbijRegistracijuProdavca = async (id) => {
    try {
      const token = localStorage.getItem('token');
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      const url = `${baseUrl}/admin/registracije/${id}/odbij-verifikaciju`;
      console.log('Odbijanje registracije:', url); // Dodatni log
      const response = await axios.post(url);
      console.log('Odbijanje odgovor:', response.data); // Dodatni log
      return response.data;
    } catch (error) {
      console.error('Greška prilikom odbijanja registracije prodavca:', error);
      throw new Error('Greška prilikom odbijanja registracije prodavca.');
    }
  };
  

  export const odobrenaRegistracija = async (id) => {
    try {
      const token = localStorage.getItem('token');
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      const response = await axios.post(`${baseUrl}/admin/registracije/${id}/odobri`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greška prilikom prihvatanja registracije korisnika.');
    }
  };
  
  export const odbijenaRegistracija = async (id) => {
    try {
      const token = localStorage.getItem('token');
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      const response = await axios.post(`${baseUrl}/admin/registracije/${id}/odbij`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greška prilikom odbijanja registracije prodavca.');
    }
  };

  export const getKorisnikPoid = async (id) => {
    try {
      const token = localStorage.getItem('token');
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      const response = await axios.get(`${baseUrl}/korisnik/${id}`);
      return response.data;
    } catch (error) {
      console.error('Greška prilikom dobijanja korisnika po id-u:', error);
      throw error;
    }
  };

 
