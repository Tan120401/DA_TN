<template>
  <div id="main">
    <div class="modal-form">
      <img src="../assets/img/login.jpg" alt="" />
      <div class="modal1">
        <router-link to="/"><h1 class="logo">Nine Store</h1></router-link>
        <form class="login-form" @submit.prevent="onLogin" ref="validateForm">
          <h1 class="login-title">Đăng nhập</h1>
          <TInput
            type="text"
            placeholder="Tên đăng nhập"
            name="Tên đăng nhập"
            v-model="user.username"
            :rules="['Email']"
          ></TInput>
          <TInput
            type="password"
            placeholder="Mật khẩu"
            name="Mật khẩu"
            v-model="user.password"
            :rules="['Password']"
          ></TInput>
          <button type="submit" class="tbutton btn-login">Đăng nhập</button>
          <div style="position: relative">
            <span class="login-fail" v-if="isShowLoginFail"
              >Tên đăng nhập hoặc mật khẩu không chính xác</span
            >
          </div>
          <div
            class="dont-account d-flex align-items-center m-r-8 m-t-20 justify-content-between"
          >
            <div class="dont-account-text">
              Bạn chưa có tài khoản?
              <router-link to="/Register"
                ><span class="m-t-20">Đăng ký</span></router-link
              >
            </div>
            <span @click="onShowPoupForgotPass">Quên mật khẩu?</span>
          </div>
        </form>
      </div>
    </div>
  </div>

  <TPopup
    popupTile="Quên mật khẩu"
    v-if="isShowPopupForgotPassword"
    @hidePopup="hidePopupForgotPassword"
  >
    <template #content>
      <div ref="validateForgotPass">
        <div class="mb-1">Vui lòng nhập tên tài khoản của bạn</div>
        <t-input
          v-model="emailOfUser"
          :rules="['Email']"
          name="Email"
        ></t-input>
        <div style="position: relative; top: -18px">
          <span class="login-fail" v-if="isShowForgotFail"
            >Tài khoản không chính xác</span
          >
        </div>
      </div>
    </template>
    <template #footer>
      <button type="button" class="btn btn-primary" @click="onSubmitForgotPass">
        Xác nhận
      </button>
    </template>
  </TPopup>
</template>

<script>
import TInput from "@/components/TInput.vue";
import TPopup from "@/components/TPopup.vue";
import { USER } from "@/js/data";
import { reactive, ref, watch } from "vue";
import { useStore } from "vuex";
import _ from "lodash";
import { validateData } from "@/js/validateion";
import USER_AXIOS from "@/api/user";
import { notification } from "ant-design-vue";
export default {
  name: "TLogin",
  components: {
    TInput,
    TPopup,
  },
  setup() {
    const isLogin = ref(true);
    const user = reactive(_.cloneDeep(USER));
    const isShowLoginFail = ref(false);
    const $store = useStore();
    const validateForm = ref(null);
    const isShowPopupForgotPassword = ref(false);
    const validateForgotPass = ref(null);
    const emailOfUser = ref();
    const isShowForgotFail = ref();

    /**
     * Hiển thị form quên mật khẩu
     */
    const onShowPoupForgotPass = () => {
      isShowPopupForgotPassword.value = true;
      emailOfUser;
    };
    /**
     * Ẩn form quên mật khẩu
     */
    const hidePopupForgotPassword = () => {
      isShowPopupForgotPassword.value = false;
    };

    /**
     * Nhấn quên mật khẩu
     */
    const onSubmitForgotPass = async () => {
      var lstInput = validateForgotPass.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        try {
          let response = await USER_AXIOS.forgotPassword(emailOfUser.value);
          if (response) {
            notification.success({
              message: "Mật khẩu mới đã được gửi về tài khoản gmail của bạn!",
            });
            setTimeout(() => {
              hidePopupForgotPassword();
            }, 1000);
          }
        } catch (error) {
          console.log(error);
          if (error.response.data.ErrorCode == 5) {
            isShowForgotFail.value = true;
          }
        }
      }
    };
    /**
     * Hàm đăng nhập
     * Created by NVTAN(04/04/2023)
     */
    const onLogin = async () => {
      var lstInput = validateForm.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        await $store.dispatch("fetchLogin", user);
        isShowLoginFail.value = !$store.state.isLogin;
      }
    };
    return {
      isShowForgotFail,
      isLogin,
      isShowLoginFail,
      user,
      validateForm,
      onLogin,
      isShowPopupForgotPassword,
      validateForgotPass,
      hidePopupForgotPassword,
      onShowPoupForgotPass,
      onSubmitForgotPass,
      emailOfUser,
    };
  },
};
</script>

<style>
</style>