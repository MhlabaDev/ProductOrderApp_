
import axios from "axios";

const API_BASE = "http://localhost:5225/api";

export const getProducts = async () => {
  const res = await axios.get(`${API_BASE}/Product`);
  return res.data;
};

export const createCustomer = async (customer) => {
  const res = await axios.post(`${API_BASE}/Customer`, customer);
  return res.data;
};

export const createOrder = async (order) => {
  const res = await axios.post(`${API_BASE}/Order`, order);
  return res.data;
};

export  const showNotification = (message, type = "success") => {
    setNotification({ message, type });
    setTimeout(() => setNotification(null), 3000);
  };