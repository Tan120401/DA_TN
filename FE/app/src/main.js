import "bootstrap/dist/css/bootstrap.css";
import "bootstrap/dist/js/bootstrap.js";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router/router";
import store from "./vuex/store";
const app = createApp(App);

app.use(router).use(store).mount("#app");
