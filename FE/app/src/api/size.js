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
};

export default AXIOS_SIZE;
