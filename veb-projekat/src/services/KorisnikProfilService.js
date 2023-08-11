import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

export const getKorisnikId = async (id) => {
  try {
    const response = await axios.get(`${baseUrl}/Korisnik/${id}/profil`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error('Greska prilikom dobavljanja informacija o korisniku.');
  }
};

export const getStatusProcesaVerifikacije = async (id) => {
  try {
    const response = await axios.get(`${baseUrl}/Korisnik/${id}/status-verifikacije`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error('Greska prilikom dobavljanja statusa procesa verifikacije.');
  }
};

export const azurirajKorisnika = async (id, formData) => {
  try {
    const response = await axios.put(`${baseUrl}/Korisnik/${id}`, formData,  {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error('Greska prilikom azuriranja korisnika.');
  }
};

export const obrisiKorisnika = async (id) => {
  try {
    await axios.delete(`${baseUrl}/Korisnik/${id}`);
    return true;
  } catch (error) {
    console.error(error);
    throw new Error('Greska prilikom brisanja korisnika.');
  }
};

