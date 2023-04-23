import axios from "axios";

const API = "https://localhost:7128/api/v1/ImageProducts";

const AXIOS_IMAGE = {
  async getImageByProductId(param){
    return await axios.get(`${API}/${param}`);
  }
};

export default AXIOS_IMAGE;