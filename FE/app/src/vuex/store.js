import { createStore } from "vuex";
import USER_AXIOS from "@/api/user";
import router from "@/router/router";

// Create a new store instance.
const store = createStore({
  state() {
    return {
      isLogin: false,
      userInfo: [],
    };
  },
  getters: {},
  mutations: {
    increment(state) {
      state.count++;
    },
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
    Logout(state) {
      state.isLogin = false;
      localStorage.removeItem("isLogin");
      localStorage.removeItem("userInfo");
    },
    getUserById(state, payLoad) {
      if (payLoad) {
        console.log(payLoad.data[0])
        state.userInfo = payLoad.data[0];
        localStorage.setItem("userInfo", JSON.stringify(payLoad.data[0]));
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
    async fetchLogin({ commit }, params) {
      try {
        let response = await USER_AXIOS.setLogin(params);
        if (response.data != null) {
          commit("setLogin", response);
          router.push({ path: "/" });
        }
      } catch (err) {
        commit("setLogin", false);
        console.log(err);
      }
    },
    async getUserById({ commit }, param) {
      try {
        let response = await USER_AXIOS.getUserById(param);
        if (response.data != null) {
          commit("getUserById", response);
          router.push({ path: "/" });
        }
      } catch (err) {
        commit("getUserById", false);
        console.log(err);
      }
    },
    setLogout({ commit }) {
      commit("Logout");
    },
    initializeStore({ commit }) {
      commit("initializeStore");
    },
  },
});

export default store;
