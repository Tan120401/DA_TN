import axios from "axios";

const API = "https://localhost:7128/api/v1/Sizes";

const AXIOS_SIZE = {
  async getSizeByProductId(param) {
    return await axios.get(`${API}/get-by-productid/${param}`);
  },
  async getSizeBySizeNumber(param) {
    return await axios.get(`${API}/${param}`);
  },
  async insertMulptySize(params) {
    return await axios.post(`${API}/insert-mulpty`, params);
  },
  async updateToSize(id, params) {
    return await axios.put(`${API}/${id}`, params);
  },
  async deleteSizeByProductId(param) {
    return await axios.delete(`${API}/${param}`);
  },
};

export default AXIOS_SIZE;
