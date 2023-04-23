import axios from "axios";

const API = "https://localhost:7128/api/v1/Carts";

const AXIOS_CART = {
  async getCartByUserId(param) {
    return await axios.get(`${API}/get-cart/${param}`);
  },
  async insertCart(params) {
    return await axios.post(API, params);
  },
};

export default AXIOS_CART;
