import axios from "axios";

const API = "https://localhost:7128/api/v1/Products";

const AXIOS_PRODUCT = {
  async getProductByCategory(params) {
    return await axios.get(`${API}/get-product`, { params });
  },
  async getProductById(param){
    return await axios.get(`${API}/${param}`);
  }
};

export default AXIOS_PRODUCT;
