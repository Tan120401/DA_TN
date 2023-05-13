<template>
  <div id="main">
    <THeader></THeader>
    <TContent></TContent>
    <TFooter></TFooter>
    <router-view />
  </div>
</template> 
  
  <script>
import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TContent from "@/layout/TContent.vue";
import { useRoute } from "vue-router";
import { computed } from "vue";
export default {
  name: "TCategory",
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
</style>