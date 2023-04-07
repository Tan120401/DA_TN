import axios from "axios";

const API = "https://localhost:7128/api/v1/Users";

const USER_AXIOS = {
  async setLogin(params) {
    return await axios.post(`${API}/Login`, params);
  },
  async setRegister(params) {
    return await axios.post(`${API}`, params);
  },
  async getAll() {
    return await axios.get(`${API}`);
  },
};

export default USER_AXIOS;
