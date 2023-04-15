import { createStore } from "vuex";
import USER_AXIOS from "@/api/user";
import router from "@/router/router";

// Create a new store instance.
const store = createStore({
  state() {
    return {
      count: 0,
      isLogin: false,
      userId: null,
      token: null,
    };
  },
  getters: {},
  mutations: {
    increment(state) {
      state.count++;
    },
    setLogin(state, payLoad) {
      if (payLoad != false) {
        console.log(payLoad);
        if (payLoad.data.Token) {
          state.isLogin = true;
          state.token = payLoad.data.Token;
          state.userId = payLoad.data.UserId;
          localStorage.setItem("isLogin", true);
          localStorage.setItem("userId", payLoad.data.UserId);
          localStorage.setItem("token", payLoad.data.Token);

          
        }
      } else {
        state.isLogin = false;
      }
    },
    Logout(state) {
      state.isLogin = false;
      localStorage.removeItem("isLogin");
      localStorage.removeItem("userId");
      localStorage.removeItem("token");
    },
    initializeStore(state) {
      if (localStorage.getItem("isLogin")) {
        state.isLogin =
          String(localStorage.getItem("isLogin")).toLowerCase() === "true";
      }
      if (localStorage.getItem("userId")) {
        state.userId = localStorage.getItem("userId");
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
    setLogout({ commit }) {
      commit("Logout");
    },
    initializeStore({ commit }) {
      commit("initializeStore");
    },
  },
});

export default store;
