import axios from "axios";

const API = "https://localhost:7128/api/Momos/pay-momo";

const AXIOS_MOMO = {
  async paymentMomo(params) {
    return await axios.post(`${API}/?amounts=${params.amount}&returnUrls=${params.returnUrl}`);
  },
};

export default AXIOS_MOMO;
