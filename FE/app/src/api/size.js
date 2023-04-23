import axios from "axios";

const API = "https://localhost:7128/api/v1/Sizes";

const AXIOS_SIZE = {
  async getSizeByProductId(param){
    return await axios.get(`${API}/${param}`);
  }
};

export default AXIOS_SIZE;