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
            <th style="width: 320px">Thành tiền</th>
            <th style="width: 100px"></th>
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
            <td style="text-align: center">
              <div class="product__detail-num m-t-0">
                <i
                  class="fa-solid fa-minus product__detail-remove"
                  @click="removeProduct(item)"
                ></i>
                <input
                  type="text"
                  class="number"
                  v-model="item.NumProduct"
                  @change="onChangeValue(item)"
                />
                <i
                  class="fa-solid fa-plus product__detail-add"
                  @click="addProduct(item)"
                ></i>
              </div>
              <div style="position: relative">
                <span
                  class="numproduct-fail"
                  style="left: 0"
                  v-if="item.NumProduct > item.Quantity"
                  >Số lượng lớn hơn số còn lại.</span
                >
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
                @click="onShowPopup(item)"
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
              @click="onShowPopupMulpty"
              class="btn btn-outline-danger btn-sm"
            >
              <i class="bi bi-trash"></i>
              Xóa
            </button>
          </div>
        </div>
        <div class="cart__bottom-right">
          <div class="flex-item">
            Tổng thanh toán( {{ selected.length }} ):
            <span>{{ formatter.format(sumPrice) }}</span>
          </div>
          <div class="tbutton buy-now" @click="onBuyItem">Mua hàng</div>
        </div>
      </div>
    </div>
    <TPopup
      popupTile="Xóa sản phẩm giỏ hàng"
      popupContent="Bạn có chắc chắn muốn xóa sản phẩm này không?"
      v-if="isShowPopup"
      @handleClick="handleClickPopup"
      @hidePopup="hidePopup"
    ></TPopup>

    <TPopup
      popupTile="Xóa sản phẩm giỏ hàng"
      popupContent="Bạn có chắc chắn muốn xóa những sản phẩm đã chọn không không?"
      v-if="isShowPopupMulpty"
      @handleClick="handleClickPopupMulpty"
      @hidePopup="hidePopup"
    ></TPopup>
    <TFooter></TFooter>
  </div>
</template>
  
  <script>
import { useRoute } from "vue-router";
import { computed, ref, reactive, watch } from "vue";
import { useStore } from "vuex";
import router from "@/router/router";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import AXIOS_CART from "@/api/cart";
import AXIOS_PRODUCT from "@/api/Product";
import TPopup from "@/components/TPopup.vue";
import AXIOS_SIZE from "@/api/size";

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
    const isShowPopup = ref(false);
    const isShowPopupMulpty = ref(false);
    const cartIdSelected = ref();
    const listCartIdSelected = ref();
    const popupContent = ref();
    const productDetail = ref([]);
    const sizeOptions = ref([]);
    const isOverLoadNumProduct = ref(false);
    const lstCartIdQuery = ref([]);
    /**
     * Thay đổi số lượng sản phẩm
     */
    const onChangeValue = (value) => {
      getProductById(value.ProductId);
      getAllSizeProduct(value.ProductId);
      sizeOptions.value.forEach((item, index) => {
        if (item.SizeNumber == value.SizeProduct) {
          value.Quantity = item.Quantity;
        }
      });
      if (value.NumProduct > value.Quantity) {
        isOverLoadNumProduct.value = true;
      } else {
        isOverLoadNumProduct.value = false;
      }
    };

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
      onChangeValue(value);
    };
    /**
     * Thêm sản phẩm
     */
    const addProduct = (value) => {
      if (typeof value.NumProduct == "string") {
        value.NumProduct = parseInt(value.NumProduct);
      }
      value.NumProduct += 1;
      onChangeValue(value);
    };
    /**
     * Hàm lấy thông tin sản phẩm thông qua id
     * @param {Id product} param
     */
    const getProductById = async (param) => {
      try {
        let response = await AXIOS_PRODUCT.getProductById(param);
        if (response) {
          productDetail.value = response.data[0];
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Tính tổng tiền
     */

    const sumPrice = computed(() => {
      var sum = 0;
      for (let key in selected.value) {
        sum +=
          selected.value[key].NumProduct *
          selected.value[key].Price *
          (1 - selected.value[key].Discount / 100);
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
          dataSources.value.forEach((item) => {
            item.Quantity = 999;
          });
        }
      } catch (err) {
        console.log(err);
      }
    };
    getCartByUserId(store.state.userInfo.UserId);

    /**
     * Hiện popup
     */
    const onShowPopup = (item) => {
      isShowPopup.value = true;
      cartIdSelected.value = item.CartId;
    };
    /**
     * Hiện popup xóa nhiều
     */
    const onShowPopupMulpty = () => {
      isShowPopupMulpty.value = true;
      let selectedCart = [];
      selected.value.forEach((item) => {
        selectedCart.push(item.CartId);
      });
      listCartIdSelected.value = selectedCart;
    };

    /**
     * Ẩn popup
     */
    const hidePopup = () => {
      isShowPopup.value = false;
      isShowPopupMulpty.value = false;
    };
    /**
     * Xóa sản phẩm giỏ hàng
     * @param {Cart id} param
     */
    const delteCartById = async (param) => {
      try {
        let response = await AXIOS_CART.deleteCartById(param);
        if (response) {
          getCartByUserId(store.state.userInfo.UserId);
          hidePopup();
          selected.value.forEach((element, index) => {
            if (element.CartId == cartIdSelected.value) {
              selected.value.splice(index, 1);
            }
          });
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Xóa nhiều sản phẩm trong giỏ hàng
     */
    const deleteMulptyCart = async (params) => {
      try {
        let response = await AXIOS_CART.deleteMulptyCart(params);
        if (response) {
          getCartByUserId(store.state.userInfo.UserId);
          hidePopup();
          selected.value = [];
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Khi ấn nút xóa
     */
    const handleClickPopup = () => {
      delteCartById(cartIdSelected.value);
    };
    /**
     * Khi ấn nút xóa nhiều
     */
    const handleClickPopupMulpty = () => {
      if (listCartIdSelected.value) {
        deleteMulptyCart(listCartIdSelected.value);
      }
    };
    const onBuyItem = () => {
      if (isOverLoadNumProduct.value) {
        return;
      } else if (selected.value.length > 0) {
        selected.value.forEach((item) => {
          lstCartIdQuery.value.push(item.CartId);
        });
        console.log(lstCartIdQuery.value);
        router.push({
          path: "/Pay",
          query: {
            lstCarId: JSON.stringify(lstCartIdQuery.value),
          },
        });
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
      lstCartIdQuery,
      isOverLoadNumProduct,
      sizeOptions,
      getAllSizeProduct,
      onChangeValue,
      getProductById,
      productDetail,
      onBuyItem,
      isShowPopup,
      isShowPopupMulpty,
      popupContent,
      cartIdSelected,
      listCartIdSelected,
      handleClickPopup,
      handleClickPopupMulpty,
      delteCartById,
      hidePopup,
      onShowPopup,
      onShowPopupMulpty,
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
      deleteMulptyCart,
    };
  },
};
</script>
  
  <style>
</style>