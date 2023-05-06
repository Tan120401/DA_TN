import axios from "axios";

const API = "https://localhost:7128/api/v1/Carts";

const AXIOS_CART = {
  async getCartByUserId(param) {
    return await axios.get(`${API}/get-cart/${param}`);
  },
  async insertCart(params) {
    return await axios.post(API, params);
  },
  async deleteCartById(param) {
    return await axios.delete(`${API}/${param}`);
  },
  async deleteMulptyCart(params) {
    return await axios.delete(`${API}/delete-mulpty`, {
      data: params,
    });
  },
};

export default AXIOS_CART;
