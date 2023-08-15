import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

export const kreirajArtikal = async (formData) => {
  try {
    const response = await axios.post(`${baseUrl}/artikal`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error('Greška prilikom kreiranja artikla.');
  }
};
