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
        if (props.rules[1]) {
          if (props.rules[1] == "Email") {
            const regexEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (!val.trim().match(regexEmail)) {
              isValidate.value = true;
              messageValidate.value = props.name + " không đúng định dạng";
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
  bottom: 3px;
  font-size: 11px;
  font-style: italic;
}
.isValid {
  border-color: var(--sub-color);
}
</style>