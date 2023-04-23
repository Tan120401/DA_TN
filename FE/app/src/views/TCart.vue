<template>
  <div id="main">
    <THeader></THeader>
    <div id="cart">
      <h2>Giỏ hàng của tôi</h2>
      <div class="cart__note">
        <i class="fa-solid fa-circle-exclamation m-r-8"></i>
        <span
          >Nine store thông báo: quý khách vui lòng kiểm tra thông tin đầy đủ
          trước khi đặt hàng</span
        >
      </div>
      <table class="cart__table">
        <tbody>
          <tr>
            <th style="width: 40px" class="flex-item just-center">
              <input type="checkbox" v-model="selectAll" />
            </th>
            <th style="width: 380px; text-align: left">Sản phẩm</th>
            <th style="width: 120px">Size</th>
            <th style="width: 120px">Số lượng</th>
            <th style="width: 320px">Đơn giá</th>
            <th style="width: 169px">Thành tiền</th>
            <th style="width: 80px"></th>
          </tr>
          <tr v-for="item in dataSources" :key="item.id">
            <td class="flex-item just-center">
              <input type="checkbox" v-model="selected" :value="item" />
            </td>
            <td style="text-align: left">
              <div class="cart__item">
                <div class="cart__item-img">
                  <img
                    :src="require(`@/assets/img/product/${item.ImgProduct}`)"
                    alt=""
                  />
                </div>
                <div class="cart__item-info">
                  <h4>{{ item.ProductName }}</h4>
                  <div class="cart__item-sale">Giảm {{ item.Discount }}%</div>
                </div>
              </div>
            </td>
            <td>{{ item.SizeProduct }}</td>
            <td>
              <div class="product__detail-num m-t-0">
                <i
                  class="fa-solid fa-minus product__detail-remove"
                  @click="removeProduct(item)"
                ></i>
                <input type="text" class="number" v-model="item.NumProduct" />
                <i
                  class="fa-solid fa-plus product__detail-add"
                  @click="addProduct(item)"
                ></i>
              </div>
            </td>
            <td>
              {{ formatter.format(item.Price * (1 - item.Discount / 100)) }}
              <span class="product__price-discount"
                >{{ formatter.format(item.Price) }}
              </span>
            </td>
            <td>
              <!-- <span class="product__price-discount m-r-8">{{
                formatter.format(item.Price * item.NumProduct)
              }}</span> -->
              <span>{{
                formatter.format(
                  item.Price * (1 - item.Discount / 100) * item.NumProduct
                )
              }}</span>
            </td>
            <td>
              <button
                type="button"
                class="btn btn-outline-danger btn-sm"
                data-bs-toggle="modal"
                data-bs-target="#popupdeletecart"
              >
                <i class="bi bi-trash"></i>
                Xóa
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="cart__bottom">
        <div class="cart__bottom-left">
          <div class="selected-item">
            {{ selected.length }} sản phẩm đã chọn
          </div>
          <div class="delete-item">
            <button
              type="button"
              class="btn btn-outline-danger btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#popupdeletecarts"
            >
              <i class="bi bi-trash"></i>
              Xóa
            </button>
          </div>
        </div>
        <div class="cart__bottom-right">
          <div class="flex-item">
            Tổng thanh toán( {{ selected.length }} ):
            <span>{{ sumPrice }}</span>
          </div>
          <div class="tbutton buy-now">Mua hàng</div>
        </div>
      </div>
    </div>
    <TPopup
      popupId="popupdeletecart"
      popupTile="Xóa sản phẩm giỏ hàng"
      popupContent="Bạn có chắc chắn muốn xóa sản phẩm này không?"
      v-if="isShowPopup"
      @handleClick="handleClick"
    ></TPopup>
    <TFooter></TFooter>
  </div>
</template>
  
  <script>
import { useRoute } from "vue-router";
import { computed, ref, reactive, watch } from "vue";
import { useStore } from "vuex";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import AXIOS_CART from "@/api/cart";
import TPopup from "@/components/TPopup.vue";
import { defineComponent } from 'vue'
export default {
  name: "TCart",
  components: {
    THeader,
    TFooter,
    TPopup,
  },
  setup(emits) {
    const route = useRoute();
    const store = useStore();
    const dataSources = ref();
    const selected = ref([]);
    const isShowPopup = ref(true);
    /**
     * Bỏ sản phẩm
     */
    const removeProduct = (value) => {
      if (typeof value.NumProduct == "string") {
        value.NumProduct = parseInt(value.NumProduct);
      }
      if (value.NumProduct > 1) {
        value.NumProduct -= 1;
      }
    };
    /**
     * Thêm sản phẩm
     */
    const addProduct = (value) => {
      if (typeof value.NumProduct == "string") {
        value.NumProduct = parseInt(value.NumProduct);
      }
      value.NumProduct += 1;
    };
    /**
     * Tính tổng tiền
     */

    const sumPrice = computed(() => {
      var sum = 0;
      for (let key in selected.value) {
        sum += selected.value[key].NumProduct * selected.value[key].Price;
      }
      return sum;
    });

    /**
     * Hàm tính check all
     */
    const selectAll = computed({
      get: function () {
        return dataSources.value
          ? selected.value.length == dataSources.value.length
          : false;
      },
      set: function (value) {
        var selectedAll = [];
        if (value) {
          dataSources.value.forEach((item) => {
            selectedAll.push(item);
          });
        }
        selected.value = selectedAll;
      },
    });
    const getCartByUserId = async (param) => {
      try {
        let response = await AXIOS_CART.getCartByUserId(param);
        if (response) {
          dataSources.value = response.data;
        }
      } catch (err) {
        console.log(err);
      }
    };
    getCartByUserId(store.state.userInfo.UserId);
    // console.log(route.query);

    const handleClick = () => {
      console.log(123);
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
      handleClick,
      store,
      route,
      dataSources,
      selected,
      selectAll,
      removeProduct,
      addProduct,
      sumPrice,
      formatter,
      getCartByUserId,
    };
  },
};
</script>
  
  <style>
</style>