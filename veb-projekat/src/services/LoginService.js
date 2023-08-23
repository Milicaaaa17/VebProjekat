import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;
console.log('Slanje zahteva za prijavu...');
export const prijaviKorisnika = async (podaci) => {
  try {
    const headers = {
      'Content-Type': 'application/json',
     
    };

    const response = await axios.post(`${baseUrl}/login`, JSON.stringify(podaci), {
      headers: headers,
    });
    console.log('Odgovor od servera:', response);
    return response.data;
  }catch (error) {
    console.log('Greška prilikom slanja zahteva:', error);
    if (error.response) {
      console.log('Odgovor od servera sa greškom:', error.response);
    }
    throw error;
  }

  
};


export const prijavaPrekoGoogle = async (data) => {
  try {
    const headers = {
      'Content-Type': 'multipart/form-data',
    };

    const response = await axios.post(`${baseUrl}/login/socialLogin`, data, {
      headers:headers,
    });

    return response;
    
  }
  catch(error)
  {
    console.error(error);
   
    throw new Error('Greška prilikom prijave korisnika.');
  }
};



