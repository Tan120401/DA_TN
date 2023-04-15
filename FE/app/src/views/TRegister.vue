<template>
  <div id="main">
    <div class="modal-form">
      <img src="../assets/img/login.jpg" alt="" />
      <div class="modal1">
        <router-link to="/"><h1 class="logo">Nine Store</h1></router-link>
        <form
          class="register-form"
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
          <div style="position: relative">
            <span class="register-fail" v-if="isShowDuplicateUser"
              >Tên đăng nhập đã tồn tại</span
            >
          </div>

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
            <div class="dont-account-text m-r-8">
              Bạn đã có tài khoản?
              <router-link to="/Login"> <span>Đăng nhập</span></router-link>
            </div>
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
import USER_AXIOS from "@/api/user";
import { reactive, ref, watch } from "vue";
import { useStore } from "vuex";
import _ from "lodash";
import router from "@/router/router";

export default {
  name: "TRegister",
  components: {
    TInput,
  },
  setup() {
    const isRegister = ref(false);
    const isAccept = ref(false);
    const userRegister = reactive(_.cloneDeep(USER_REGISTER));
    const isShowDuplicateUser = ref(false);
    const validateForm = ref(null);
    const confirmPassword = ref();
    const isConfirmPassError = ref(false);
    /**
     * Hàm đăng ký tài khoản
     * Created by NVTAN(04/04/2023)
     */
    const onRegister = async () => {
      var inValid = validateData();
      if (inValid.isValidate) {
        if (confirmPassword.value != userRegister.Password) {
          isConfirmPassError.value = true;
        } else {
          isConfirmPassError.value = false;
          try {
            let response = await USER_AXIOS.setRegister(userRegister);
            if (response) {
              router.push({ path: "/Login" });
            }
          } catch (err) {
            console.log(err);
            if (err.response.data.ErrorCode == 4) {
              isShowDuplicateUser.value = true;
            } else {
              isShowDuplicateUser.value = false;
            }
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
     * Hàm chọn chấp nhận điều khoản
     * Created by NVTAN (30/03/2023)
     */
    const onAccept = () => {
      isAccept.value = !isAccept.value;
    };
    return {
      isRegister,
      isAccept,
      isShowDuplicateUser,
      userRegister,
      validateForm,
      confirmPassword,
      isConfirmPassError,
      validateData,
      validateInput,
      onAccept,
      onRegister,
    };
  },
};
</script>
  
  <style>
</style>