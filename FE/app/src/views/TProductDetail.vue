<template>
  <div id="main">
    <THeader></THeader>
    <div id="content" v-if="!isLoading">
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
          <img
            :src="require(`@/assets/img/product/${pictureHighlight}`)"
            alt=""
          />
          <div class="product__detail-smallpic">
            <img
              :src="require(`../assets/img/product/${item.ImageLink}`)"
              alt=""
              v-for="(item, index) in imageOptions"
              :key="index"
              @click="selectedImage(item.ImageLink)"
            />
          </div>
        </div>
        <div class="product__detail-right">
          <h1 class="product__detail-name">
            {{ productDetail.ProductName }}
          </h1>
          <div class="product__detail-price">
            <h4 class="price-discount">
              {{
                formatter.format(
                  (productDetail.Price * (100 - productDetail.Discount)) / 100
                )
              }}
            </h4>
            <h4 class="product__price-discount">
              {{ formatter.format(productDetail.Price) }}
            </h4>

            <h4 class="price-sale">Sale: {{ productDetail.Discount }}%</h4>
          </div>
          <h4>Kích thước giày:</h4>
          <div class="product__detail-size d-flex align-items-center">
            <div
              class="product__detail-size-item"
              v-for="(item, index) in sizeOptions"
              :key="index"
              :class="{
                'product__detail-size-item--active':
                  item.SizeNumber == sizeProduct,
              }"
              @click="selectedSize(item)"
            >
              {{ item.SizeNumber }}
            </div>
            <h6 v-if="quantityBySize">Số lượng: {{ quantityBySize }}</h6>
          </div>
          <div style="position: relative">
            <span class="product-fail" v-if="isSizeItem"
              >Vui lòng chọn size giày</span
            >
          </div>
          <h4>Số lượng:</h4>
          <div class="product__detail-num">
            <i
              class="fa-solid fa-minus product__detail-remove"
              @click="removeProduct"
            ></i>
            <input type="text" class="number" v-model="numProduct" />
            <i
              class="fa-solid fa-plus product__detail-add"
              @click="addProduct"
            ></i>
          </div>
          <div style="position: relative">
            <span class="numproduct-fail" v-if="isOverloadNumProduct"
              >Số lượng lớn hơn số còn lại.</span
            >
          </div>
          <div class="product__detail-options">
            <!-- <router-link to="Cart/"> -->
            <div class="add-to-card tbutton" @click="addToCart()">
              <i class="fa-solid fa-cart-shopping"></i>Thêm vào giỏ hàng
            </div>
            <!-- </router-link> -->
            <div class="tbutton buy-now" @click="onBuyNow">
              <i class="fa-solid fa-bag-shopping"></i> Mua ngay
            </div>
          </div>
        </div>
      </div>
      <div class="product__detail-descriptions">
        <h3>Chi tiết sản phẩm</h3>
        {{ productDetail.Description }}
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
  <TPopup
    v-if="isShowPopup"
    popupTile="Thông báo"
    popupContent="Vui lòng đăng nhập trước khi mua hàng"
    @hidePopup="hidePopup"
    ><template #footer>
      <button type="button" class="btn btn-secondary" @click="hidePopup">
        Đóng
      </button>
      <button type="button" class="btn btn-primary" @click="linkToLogin">
        Đăng nhập
      </button>
    </template></TPopup
  >
</template>

<script>
import { useRoute } from "vue-router";
import { reactive, ref, watchEffect, watch, computed } from "vue";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TPopup from "@/components/TPopup.vue";
import AXIOS_PRODUCT from "@/api/Product";
import AXIOS_SIZE from "@/api/size";
import AXIOS_IMAGE from "@/api/image";
import { CART_OPTIONS } from "@/js/constrant";
import AXIOS_CART from "@/api/cart";

import router from "@/router/router";
import { useStore } from "vuex";
import _ from "lodash";
export default {
  name: "TProductdetail",
  components: {
    THeader,
    TFooter,
    TPopup,
  },
  setup() {
    const route = useRoute();
    const store = useStore();
    const sizeOptions = ref();
    const imageOptions = ref();
    const sizeProduct = ref();
    const numProduct = ref(1);
    const pictureHighlight = ref();
    const productDetail = ref();
    const cartOptions = reactive(_.cloneDeep(CART_OPTIONS));
    const isLoading = ref(true);
    const isSizeItem = ref();
    const sizeSelected = ref([]);
    const quantityBySize = ref();
    const isShowPopup = ref(false);

    /**
     * Ẩn popup
     */
    const hidePopup = () => {
      isShowPopup.value = false;
    };
    /**
     * Đăng nhập
     */
    const linkToLogin = () => {
      router.push({ name: "Login" });
    };
    /**
     * Hàm lấy thông tin sản phẩm thông qua id
     * @param {Id product} param
     */
    const getProductById = async (param) => {
      try {
        let response = await AXIOS_PRODUCT.getProductById(param);
        if (response) {
          isLoading.value = true;
          productDetail.value = response.data[0];
          pictureHighlight.value = response.data[0].ImgProduct;
          isLoading.value = false;
        }
      } catch (err) {
        console.log(err);
      }
    };
    getProductById(route.params.id);

    /**
     * Hàm lấy size của sản phẩm
     */
    const getAllSizeProduct = async (param) => {
      try {
        let response = await AXIOS_SIZE.getSizeByProductId(param);
        if (response) {
          sizeOptions.value = response.data;
        }
      } catch (err) {
        console.log(err);
      }
    };
    getAllSizeProduct(route.params.id);

    /**
     * Hàm lấy ảnh của sản phẩm
     */
    const getALLImageProduct = async (param) => {
      try {
        let response = await AXIOS_IMAGE.getImageByProductId(param);
        if (response) {
          imageOptions.value = response.data;
        }
      } catch (err) {
        console.log(err);
      }
    };
    getALLImageProduct(route.params.id);
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
        sizeProduct.value = value.SizeNumber;
        quantityBySize.value = value.Quantity;
        sizeSelected.value = value;
      }
    };

    /**
     * Hàm thêm số lượng
     */
    const addProduct = () => {
      if (typeof numProduct.value == "string") {
        numProduct.value = parseInt(numProduct.value);
      }
      numProduct.value += 1;
    };
    /**
     * Hàm bớt số lượng
     */
    const removeProduct = () => {
      if (typeof numProduct.value == "string") {
        numProduct.value = parseInt(numProduct.value);
      }
      if (numProduct.value > 1) {
        numProduct.value -= 1;
      }
    };
    /**
     * Hàm theo dõi có chọn size hay không
     */
    watch(sizeProduct, (newVal) => {
      if (newVal) {
        isSizeItem.value = false;
      }
    });
    /**
     * Hàm theo dõi số lượng mua
     */
    const isOverloadNumProduct = computed(() => {
      if (quantityBySize.value) {
        return numProduct.value
          ? parseInt(numProduct.value) > quantityBySize.value
          : false;
      }
    });

    const insertToCart = async (params) => {
      try {
        let response = await AXIOS_CART.insertCart(params);
        if (response) {
          router.push({
            path: `/Cart`,
          });
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Hàm thêm sản phẩm vào giỏ hàng
     */
    const addToCart = () => {
      if (store.state.isLogin) {
        if (!sizeProduct.value) {
          isSizeItem.value = true;
        }
        if (isSizeItem.value || isOverloadNumProduct.value) {
          return;
        } else {
          cartOptions.UserId = store.state.userInfo.UserId;
          cartOptions.ProductId = route.params.id;
          cartOptions.SizeProduct = sizeProduct.value;
          cartOptions.NumProduct = numProduct.value;
          insertToCart(cartOptions);
        }
      } else {
        isShowPopup.value = true;
      }
    };

    /**
     * Ấn mua ngay
     */
    const onBuyNow = () => {
      if (store.state.isLogin) {
        if (!sizeProduct.value) {
          isSizeItem.value = true;
        }
        if (isSizeItem.value || isOverloadNumProduct.value) {
          return;
        } else {
        }
      } else {
        isShowPopup.value = true;
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
      isShowPopup,
      sizeSelected,
      quantityBySize,
      isOverloadNumProduct,
      store,
      isLoading,
      productDetail,
      route,
      sizeOptions,
      sizeProduct,
      isSizeItem,
      imageOptions,
      pictureHighlight,
      cartOptions,
      formatter,
      numProduct,
      hidePopup,
      linkToLogin,
      selectedImage,
      addToCart,
      onBuyNow,
      selectedSize,
      addProduct,
      getProductById,
      removeProduct,
      getAllSizeProduct,
      getALLImageProduct,
      insertToCart,
    };
  },
};
</script>

<style>
.product-fail,
.numproduct-fail {
  position: absolute;
  top: -20px;
  font-size: 14px;
  color: red;
  font-style: italic;
}
.numproduct-fail {
  top: 6px;
}
.number {
  width: 40px;
  display: flex;
  text-align: center;
  border: 1px solid transparent;
}
</style>