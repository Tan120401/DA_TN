import axios from "axios";

const API = "https://localhost:7128/api/v1/Bills";

const AXIOS_BILL = {
  async getBillById(param) {
    return await axios.get(`${API}/${param}`);
  },
  async insertBill(params) {
    return await axios.post(API, params);
  },
  async deleteBillById(param) {
    return await axios.delete(`${API}/${param}`);
  },
  async deleteMulptyBill(params) {
    return await axios.delete(`${API}/delete-mulpty`, {
      data: params,
    });
  },
};

export default AXIOS_BILL;
