<template>
  <div id="main">
    <THeader></THeader>
    <!-- <h1>Home</h1>
  <router-link :to="{ name: 'about' }">about</router-link> -->
    <TContent></TContent>
    <TFooter></TFooter>
  </div>
</template> 

<script>
import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TContent from "@/layout/TContent.vue";
export default {
  name: "THome",
  components: {
    THeader,
    TFooter,
    TContent,
  },
  setup() {},
};
</script>

<style>
#main {
  width: 100%;
  height: calc(100vh - 60px);
}
</style>