import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

export const getSvePorudzbine = async () => {
 return await axios.get(`${baseUrl}/porudzbina/svePorudzbine`);
};



export const kreirajPorudzbinu = async (porudzbina) => {
  try {
    const response = await axios.post(`${baseUrl}/porudzbina`, porudzbina);
    console.log(response.data);
    return response.data.porudzbinaId;
  } catch (error) {
    console.error(error);
    throw new Error('Greska prilikom kreiranja porudzbine.');
  }
};


  export const getSvePorudzbineKupca = async (kupacId) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/svePorudzbineKupca/${kupacId}`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja  porudzbina.');
    }
  };

  export const getPrethodnePorudzbine = async (kupacId) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/prethodnePorudzbineKupca/${kupacId}`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja prethodnih porudzbina.');
    }
  };

  export const otkaziPorudzbinu = async (id) => {
    try {
      const response = await axios.put(`${baseUrl}/porudzbina/otkazi/${id}`);
      
      return response.data;
    } catch (error) {
      throw new Error('Greška prilikom otkazivanja porudžbine.');
    }
  };

  export const getVrijemeDostave = async (id) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/${id}/vrijemeDostave`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja vremena dostave.');
    }
  };

  export const getMojePorudzbineProdavac = async (prodavacId) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/mojePorudzbineProdavac/${prodavacId}`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja prethodnih porudzbina prodavca.');
    }
  };

  export const getNovePorudzbineProdavac = async (prodavacId) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/novePorudzbineProdavac/${prodavacId}`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja prethodnih porudzbina prodavca.');
    }
  };

  export const getArtiklePorudzbine = async (id) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/${id}/artikliPorudzbine`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja detalja porudzbine');
    }
  };

  export const getArtiklePorudzbineProdavca = async (id) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/${id}/artikliProdavca`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja detalja porudzbine');
    }
  };

 
  export const getPorudzbinaNaOsnovuId = async (id) => {
    try {
      const response = await axios.get(`${baseUrl}/porudzbina/porudzbine/${id}`);
      return response.data;
    } catch (error) {
      console.error(error);
      throw new Error('Greska prilikom dobavljanja  porudzbine');
    }
  };

 
  