<template>
  <div id="main">
    <THeader></THeader>
    <div id="content">
      <div class="product__detail-title">
        <router-link :to="{ name: 'Home' }"
          ><span class="product__detail-title-text"
            >Trang chủ</span
          ></router-link
        >
        <i class="fa-solid fa-chevron-right"></i>
        <span class="product__detail-title-text">Chi tiết sản phẩm </span>
        <!-- <span>{{ route.params.id }}</span> -->
      </div>
      <div class="product__detail-content">
        <div class="product__detail-left">
          <img :src="require(`@/assets/img/${pictureHighlight}`)" alt="" />
          <div class="product__detail-smallpic">
            <img
              :src="require(`../assets/img/${item}`)"
              alt=""
              v-for="(item, index) in imageOptions"
              :key="index"
              @click="selectedImage(item)"
            />
          </div>
        </div>
        <div class="product__detail-right">
          <h1 class="product__detail-name">NIKE AIR FORCE</h1>
          <div class="product__detail-price">
            <h4 class="price">{{ formatter.format(1000000) }}</h4>
            <h4 class="price-discount">{{ formatter.format(900000) }}</h4>
            <h4 class="price-sale">Sale: 10%</h4>
          </div>
          <h4>Kích thước giày:</h4>
          <div class="product__detail-size">
            <div
              class="product__detail-size-item"
              v-for="(item, index) in sizeOptions"
              :key="index"
              :class="{
                'product__detail-size-item--active': item == isSizeItem,
              }"
              @click="selectedSize(item)"
            >
              {{ item }}
            </div>
          </div>
          <h4>Số lượng:</h4>
          <div class="product__detail-num">
            <i
              class="fa-solid fa-minus product__detail-remove"
              @click="removeProduct"
            ></i>
            <div class="number">{{ numProduct }}</div>
            <i
              class="fa-solid fa-plus product__detail-add"
              @click="addProduct"
            ></i>
          </div>
          <div class="product__detail-options">
            <router-link to="Cart/"
              ><div class="add-to-card tbutton">
                <i class="fa-solid fa-cart-shopping"></i>Thêm vào giỏ hàng
              </div></router-link
            >
            <router-link :to= "{name: 'Pay'}"><div class="tbutton buy-now">Mua ngay</div></router-link>
          </div>
        </div>
      </div>
      <div class="product__detail-descriptions">
        <h3>Chi tiết sản phẩm</h3>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Enim debitis a
        ad fugit nesciunt eos explicabo placeat eligendi error cumque facere,
        reprehenderit quasi, nisi veniam harum ratione recusandae. Mollitia,
        eius!
      </div>
      <div class="product__detail-evaluate">
        <h3>Đánh giá</h3>
        <div class="user-evaluate">
          <div class="user-avatar"></div>
          <div class="user-profile">
            <div class="user-name">Nguyễn Văn Tân</div>
            <div class="user-time">01/07/2020</div>
            <div class="evaluate-content m-t-16">Sản phẩm tốt</div>
          </div>
        </div>
      </div>
    </div>
    <TFooter></TFooter>
  </div>
</template>

<script>
import { useRoute } from "vue-router";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import { reactive, ref } from "vue";
export default {
  name: "TProductdetail",
  components: {
    THeader,
    TFooter,
  },
  setup() {
    const route = useRoute();
    const sizeOptions = reactive([38, 39, 40, 41, 42]);
    const imageOptions = reactive([
      "1.1nike.png",
      "1.2nike.png",
      "1.3nike.png",
      "1.4nike.png",
    ]);
    const isSizeItem = ref();
    const numProduct = ref(1);
    const pictureHighlight = ref("1.1nike.png");

    /**
     * Hàm chọn size
     */
    const selectedImage = (value) => {
      pictureHighlight.value = value;
    };
    /**
     * Hàm chọn size
     */
    const selectedSize = (value) => {
      if (value) {
        console.log(value);
        isSizeItem.value = value;
      }
    };

    /**
     * Hàm thêm số lượng
     */
    const addProduct = () => {
      numProduct.value += 1;
    };
    /**
     * Hàm bớt số lượng
     */
    const removeProduct = () => {
      if (numProduct.value > 1) {
        numProduct.value -= 1;
      }
    };
    /**
     * Định dạng tiền tệ VND
     */
    const formatter = new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    });
    return {
      route,
      sizeOptions,
      isSizeItem,
      selectedSize,
      imageOptions,
      pictureHighlight,
      selectedImage,
      numProduct,
      addProduct,
      removeProduct,
      formatter,
    };
  },
};
</script>

<style>
</style>