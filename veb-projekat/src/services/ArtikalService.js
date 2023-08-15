import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

export const getSviArtikli = async () => {
 
 return await axios.get(`${baseUrl}/artikal/sviArtikli`);
};

export const getSviArtikliProdavac = async (prodavacid) => {
  try {
    const response = await axios.get(`${baseUrl}/artikal/sviArtikliProdavac/${prodavacid}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error('Greska prilikom pronalaženja artikla.');
  }
 };
 

export const azurirajArtikal = async (id, formData) => {
    try {
      const response = await axios.put(`${baseUrl}/artikal/${id}`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom azuriranja artikla.');
    }
  };
  
  export const obrisiArtikal = async (idArtikla) => {
    try {
      const response = await axios.delete(`${baseUrl}/artikal/${idArtikla}`);
      return response.data;
    } catch (error) {
      throw new Error('Greška prilikom brisanja artikla.'); 
    }
  };

  export const pronadjiArtikalpoid = async (id) => {
    try {
      const response = await axios.get(`${baseUrl}/artikal/${id}`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom pronalaženja artikla.');
    }
  };
  

