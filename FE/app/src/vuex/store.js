import { createStore } from "vuex";
import USER_AXIOS from "@/api/user";
import router from "@/router/router";

// Create a new store instance.
const store = createStore({
  state() {
    return {
      count: 0,
      isLogin: null,
    };
  },
  getters: {},
  mutations: {
    increment(state) {
      state.count++;
    },
    setLogin(state, payLoad) {
      state.isLogin = payLoad;
      localStorage.setItem("isLogin", payLoad);
    },
    Logout(state) {
      state.isLogin = false;
      localStorage.removeItem("isLogin");
    },
    initializeStore(state) {
      if (localStorage.getItem("isLogin")) {
        state.isLogin =
          String(localStorage.getItem("isLogin")).toLowerCase() === "true";
      }
    },
  },
  actions: {
    async fetchLogin({ commit }, params) {
      try {
        let response = await USER_AXIOS.setLogin(params);
        console.log(response.data)
        if (response.data != null) {
          const success = true;
          commit("setLogin", success);
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
