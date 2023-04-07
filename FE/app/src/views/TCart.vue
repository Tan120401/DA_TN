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
            <th style="width: 220px">Đơn giá</th>
            <th style="width: 520px">Thành tiền</th>
          </tr>
          <tr v-for="item in dataSources" :key="item.id">
            <td class="flex-item just-center">
              <input type="checkbox" v-model="selected" :value="item" />
            </td>
            <td style="text-align: left">
              <div class="cart__item">
                <div class="cart__item-img">
                  <img :src="require(`@/assets/img/${item.picture}`)" alt="" />
                </div>
                <div class="cart__item-info">
                  <h4>{{ item.productname }}</h4>
                  <div class="cart__item-sale">
                    Giảm {{ 100 - (item.pricediscount / item.price) * 100 }}%
                  </div>
                </div>
              </div>
            </td>
            <td>{{ item.size }}</td>
            <td>
              <div class="product__detail-num m-t-0">
                <i
                  class="fa-solid fa-minus product__detail-remove"
                  @click="removeProduct(item)"
                ></i>
                <div class="number">{{ item.numproduct }}</div>
                <i
                  class="fa-solid fa-plus product__detail-add"
                  @click="addProduct(item)"
                ></i>
              </div>
            </td>
            <td>{{ formatter.format(item.pricediscount) }}</td>
            <td>
              <span class="product__price-discount m-r-8">{{
                formatter.format(item.price * item.numproduct)
              }}</span>
              <span>{{
                formatter.format(item.pricediscount * item.numproduct)
              }}</span>
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
            Xóa
            <i class="fa-solid fa-trash-can"></i>
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
    <TFooter></TFooter>
  </div>
</template>
  
  <script>
import { useRoute } from "vue-router";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import { PRODUCTS_CART } from "@/js/data";
import { computed, ref, reactive, watch } from "vue"; 

export default {
  name: "TCart",
  components: {
    THeader,
    TFooter,
  },
  setup() {
    const route = useRoute();
    const dataSources = reactive(PRODUCTS_CART);
    const selected = ref([]);

    /**
     * Bỏ sản phẩm
     */
    const removeProduct = (value) => {
      if (value.numproduct > 0) {
        value.numproduct -= 1;
      }
    };
    /**
     * Tính tổng tiền
     */

    const sumPrice = computed(() => {
      var sum = 0;
      for (let key in selected.value) {
        sum +=
          selected.value[key].numproduct * selected.value[key].pricediscount;
      }
      return sum;
    });
    /**
     * Thêm sản phẩm
     */
    const addProduct = (value) => {
      value.numproduct += 1;
    };
    /**
     * Hàm tính check all
     */
    const selectAll = computed({
      get: function () {
        return dataSources
          ? selected.value.length == dataSources.length
          : false;
      },
      set: function (value) {
        var selectedAll = [];
        if (value) {
          dataSources.forEach((item) => {
            selectedAll.push(item);
          });
        }
        selected.value = selectedAll;
      },
    });

    /**
     * Định dạng tiền tệ VND
     */
    const formatter = new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    });
    return {
      route,
      dataSources,
      selected,
      selectAll,
      removeProduct,
      addProduct,
      sumPrice,
      formatter,
    };
  },
};
</script>
  
  <style>
</style>