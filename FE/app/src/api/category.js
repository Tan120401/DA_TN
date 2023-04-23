import axios from "axios";

const API = "https://localhost:7128/api/v1/Categorys";

const AXIOS_CATEGORY = {
  async getAllCategory() {
    return await axios.get(API);
  },
};

export default AXIOS_CATEGORY;