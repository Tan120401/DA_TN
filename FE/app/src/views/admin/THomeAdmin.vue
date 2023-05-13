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
        :title="'Tổng số đơn hàng đã giao'"
        :content="orderSuccess.length"
      ></TOrderReport>
      <TOrderReport
        :title="'Tổng số đơn hàng đã hủy'"
        :content="orderRejected"
      ></TOrderReport>
    </div>
    <div>
      <h5 class="mt-3">Thống kê doanh thu theo tháng</h5>
      <TLine v-if="loading" :label="label" :data="data"></TLine>
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
import AXIOS_BILL from "@/api/bill";
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
    const label = ref([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]);
    const data = ref([]);
    const loading = ref(false);
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
          for (var i = 0; i < label.value.length; i++) {
            sumPrice = 0;
            for (var j = 0; j < orderSuccess.value.length; j++) {
              const date = new Date(orderSuccess.value[j].CreatedDate);
              const value = date.getMonth() + 1;
              if (value == label.value[i]) {
                await getBillByOrderId(orderSuccess.value[j].OrderId);
              }
            }
            data.value.push(sumPrice);
          }
          loading.value = true;
        }
      } catch (error) {
        console.log(error);
      }
    };
    var sumPrice = 0;
    const getBillByOrderId = async (param) => {
      try {
        let response = await AXIOS_BILL.getBillById(param);
        if (response) {
          sumPrice += response.data[0].SumPrice;
        }
      } catch (err) {
        console.log(err);
      }
    };
    filterOption.pageSize = 999;
    getAllOrderByFilter(filterOption);

    return {
      loading,
      getBillByOrderId,
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