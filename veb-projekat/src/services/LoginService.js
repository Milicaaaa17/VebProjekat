import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

export const prijaviKorisnika = async (podaci) => {
  try {
    const headers = {
      'Content-Type': 'application/json',
     
    };

    const response = await axios.post(`${baseUrl}/Login`, JSON.stringify(podaci), {
      headers: headers,
    });

    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error(error.response.data.error);
  }
};



