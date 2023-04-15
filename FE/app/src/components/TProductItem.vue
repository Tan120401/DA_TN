<template>
  <div class="product__item">
    <div class="small_product__item">
      <i class="fa-solid fa-heart"></i>
      <i class="fa-solid fa-share"></i>
    </div>
    <div class="image">
      <router-link :to="`productdetail/${id}`"
        ><img :src="require(`@/assets/img/${picture}`)"
      /></router-link>
    </div>
    <div class="products_text">
      <h2>{{ productname }}</h2>
      <!-- <div class="products_star">
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star-half-stroke"></i>
        <i class="fa-regular fa-star"></i>
      </div> -->
      <div class="product__price">
        <span class="m-r-8">{{ formatMoney.format(pricediscount) }}</span>
        <span class="product__price-discount">{{
          formatMoney.format(price)
        }}</span>
      </div>
      <router-link :to="`productdetail/${id}`"
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
    const formatMoney = ref(formatter);
    return {
      formatMoney
    };
  },
};
</script>

<style>
</style>