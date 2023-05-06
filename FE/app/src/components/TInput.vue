<template>
  <div style="position: relative">
    <input
      class="Tinput"
      :type="type"
      :placeholder="placeholder"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
      :class="{ isValid: isValidate }"
    />
    <div :class="{ isEmpty: isValidate }" v-if="isValidate" class="m-t-8">
      {{ messageValidate }}
    </div>
  </div>
</template>
<script>
import { watch, ref } from "vue";
export default {
  name: "TInput",
  props: ["type", "placeholder", "modelValue", "name", "rules"],
  setup(props) {
    const isValidate = ref();
    const messageValidate = ref();
    /**
     * Kiểm tra giá trị ô input thay đổi thực hiện validate
     * Created by NVTAN(04/04/2023)
     */
    watch(
      () => props.modelValue,
      (val) => {
        if (val.trim() == "") {
          isValidate.value = true;
          messageValidate.value = props.name + " không được để trống";
        } else {
          isValidate.value = false;
        }
        for (var i = 0; i < props.rules.length; i++) {
          if (props.rules[i] == "Email") {
            const regexEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (!val || val.trim() == "") {
              isValidate.value = true;
              messageValidate.value = props.name + " không được để trống.";
            } else if (val && !val.match(regexEmail)) {
              isValidate.value = true;
              messageValidate.value = props.name + " không đúng định dạng.";
            } else {
              isValidate.value = false;
            } 
          } else if (props.rules[i] == "Password") {
            const regexPassword = new RegExp(
              "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$"
            );
            if (!val || val.trim() == "") {
              isValidate.value = true;
              messageValidate.value = props.name + " không được để trống.";
            } else if (val && !regexPassword.test(val)) {
              isValidate.value = true;
              messageValidate.value =
                " Tối thiểu 8 ký tự và có chứa ít nhất một chữ cái và một chữ số";
            } else {
              isValidate.value = false;
            }
          }
        }
      }
    );
    return {
      isValidate,
      messageValidate,
    };
  },
};
</script>

<style>
.isEmpty {
  color: var(--sub-color);
  position: absolute;
  bottom: 1px;
  font-size: 11px;
  font-style: italic;
}
.isValid {
  border-color: var(--sub-color);
}
</style>