import axios from "axios";

const baseURL = process.env.REACT_APP_API_URL;

console.log("ADRESA ", baseURL);

const instance = axios.create({
  baseURL: baseURL,
});

// Dodajte funkciju za postavljanje zaglavlja
instance.interceptors.request.use(config => {
  const token = localStorage.token; // Zamenite ovo sa vašim stvarnim pristupnim tokenom
  config.headers.Authorization = `Bearer ${token}`;
  return config;
});

export default instance;
