import axios from "axios";

const API = "https://localhost:7128/api/v1/Orders";

const AXIOS_ORDER = {
  async getAllOrderServicebyFilter(params) {
    return await axios.post(
      `${API}/PagingAndFilter?keyWord=${params.keyWord}&pageSize=${params.pageSize}&pageNumber=${params.pageNumber}`
    );
  },
  async getAllOrderServicebyUserId(params) {
    return await axios.post(
      `${API}/filter-by-userId?keyWord=${params.keyWord}&userId=${params.userId}&pageSize=${params.pageSize}&pageNumber=${params.pageNumber}`
    );
  },
  async getOrderByUserId(param) {
    return await axios.get(`${API}/${param}`);
  },

  async getOrderServiceByOrderId(param) {
    return await axios.get(`${API}/get-orderdetail-service/${param}`);
  },
  async insertOrder(params) {
    return await axios.post(API, params);
  },
  async updateOrder(orderId, params) {
    return await axios.put(`${API}/${orderId}`, params);
  },
  async deleteOrderById(param) {
    return await axios.delete(`${API}/${param}`);
  },
  async deleteMulptyOrder(params) {
    return await axios.delete(`${API}/delete-mulpty`, {
      data: params,
    });
  },
};

export default AXIOS_ORDER;
