<template>
    <h1>Report</h1>
  </template>
  
  <script>
  import TSidebar from "@/layout/TSidebar.vue";
  import THeaderAdmin from "./THeaderAdmin.vue";
  import TPopupVue from "@/components/TPopup.vue";
  import { useStore } from "vuex";
  
  export default {
    components: {
      TSidebar,
      THeaderAdmin,
      TPopupVue,
    },
    setup() {
      const $store = useStore();
      return {
        $store,
      };
    },
  };
  </script>
  
  <style>
  #admin__main {
    width: 100%;
    height: 100vh;
    display: flex;
    justify-content: space-between;
  }
  .admin__main-content-right {
    width: calc(100% - 240px);
  }
  </style>