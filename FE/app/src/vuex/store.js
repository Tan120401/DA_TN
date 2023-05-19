import { createStore } from "vuex";
import USER_AXIOS from "@/api/user";
import router from "@/router/router";

// Create a new store instance.
const store = createStore({
  state() {
    return {
      isLogin: false,
      userInfo: [],
      billInfo: [],
      lstSizeToPay: [],
      productBuyNow: [],
    };
  },
  getters: {},
  mutations: {
    increment(state) {
      state.count++;
    },
    /**
     * Hàm đăng nhập
     */
    setLogin(state, payLoad) {
      if (payLoad != false) {
        if (payLoad.data.Token) {
          state.isLogin = true;
          state.userInfo = payLoad.data;
          localStorage.setItem("isLogin", true);
          localStorage.setItem("userInfo", JSON.stringify(payLoad.data));
          console.log(payLoad.data);
        }
      } else {
        state.isLogin = false;
      }
    },
    /**
     * Hàm đăng xuất
     */
    Logout(state) {
      state.isLogin = false;
      localStorage.removeItem("isLogin");
      localStorage.removeItem("userInfo");
    },
    /**
     * Hàm lấy thông tin thông qua id
     */
    getUserById(state, payLoad) {
      if (payLoad) {
        console.log(payLoad.data[0]);
        state.userInfo = payLoad.data[0];
        localStorage.setItem("userInfo", JSON.stringify(payLoad.data[0]));
      }
    },
    /**
     * Thêm thông tin hóa đơn vào local
     */
    addToBill(state, payLoad) {
      if (payLoad != false) {
        state.billInfo = payLoad;
        localStorage.setItem("billInfo", JSON.stringify(state.billInfo));
      } else {
        state.billInfo = null;
      }
    },
    addListSizeToPay(state, payLoad) {
      if (payLoad != false) {
        console.log(payLoad);
        state.lstSizeToPay = payLoad;
        localStorage.setItem(
          "lstSizeToPay",
          JSON.stringify(state.lstSizeToPay)
        );
      } else {
        state.lstSizeToPay = null;
      }
    },
    addProductBuyNow(state, payLoad) {
      if (payLoad != false) {
        state.productBuyNow = payLoad;
        localStorage.setItem(
          "productBuyNow",
          JSON.stringify(state.productBuyNow)
        );
      } else {
        state.productBuyNow = null;
      }
    },
    initializeStore(state) {
      if (localStorage.getItem("isLogin")) {
        state.isLogin =
          String(localStorage.getItem("isLogin")).toLowerCase() === "true";
      }
      if (localStorage.getItem("userInfo")) {
        state.userInfo = JSON.parse(localStorage.getItem("userInfo"));
      }
    },
  },
  actions: {
    /**
     * Hàm đăng nhập
     */
    async fetchLogin({ commit }, params) {
      try {
        let response = await USER_AXIOS.setLogin(params);
        if (response.data != null) {
          commit("setLogin", response);
          console.log();
          if (response.data.Role == "user") {
            router.push({ path: "/" });
          } else {
            router.push({ path: "/Admin/Home" });
          }
        }
      } catch (err) {
        commit("setLogin", false);
        console.log(err);
      }
    },
    /**
     * Hàm lấy thông tin người dùng thông qua id
     */
    async getUserById({ commit }, param) {
      try {
        let response = await USER_AXIOS.getUserById(param);
        if (response.data != null) {
          commit("getUserById", response);
        }
      } catch (err) {
        commit("getUserById", false);
        console.log(err);
      }
    },
    addToBill({ commit }, param) {
      commit("addToBill", param);
    },
    setLogout({ commit }) {
      commit("Logout");
    },
    addListSizeToPay({ commit }, param) {
      commit("addListSizeToPay", param);
    },
    addProductBuyNow({ commit }, param) {
      commit("addProductBuyNow", param);
    },
    initializeStore({ commit }) {
      commit("initializeStore");
    },
  },
});

export default store;
