<template>
  <!-- Modal -->

  <div class="modal__custom">
    <div class="popup__container">
      <div class="popup__header d-flex justify-content-between">
        <slot name="header">
          <h4>{{ popupTile }}</h4>
          <button
            type="button"
            class="btn-close"
            @click="$emit('hidePopup')"
          ></button>
        </slot>
      </div>
      <div class="popup__content">
        <slot name="content">
          <h5>{{ popupContent }}</h5>
        </slot>
      </div>
      <div class="popup__footer d-flex justify-content-end">
        <slot name="footer">
          <button
            type="button"
            class="btn btn-secondary"
            @click="$emit('hidePopup')"
          >
            Đóng
          </button>
          <button
            type="button"
            class="btn btn-danger"
            @click="$emit('handleClick')"
          >
            Xóa
          </button>
        </slot>
      </div>
    </div>
  </div>
</template>
<script>
import { ref, defineComponent } from "vue";
export default defineComponent({
  name: "TPopup",
  props: ["popupId", "popupTile", "popupContent"],
  emits: ["handleClick", "hidePopup"],
  setup({ emit }) {
    return {};
  },
});
</script>

<style >
.modal__custom {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(79, 76, 76, 0.4);
  z-index: 999;
}
.popup__container {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  min-width: 520px;
  min-height: 220px;
  background-color: #fff;
  border-radius: 8px;
  padding: 12px 20px;
}
.popup__header {
  height: 48px;
}
.popup__content {
  min-height: 80px;
}
.popup__footer {
  margin-top: 20px;
  column-gap: 8px;
  height: 40px;
}
</style>