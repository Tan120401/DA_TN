<template>
  <div id="content">
    <TBanner></TBanner>
    <h1 class="product-title" @click="test">Sản phẩm nổi bật</h1>
    <div class="product-list">
      <TProductitem
        v-for="(item, index) in productList"
        :id="item.id"
        :key="index"
        :picture="item.picture"
        :productname="item.productname"
        :price="item.price"
        :pricediscount="item.pricediscount"
      ></TProductitem>
    </div>
    <h1 class="product-title">Sản phẩm xem nhiều nhất</h1>
    <div class="product-list">
      <TProductitem
        v-for="(item, index) in productList"
        :key="index"
        :picture="item.picture"
        :productname="item.productname"
        :price="item.price"
        :pricediscount="item.pricediscount"
      ></TProductitem>
    </div>
    <h1 class="product-title">Sản phẩm khuyến mãi</h1>
    <div class="product-list">
      <TProductitem
        v-for="(item, index) in productList"
        :key="index"
        :picture="item.picture"
        :productname="item.productname"
        :price="item.price"
        :pricediscount="item.pricediscount"
      ></TProductitem>
    </div>
    <h1 class="news-title">Tin tức</h1>
    <div class="news-list">
      <TNewitem
        v-for="(item, index) in newsList"
        :key="index"
        :newpic="item.newpic"
        :newcontent="item.newcontent"
      ></TNewitem>
    </div>
  </div>
</template>

<script>
import TProductitem from "@/components/TProductItem.vue";
import TBanner from "@/components/TBanner.vue";
import TNewitem from "@/components/TNewsItem.vue";
import USER_AXIOS from "@/api/user";
import { USER } from "@/js/data";

import { PRODUCTS, NEWS } from "@/js/data";
import { reactive } from "vue";
export default {
  name: "TContent",
  components: {
    TProductitem,
    TBanner,
    TNewitem,
  },
  setup() {
    const productList = reactive(PRODUCTS);
    const newsList = reactive(NEWS);
    const test = async () => {
      try {
        let respone = await USER_AXIOS.setLogin(USER);
        if (respone) {
          console.log(respone);
        }
      } catch (err) {
        console.log(err);
      }
    };
    return {
      productList,
      newsList,
      test,
    };
  },
};
</script>

<style>
</style>