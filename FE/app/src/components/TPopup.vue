<template>
  <!-- Modal -->
  <div
    class="modal fade"
    :id="popupId"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">{{ popupTile }}</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">{{ popupContent }}</div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
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
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { defineEmits, defineComponent } from "vue";
export default defineComponent({
  name: "TPopup",
  props: ["popupId", "popupTile", "popupContent"],
  emits: ["handleClick"],
  setup({ emit }) {
    return {};
  },
});
</script>

<style>
</style>