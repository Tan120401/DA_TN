<template>
  <div id="main">
    <THeader></THeader>
    <TContent></TContent>
    <TFooter></TFooter>
  </div>
</template> 

<script>
import { useRoute } from "vue-router";
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
  setup() {
    const route = useRoute();
    return {
      route,
    };
  },
};
</script>

<style>
#main {
  width: 100%;
  height: calc(100vh - 60px);
}
</style>