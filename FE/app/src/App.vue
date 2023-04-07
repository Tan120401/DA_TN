<template>
  <router-view></router-view>
</template>

<script >
import { useStore } from "vuex";
export default {
  name: "App",
  components: {},
  mounted() {
    const $store = useStore();
    $store.dispatch("initializeStore");
  },
};
</script>

<style>
@import url(@/css/main.css);
</style>
