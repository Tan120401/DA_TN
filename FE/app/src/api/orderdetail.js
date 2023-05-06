import axios from "axios";

const API = "https://localhost:7128/api/v1/OrderDetails";

const AXIOS_ORDER_DETAIL = {
  async getOrderDetailByUserId(param) {
    return await axios.get(`${API}/get-OrderDetail/${param}`);
  },
  async insertOrderDetail(params) {
    return await axios.post(API, params);
  },
  async deleteOrderDetailById(param) {
    return await axios.delete(`${API}/${param}`);
  },
  async deleteMulptyOrderDetail(params) {
    return await axios.delete(`${API}/delete-mulpty`, {
      data: params,
    });
  },
};

export default AXIOS_ORDER_DETAIL;
