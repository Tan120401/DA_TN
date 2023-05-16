import axios from "axios";

const API = "https://localhost:7128/api/v1/Carts";

const AXIOS_CART = {
  async getCartByUserId(param) {
    return await axios.get(`${API}/get-cart/${param}`);
  },
  async getCartById(param) {
    return await axios.get(
      `https://localhost:7128/api/v1/CartResponses/${param}`
    );
  },

  async insertCart(params) {
    return await axios.post(API, params);
  },
  async updateToCart(id, params) {
    return await axios.put(`${API}/${id}`, params);
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
