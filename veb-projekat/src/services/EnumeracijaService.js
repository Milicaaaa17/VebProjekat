import axios from 'axios';

const apiUrl = process.env.REACT_APP_API_URL;

export const getUlogaKorisnika = async () => {
  try {
    const response = await axios.get(`${apiUrl}/UlogaKorisnika`);
    return response.data;
  } catch (error) {
    console.log('Greška prilikom dohvaćanja UlogaKorisnika:', error);
    throw error;
  }
};

export const dohvatiUloge = async () => {
  try {
    const uloge = await getUlogaKorisnika();
    console.log('Uloge:', uloge);
  } catch (error) {
  }
};

