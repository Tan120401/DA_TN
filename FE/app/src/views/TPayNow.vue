<template>
  <div id="main">
    <THeader></THeader>
    <div id="cart">
      <h2>Thanh toán</h2>
      <div class="cart__note">
        <i class="fa-solid fa-circle-exclamation m-r-8"></i>
        <span
          >Nine store thông báo: quý khách vui lòng kiểm tra thông tin đầy đủ
          trước khi đặt hàng</span
        >
      </div>
      <div class="pay__container" ref="validateForm">
        <div class="pay__content-left m-t-32">
          <h3>Chi tiết cá nhân</h3>
          <div class="pay__content-user">
            <div class="flex-item justify-between">
              <div class="user-fullname">
                <span class="placeholder-input">Họ và tên</span>
                <TInput
                  name="Họ và tên"
                  :rules="['Empty']"
                  v-model="billOptions.UserName"
                ></TInput>
              </div>
              <div class="user-phonenumber">
                <span class="placeholder-input">Số điện thoại</span>
                <TInput
                  name="Số điện thoại"
                  :rules="['Empty']"
                  v-model="billOptions.PhoneNumber"
                ></TInput>
              </div>
            </div>
          </div>
          <h3>Chi tiết vận chuyển</h3>
          <div class="pay__content-user">
            <div class="user-address">
              <span class="placeholder-input">Địa chỉ giao hàng</span>
              <TInput
                name="Địa chỉ giao hàng"
                :rules="['Empty']"
                v-model="billOptions.Address"
              ></TInput>
            </div>
          </div>
          <h3>Hình thức thanh toán</h3>
          <div class="pay__content-user">
            <div class="flex-item justify-between">
              <div class="user-pay">
                <span class="placeholder-input">Thanh toán khi nhận hàng</span>
                <div class="flex-item justify-between h-100 p-l-12 p-r-12">
                  <div class="flex-item justify-center">
                    <img
                      class="user-pay-img m-r-8"
                      src="https://lzd-img-global.slatic.net/g/tps/tfs/TB1ZP8kM1T2gK0jSZFvXXXnFXXa-96-96.png_2200x2200q75.jpg"
                      alt=""
                    />
                    <span>Thanh toán khi nhận hàng</span>
                  </div>
                  <input
                    type="radio"
                    class="user-pay-check"
                    @click="paymentOnDelivery"
                    :v-model="!isPay"
                    :checked="!isPay"
                  />
                </div>
              </div>
              <div class="user-paycard">
                <span class="placeholder-input">Thanh toán bằng thẻ</span>
                <div class="flex-item justify-between h-100 p-l-12 p-r-12">
                  <div class="flex-item justify-center">
                    <img
                      class="user-pay-img m-r-8"
                      src="https://upload.wikimedia.org/wikipedia/vi/f/fe/MoMo_Logo.png"
                      alt=""
                    />
                    <span>Ví điện tử Momo</span>
                  </div>
                  <input
                    type="radio"
                    class="user-pay-check"
                    @click="paymentViaCard"
                    :v-model="isPay"
                    :checked="isPay"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="pay__content-right m-t-32">
          <h3>Sản phẩm thanh toán</h3>
          <div class="pay__content-cart flex-item">
            <img
              :src="require(`@/assets/img/product/${cartProduct.ImgProduct}`)"
              alt=""
              class="pay__content-cart-img"
            />
            <div class="pay__content-cart-info">
              <div class="cart-nameproduct">{{ cartProduct.ProductName }}</div>
              <div class="cart-size">Size: {{ cartProduct.SizeProduct }}</div>
              <div class="cart-price">
                Đơn giá:
                {{
                  formatter.format(
                    cartProduct.Price * (1 - cartProduct.Discount / 100)
                  )
                }}
              </div>
              <div>Số lượng: {{ cartProduct.NumProduct }}</div>
            </div>
          </div>
          <div class="pay__content-footer">
            <div class="flex-item justify-between">
              <h4>Tổng thanh toán:</h4>
              <span>{{ formatter.format(sumPrice) }}</span>
            </div>
            <button class="tbutton pay-btn m-t-20" @click="onBuyNow">
              Thanh toán
            </button>
          </div>
        </div>
      </div>
    </div>
    <TFooter></TFooter>
  </div>
</template>
  
  <script>
import { useRoute } from "vue-router";
import { computed, ref, reactive, onMounted } from "vue";
import _ from "lodash";
import { v4 as uuidv4 } from "uuid";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TInput from "@/components/TInput.vue";
import { validateData } from "@/js/validateion";
import {
  BILL_OPTION,
  ORDER_OPTIONS,
  ORDERDETAIL_OPTIONS,
} from "@/js/constrant.js";
import AXIOS_ORDER from "@/api/order";
import AXIOS_ORDER_DETAIL from "@/api/orderdetail";
import AXIOS_BILL from "@/api/bill";
import AXIOS_CART from "@/api/cart";
import AXIOS_MOMO from "@/api/momo";
import { Enum } from "@/js/base/enum";
import router from "@/router/router";
import { useStore } from "vuex";
import { notification } from "ant-design-vue";
import AXIOS_SIZE from "@/api/size";
export default {
  name: "TPayNow",
  components: {
    THeader,
    TFooter,
    TInput,
  },
  setup() {
    const store = useStore();
    const route = useRoute();
    const cartProduct = ref([]);
    const isPay = ref(false);
    const validateForm = ref(null);
    const billOptions = reactive(_.cloneDeep(BILL_OPTION));
    const orderOptions = reactive(_.cloneDeep(ORDER_OPTIONS));
    const orderDetailOptions = reactive(_.cloneDeep(ORDERDETAIL_OPTIONS));
    const listCartId = ref([]);
    /**
     * Nhận giá trị thông qua router query
     */
    cartProduct.value = JSON.parse(localStorage.getItem("productBuyNow"));
    const lstSizeToPay = JSON.parse(localStorage.getItem("lstSizeToPay"));

    const sumPrice = computed(() => {
      var sum = 0;
      sum =
        cartProduct.value.NumProduct *
        cartProduct.value.Price *
        (1 - cartProduct.value.Discount / 100);
      return sum;
    });
    /**
     * Thanh toán khi nhận hàng
     */
    const paymentOnDelivery = () => {
      isPay.value = !isPay.value;
    };
    const paymentByMomo = async (params) => {
      try {
        let response = await AXIOS_MOMO.paymentMomo(params);
        if (response) {
          console.log(response.data);
          window.location.href = response.data;
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Thanh toán qua thẻ ngân hàng
     */
    const paymentViaCard = () => {
      store.dispatch("addToBill", billOptions);
      isPay.value = !isPay.value;
      paymentByMomo({
        amount: "100000",
        returnUrl: window.location.href,
      });
    };
    onMounted(() => {
      const urlParams = new URLSearchParams(window.location.href.split("?")[1]);
      const errorCode = urlParams.get("errorCode");
      if (errorCode && errorCode == Enum.PAY_CODE.SUCCESS) {
        Object.assign(
          billOptions,
          JSON.parse(localStorage.getItem("billInfo"))
        );
        isPay.value = true;
        notification.success({ message: "Thanh toán thành công" });
        setTimeout(async () => {
          await onBuyNow();
          console.log("test pay");
        }, 1000);
      } else if (errorCode && errorCode != Enum.PAY_CODE.SUCCESS) {
        Object.assign(
          billOptions,
          JSON.parse(localStorage.getItem("billInfo"))
        );
        isPay.value = false;
        notification.error({ message: "Thanh toán thất bại" });
      }
    });
    /**
     * insert bảng order
     */
    const insertToOrder = async (params) => {
      try {
        let response = await AXIOS_ORDER.insertOrder(params);
        if (response) {
          if (response) {
            billOptions.OrderId = orderOptions.OrderId;
            billOptions.SumPrice = sumPrice;
            insertToBill(billOptions);
          }
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Insert bảng orderDetail
     */
    const insertToOrderDetail = async (params) => {
      try {
        let response = await AXIOS_ORDER_DETAIL.insertOrderDetail(params);
        if (response) {
          router.push({ path: "/Order/0" });
          notification.success({ message: "Mua hàng thành công" });
        }
      } catch (err) {
        console.log(err);
      }
    };
    const updateToSize = async (id, params) => {
      try {
        let response = await AXIOS_SIZE.updateToSize(id, params);
        if (response) {
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Insert bảng Bill
     */
    const insertToBill = async (params) => {
      try {
        let response = await AXIOS_BILL.insertBill(params);
        if (response) {
          orderDetailOptions.OrderId = orderOptions.OrderId;
          orderDetailOptions.ProductId = cartProduct.value.ProductId;
          orderDetailOptions.Quantity = cartProduct.value.NumProduct;
          orderDetailOptions.Price = cartProduct.value.Price;
          orderDetailOptions.Size = cartProduct.value.SizeProduct;
          insertToOrderDetail(orderDetailOptions);
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Hàm update lại số lượng sản phẩm
     */
    const onUpdateQuantity = () => {
      cartProduct.value.ProductId == lstSizeToPay.ProductId &&
        cartProduct.value.SizeProduct == lstSizeToPay.SizeNumber;
      updateToSize(lstSizeToPay.SizeId, {
        Quantity: lstSizeToPay.Quantity - cartProduct.value.NumProduct,
      });
    };
    /**
     * Click thanh toán
     */
    const onBuyNow = () => {
      /**
       * Validate
       */
      var lstInput = validateForm.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        orderOptions.UserId = store.state.userInfo.UserId;
        orderOptions.OrderId = uuidv4();
        if (isPay.value) {
          billOptions.IsPay = 1;
        } else {
          billOptions.IsPay = 0;
        }
        insertToOrder(orderOptions);
        onUpdateQuantity();
      }
    };

    const formatter = new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    });

    return {
      store,
      sumPrice,
      lstSizeToPay,
      paymentByMomo,
      validateForm,
      insertToOrder,
      insertToOrderDetail,
      onBuyNow,
      route,
      isPay,
      paymentOnDelivery,
      paymentViaCard,
      cartProduct,
      formatter,
      billOptions,
      orderOptions,
      orderDetailOptions,
      insertToBill,
      listCartId,
      updateToSize,
      onUpdateQuantity,
    };
  },
};
</script>
  
  <style>
</style>