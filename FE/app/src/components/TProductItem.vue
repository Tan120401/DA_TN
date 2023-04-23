<template>
  <div class="product__item">
    <div class="small_product__item">
      <i class="fa-solid fa-heart"></i>
      <i class="fa-solid fa-share"></i>
    </div>
    <div class="image">
      <router-link :to="`/Productdetail/${id}`"
        ><img :src="require(`../assets/img/product/${picture}`)"
      /></router-link>
    </div>
    <div class="products_text">
      <h2>{{ productname }}</h2>
      <div class="product__price">
        <span class="m-r-8">{{ formatMoney.format(pricediscount) }}</span>
        <span class="product__price-discount">{{
          formatMoney.format(price)
        }}</span>
      </div>
      <router-link :to="`/Productdetail/${id}`"
        ><div class="product__show-detail">Xem chi tiết</div></router-link
      >
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import { formatter } from "@/js/constrant";
export default {
  name: "TProductitem",
  props: ["id", "picture", "productname", "price", "pricediscount"],
  setup() {
    /**
     * Định dạng tiền tệ
     */
    const formatMoney = ref(formatter);
    return {
      formatMoney,
    };
  },
};
</script>

<style>
</style>