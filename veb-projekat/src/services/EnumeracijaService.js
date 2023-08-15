import axios from 'axios';

const apiUrl = process.env.REACT_APP_API_URL;

export const gettipKorisnika = async () => {
  try {
    const response = await axios.get(`${apiUrl}/tipKorisnika`);
    return response.data;
  } catch (error) {
    console.log('Greška prilikom dohvaćanja tipKorisnika:', error);
    throw error;
  }
};

export const dohvatiUloge = async () => {
  try {
    const uloge = await gettipKorisnika();
    console.log('Uloge:', uloge);
  } catch (error) {
  }
};

