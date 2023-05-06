<template>
  <div id="admin__main">
    <TSidebar></TSidebar>
    <div class="admin__main-content-right">
      <THeaderAdmin></THeaderAdmin>
      <router-view />
    </div>
  </div>
</template>
  
<script>
import TSidebar from "@/layout/TSidebar.vue";
import THeaderAdmin from "./THeaderAdmin.vue";

export default {
  name: "TAdmin",
  components: {
    TSidebar,
    THeaderAdmin,
  },
  setup() {
    return {};
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