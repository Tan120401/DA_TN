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
  async getUserById(param) {
    return await axios.get(`${API}/${param}`);
  },
  async updateUser(id, params) {
    return await axios.put(`${API}/${id}`, params);
  },
  async uploadFile(params) {
    return await axios.post(`${API}/upload-file`, params, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  },
};

export default USER_AXIOS;
