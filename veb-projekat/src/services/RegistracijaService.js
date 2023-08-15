import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;



export const registrujKorisnika = async (formData) => {
    const response = await axios.post(`${baseUrl}/Registracija/register`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
};


