import axios from "axios";

const API = "https://localhost:7128/api/v1/Products";

const AXIOS_PRODUCT = {
  async getProductByFilter(params) {
    return await axios.post(
      `${API}/PagingAndFilter?keyWord=${params.keyWord}&pageSize=${params.pageSize}&pageNumber=${params.pageNumber}`
    );
  },
  async getProductByCategory(params) {
    return await axios.get(`${API}/get-product`, { params });
  },
  async getProductById(param) {
    return await axios.get(`${API}/${param}`);
  },
  async insertToProduct(param) {
    return await axios.post(`${API}`, param);
  },
  async uploadFile(params) {
    return await axios.post(`${API}/upload-file`, params, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  },
};

export default AXIOS_PRODUCT;
