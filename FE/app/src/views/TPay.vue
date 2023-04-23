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
      <div class="pay__container">
        <div class="pay__content-left m-t-32">
          <h3>Chi tiết cá nhân</h3>
          <div class="pay__content-user">
            <div class="flex-item justify-between">
              <div class="user-fullname">
                <span class="placeholder-input">Họ và tên</span>
                <TInput></TInput>
              </div>
              <div class="user-phonenumber">
                <span class="placeholder-input">Số điện thoại</span>
                <TInput></TInput>
              </div>
            </div>
            <div class="user-email">
              <span class="placeholder-input">Địa chỉ Email</span>
              <TInput></TInput>
            </div>
          </div>
          <h3>Chi tiết vận chuyển</h3>
          <div class="pay__content-user">
            <div class="user-address">
              <span class="placeholder-input">Địa chỉ giao hàng</span>
              <TInput></TInput>
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
                  <input type="radio" class="user-pay-check" />
                </div>
              </div>
              <div class="user-paycard">
                <span class="placeholder-input">Thanh toán bằng thẻ</span>
                <div class="flex-item justify-between h-100 p-l-12 p-r-12">
                  <div class="flex-item justify-center">
                    <img
                      class="user-pay-img m-r-8"
                      src="https://www.coolmate.me/images/logo-zalopay.svg"
                      alt=""
                    />
                    <span>Ví điện tử ZaloPay</span>
                  </div>
                  <input type="radio" class="user-pay-check" />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="pay__content-right m-t-32">
          <h3>Giỏ hàng của tôi</h3>
          <div class="pay__content-cart flex-item">
            <img
              src="@/assets/img/product/1.1nike.png"
              alt=""
              class="pay__content-cart-img"
            />
            <div class="pay__content-cart-info">
              <div class="cart-nameproduct">Nike</div>
              <div class="cart-size">Size: 40</div>
              <div class="cart-price">100000đ</div>
            </div>
          </div>
          <div class="pay__content-footer ">
            <div class="flex-item justify-between"><h4>Tổng thanh toán:</h4>
            <span >1000000đ</span></div>
            <button class="tbutton pay-btn m-t-20">Thanh toán</button>
          </div>
        </div>
      </div>
    </div>
    <TFooter></TFooter>
  </div>
</template>

<script>
import { useRoute } from "vue-router";
import { computed, ref, reactive, watch } from "vue";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TInput from "@/components/TInput.vue";

export default {
  name: "TPay",
  components: {
    THeader,
    TFooter,
    TInput,
  },
};
</script>

<style>
</style>