<template>
  <div class="table-list">
    <div class="d-flex justify-between">
      <TOrderReport
        :title="'Tổng số đơn hàng tháng này'"
        :content="orderAll.length"
      ></TOrderReport>
      <TOrderReport
        :title="'Tổng số đơn hàng chờ xác nhận'"
        :content="orderPending"
      ></TOrderReport>
      <TOrderReport
        :title="'Tổng số đơn hàng đang giao'"
        :content="orderShipping"
      ></TOrderReport>
      <TOrderReport
        :title="'Tổng số đơn hàng đã hủy'"
        :content="orderRejected"
      ></TOrderReport>
    </div>
    <div>
      <h5 class="mt-3">Thống kê số đơn hàng đã hoàn thành theo tháng</h5>
      <TLine v-if="label.length" :label="label" :data="data"></TLine>
    </div>
  </div>
</template>

<script>
import TSidebar from "@/layout/TSidebar.vue";
import THeaderAdmin from "./THeaderAdmin.vue";
import TPopupVue from "@/components/TPopup.vue";
import TOrderReport from "@/components/TOrderReport.vue";
import TLine from "@/components/TLine.vue";
import AXIOS_ORDER from "@/api/order";
import { FILTER_OPTION } from "@/js/constrant";
import { ref, reactive } from "vue";
export default {
  components: {
    TSidebar,
    THeaderAdmin,
    TPopupVue,
    TOrderReport,
    TLine,
  },
  setup() {
    const filterOption = reactive(_.cloneDeep(FILTER_OPTION));
    const orderAll = ref([]);
    const orderPending = ref(0);
    const orderShipping = ref(0);
    const orderRejected = ref(0);
    const orderSuccess = ref([]);
    const takenValues = {};
    const label = ref([]);
    const data = ref([]);
    const getAllOrderByFilter = async (params) => {
      try {
        let response = await AXIOS_ORDER.getAllOrderServicebyFilter(params);
        if (response) {
          orderAll.value = response.data.Data;
          for (var i = 0; i < orderAll.value.length; i++) {
            if (orderAll.value[i].Status == 0) {
              orderPending.value += 1;
            }
            if (orderAll.value[i].Status == 1) {
              orderShipping.value += 1;
            }
            if (orderAll.value[i].Status == 2) {
              orderSuccess.value.push(orderAll.value[i]);
            }
            if (orderAll.value[i].Status == 3) {
              orderRejected.value += 1;
            }
          }
          for (var i = 0; i < orderSuccess.value.length; i++) {
            console.log(orderSuccess.value[i].CreatedDate);
            const date = new Date(orderSuccess.value[i].CreatedDate);
            const value = date.getMonth() + 1;
            if (!takenValues[value]) {
              takenValues[value] = true;
              label.value.push(value);
            }
          }
          label.value.sort((a, b) => a - b);

          for (var i = 0; i < label.value.length; i++) {
            let num = 0;
            for (var j = 0; j < orderSuccess.value.length; j++) {
              const date = new Date(orderSuccess.value[j].CreatedDate);
              const value = date.getMonth() + 1;
              if (value == label.value[i]) {
                num += 1;
              }
            }
            data.value.push(num);
          }
        }
      } catch (error) {
        console.log(error);
      }
    };
    filterOption.pageSize = 999;
    getAllOrderByFilter(filterOption);

    return {
      takenValues,
      label,
      data,
      filterOption,
      orderAll,
      orderPending,
      orderShipping,
      orderSuccess,
      orderRejected,
      getAllOrderByFilter,
    };
  },
};
</script>

<style>
#admin__main {
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: space-between;
}
.admin__main-content-right {
  width: calc(100% - 240px);
}
</style>