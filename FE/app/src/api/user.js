import axios from "axios";

const API = "https://localhost:7128/api/v1/Users";

const USER_AXIOS = {
  async setLogin(params) {
    return await axios.post(`${API}/Login`, params);
  },
  async setRegister(params) {
    return await axios.post(`${API}`, params);
  },
  async forgotPassword(param) {
    return await axios.post(`${API}/fogot-password?userName=${param}`);
  },
  async getUserByFilter(params) {
    return await axios.post(
      `${API}/PagingAndFilter?keyWord=${params.keyWord}&pageSize=${params.pageSize}&pageNumber=${params.pageNumber}`
    );
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
  async deleteUserById(param) {
    return await axios.delete(`${API}/${param}`);
  },
};

export default USER_AXIOS;
