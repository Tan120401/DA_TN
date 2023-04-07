<template>
  <div id="main">
    <div class="modal-form">
      <img src="../assets/img/login.jpg" alt="" />
      <div class="modal">
        <router-link to="/"><h1 class="logo">Nine Store</h1></router-link>
        <form
          class="login-form"
          @submit.prevent="onLogin"
          v-if="isLogin"
          ref="validateForm"
        >
          <h1 class="login-title">Đăng nhập</h1>
          <TInput
            type="text"
            placeholder="Tên đăng nhập"
            name="Tên đăng nhập"
            v-model="user.username"
            :rules="['Empty']"
          ></TInput>
          <TInput
            type="password"
            placeholder="Mật khẩu"
            name="Mật khẩu"
            v-model="user.password"
            :rules="['Empty']"
          ></TInput>

          <button type="submit" class="tbutton btn-login">Đăng nhập</button>
          <div style="position: relative">
            <span class="login-fail" v-if="isShowLoginFail"
              >Tên đăng nhập hoặc mật khẩu không chính xác</span
            >
          </div>
          <div class="dont-account">
            <div class="dont-account-text m-r-8 m-t-20">
              Bạn chưa có tài khoản?
            </div>
            <span @click="onShowRegister" class="m-t-20">Đăng ký</span>
          </div>
        </form>
        <form
          class="register-form"
          v-if="isRegister"
          @submit.prevent="onRegister"
          ref="validateForm"
        >
          <h1 class="login-title">Đăng ký tài khoản</h1>
          <TInput
            type="text"
            placeholder="Email"
            v-model="userRegister.Email"
            name="Email"
            :rules="['Empty', 'Email']"
          ></TInput>
          <TInput
            type="text"
            placeholder="Họ và tên"
            v-model="userRegister.FullName"
            name="Tên đăng nhập"
            :rules="['Empty']"
          ></TInput>
          <TInput
            type="text"
            placeholder="Tên đăng nhập"
            v-model="userRegister.UserName"
            name="Tên đăng nhập"
            :rules="['Empty']"
          ></TInput>
          <TInput
            type="password"
            placeholder="Mật khẩu"
            v-model="userRegister.Password"
            name="Mật khẩu"
            :rules="['Empty']"
          ></TInput>
          <TInput
            type="password"
            placeholder="Nhập lại mật khẩu"
            v-model="confirmPassword"
            name="Xác nhận mật khẩu"
            :rules="['Empty']"
          ></TInput>
          <span v-if="isConfirmPassError" class="confirm-error"
            >Xác nhận mật khẩu không đúng</span
          >
          <button type="submit" class="tbutton btn-login">Đăng ký</button>
          <div class="dont-account m-t-12">
            <div class="dont-account-text m-r-8">Bạn đã có tài khoản?</div>
            <span @click="onShowLogin">Đăng nhập</span>
          </div>
          <div class="accept m-t-12" @click="onAccept">
            <i
              class="fa-solid fa-circle-check m-r-8"
              :style="[isAccept ? 'color:red' : '']"
            ></i>
            <div class="dont-account-text">
              Đồng ý với điều khoản và bảo mật của Nine store
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import TInput from "@/components/TInput.vue";

import { USER, USER_REGISTER } from "@/js/data";
import  USER_AXIOS  from "@/api/user";
import { reactive, ref, watch } from "vue";
import { useStore } from "vuex";
import _ from "lodash";

export default {
  name: "TLogin",
  components: {
    TInput,
  },
  setup() {
    const isLogin = ref(true);
    const isRegister = ref(false);
    const isAccept = ref(false);
    const user = reactive(_.cloneDeep(USER));
    const userRegister = reactive(_.cloneDeep(USER_REGISTER));
    const isShowLoginFail = ref(false);
    const $store = useStore();
    const validateForm = ref(null);

    const confirmPassword = ref();
    const isConfirmPassError = ref(false);
    /**
     * Hàm đăng nhập
     * Created by NVTAN(04/04/2023)
     */
    const onLogin = async () => {
      var inValid = validateData();
      if (inValid.isValidate) {
        await $store.dispatch("fetchLogin", user);
        isShowLoginFail.value = !$store.state.isLogin;
      }
    };
    /**
     * Hàm đăng ký tài khoản
     * Created by NVTAN(04/04/2023)
     */
    const onRegister = async() => {
      var inValid = validateData();
      if (inValid.isValidate) {
        if (confirmPassword.value != userRegister.Password) {
          isConfirmPassError.value = true;
        } else {
          isConfirmPassError.value = false;
          try {
            let response = await USER_AXIOS.setRegister(userRegister);
            console.log(response)
          } catch (err) {
            console.log(err);
          }
        }
      }
    };

    /**
     * Hàm validate dữ liệu
     * Created by NVTAN(04/04/2023)
     */
    const validateData = () => {
      var isValidate = true;
      var lstInput = validateForm.value.querySelectorAll("input");
      if (lstInput) {
        for (var i = 0; i < lstInput.length; i++) {
          var component = lstInput[i].__vueParentComponent;
          if (component.props.rules) {
            let validate = validateInput(lstInput[i]);
            if (validate) {
              isValidate = false;
            }
          }
        }
      }
      return {
        isValidate: isValidate,
      };
    };
    /**
     * Hàm validate ô input
     * Created by NVTAN(04/04/2023)
     */
    const validateInput = (input) => {
      var component = input.__vueParentComponent;
      // Lấy ra danh sách prop
      var props = component.props;
      var setup = component.setupState;
      // Lấy ra danh sách validate
      var rules = props.rules;
      var name = props.name;
      var val = input.value;
      let inValid = true;
      if (rules && rules.length) {
        for (var i = 0; i < rules.length; i++) {
          if (rules[i] == "Empty") {
            if (!val || val.trim() == "") {
              setup.isValidate = true;
              setup.messageValidate = name + " không được để trống.";
              inValid = true;
            } else {
              inValid = false;
            }
          } else if (rules[i] == "Email") {
            const regexEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (val && !val.match(regexEmail)) {
              setup.isValidate = true;
              setup.messageValidate = name + " không đúng định dạng.";
              inValid = true;
            } else {
              inValid = false;
            }
          }
          return inValid;
          // else if (rules[i] == "Number") {
          //   const regexNumber = /^\d+$/;
          //   if (val && !val.match(regexNumber)) {
          //     setup.isEmpty = true;
          //     setup.validateError = name + " không đúng định dạng.";
          //     return {
          //       isValid: false,
          //       textValidate: setup.validateError,
          //     };
          //   } else {
          //     return {
          //       isValid: true,
          //     };
          //   }
          // }
        }
      }
    };

    /**
     * Hàm hiện form đăng ký
     * Created by NVTAN (30/03/2023)
     */
    const onShowRegister = () => {
      isRegister.value = true;
      isLogin.value = false;
    };

    /**
     * Hàm hiện form đăng nhập
     * Created by NVTAN (30/03/2023)
     */
    const onShowLogin = () => {
      isRegister.value = false;
      isLogin.value = true;
    };

    /**
     * Hàm chọn chấp nhận điều khoản
     * Created by NVTAN (30/03/2023)
     */
    const onAccept = () => {
      isAccept.value = !isAccept.value;
    };
    return {
      isLogin,
      isRegister,
      isAccept,
      isShowLoginFail,
      user,
      userRegister,
      validateForm,
      confirmPassword,
      isConfirmPassError,
      validateData,
      validateInput,
      onShowRegister,
      onShowLogin,
      onAccept,
      onLogin,
      onRegister,
    };
  },
};
</script>

<style>
.confirm-error {
  position: relative;
  font-size: 11px;
  color: var(--sub-color);
  top: -10px;
}
.modal {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(79, 76, 76, 0.4);
  z-index: 10;
}
.modal-form img {
  width: 100%;
  height: calc(100vh - 4px);
}
.modal .logo {
  position: absolute;
  left: 128px;
  top: 56px;
  color: var(--primary-color);
  z-index: 100;
}
.login-form,
.register-form {
  width: 488px;
  height: 544px;
  padding: 64px;
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  right: 128px;
  background-color: #fff;
  border-radius: 32px;
}
.register-form {
  height: 644px;
}
.register-title {
  margin: 0 auto;
  margin-bottom: 16px;
  text-align: center;
}
.login-title {
  margin: 0 auto;
  margin-bottom: 32px;
  text-align: center;
}
.tbutton.btn-login {
  width: 100%;
  height: 48px;
  font-size: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.dont-account,
.accept {
  font-size: 14px;
  display: flex;
  align-items: center;
}
.accept i {
  opacity: 0.5;
}
.dont-account-text {
  opacity: 0.6;
}
.dont-account span {
  color: var(--primary-color);
  cursor: pointer;
  font-weight: 70;
}

.confirm-password {
  margin-top: 20px;
}

.login-fail {
  position: absolute;
  top: 6px;
  font-size: 11px;
  color: var(--sub-color);
  font-style: italic;
}
</style>