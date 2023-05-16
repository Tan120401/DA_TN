<template>
  <div class="table-list">
    <div class="d-flex justify-content-between">
      <h4>Danh sách đơn hàng</h4>
    </div>
    <div class="input-group" style="width: 300px">
      <span class="input-group-text" id="basic-addon1">
        <i class="bi bi-search"></i>
      </span>
      <input
        type="text"
        class="form-control"
        placeholder="Tìm kiếm..."
        aria-label="Input group example"
        aria-describedby="basic-addon1"
        v-model="keywordSearch"
      />
    </div>
    <div class="table-record-list">
      <table class="table mt-3" style="vertical-align: middle">
        <thead>
          <tr>
            <th scope="col">Mã đơn hàng</th>
            <th scope="col">Người đặt hàng</th>
            <th scope="col">Ngày đặt</th>
            <th scope="col">Ngày sửa</th>
            <th scope="col" style="text-align: center">Trạng thái</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in orderData" :key="index">
            <th scope="row">{{ item.OrderId }}</th>
            <td>{{ item.FullName }}</td>
            <td>{{ formatDate(item.CreatedDate) }}</td>
            <td>{{ formatDate(item.ModifiedDate) }}</td>
            <td>
              <div
                :class="{
                  'pending-status': item.Status == 0,
                  'shipping-status': item.Status == 1,
                  'approved-status': item.Status == 2,
                  'refuse-status': item.Status == 3,
                }"
                class="status-type"
                style="margin: 0 auto"
              >
                {{ formatStatus(item.Status) }}
              </div>
            </td>
            <td>
              <button
                type="button"
                class="btn btn-primary"
                @click="onShowPopupOrderDetail(item)"
              >
                Xem chi tiết
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div v-if="!isLoading">
      <t-paging
        :totalPage="totalPage"
        @onChangPageNumber="onChangPageNumber"
        @onSelectPageNumber="onChangPageNumber"
      ></t-paging>
    </div>
  </div>
  <TPopupVue
    popupTile="Chi tiết đơn hàng"
    @hidePopup="hidePopup"
    v-if="isShowPopupOrderDetail"
  >
    <template #content>
      <td style="text-align: center">
        <div class="cart__item" v-for="item in dataSources" :key="item.id">
          <div class="cart__item-img">
            <img
              :src="require(`@/assets/img/product/${item.ImgProduct}`)"
              alt=""
            />
          </div>
          <div class="cart__item-info">
            <h4>{{ item.ProductName }}</h4>
            <div class="cart__item-sale">Giảm {{ item.DisCount }}%</div>
          </div>
          <div>Size: {{ item.Size }}</div>
          <div>Số lượng: {{ item.Quantity }}</div>
          <div>
            Đơn giá:
            <br />
            {{ formatter.format(item.Price * (1 - item.DisCount / 100)) }}
            <span class="product__price-discount"
              >{{ formatter.format(item.Price) }}
            </span>
          </div>
          <div>
            Thành tiền:
            <span>{{
              formatter.format(
                item.Price * (1 - item.DisCount / 100) * item.Quantity
              )
            }}</span>
          </div>
          <div style="width: 100px">{{ formatStatus(item.Status) }}</div>
          <span v-if="item.Status == 3" style="width: 130px"
            >Lý do hủy: {{ item.Reason }}
          </span>
        </div>
        <h5>
          Tổng tiền:
          {{ formatter.format(sumPrice) }}
        </h5>
      </td>
      <div>Họ tên người nhận hàng: {{ billDatas.UserName }}</div>
      <div>Địa chỉ giao hàng: {{ billDatas.Address }}</div>
      <div>Số điện thoại: {{ billDatas.PhoneNumber }}</div>
      <div>
        Đơn hàng
        <span style="font-style: oblique">{{
          billDatas.IsPay == 1 ? "đã thanh toán" : "chưa thanh toán"
        }}</span>
      </div>
    </template>
    <template #footer>
      <button type="button" class="btn btn-secondary" @click="hidePopup">
        Đóng
      </button>
      <button
        v-if="isPending"
        type="button"
        class="btn btn-danger"
        @click="onCancelOrder"
      >
        Hủy đơn
      </button>
      <button
        v-if="!isRejected"
        type="button"
        class="btn btn-primary"
        @click="onUpdateStatusOrder"
      >
        Xác nhận
      </button>
    </template>
  </TPopupVue>
  <TPopupVue
    popupTile="Lý do hủy đơn"
    v-if="isShowReason"
    @hidePopup="hidePopupReason"
  >
    <template #content>
      <div ref="validateForm">
        <t-input
          v-model="reasonContent"
          :rules="['Empty']"
          name="Lý do hủy đơn"
        ></t-input>
      </div>
    </template>
    <template #footer>
      <button type="button" class="btn btn-secondary" @click="hidePopupReason">
        Đóng
      </button>

      <button
        v-if="!isRejected"
        type="button"
        class="btn btn-primary"
        @click="onConfirmReasonCancel"
      >
        Xác nhận
      </button>
    </template>
  </TPopupVue>

  <TLoading v-if="isLoading"></TLoading>
  <div v-if="isShowNoData" class="text-nodata">
    Không có đơn hàng nào thỏa mãn.
  </div>
</template>
  
<script>
import { reactive, ref, computed, watch } from "vue";
import _ from "lodash";

import TPopupVue from "@/components/TPopup.vue";
import TInput from "@/components/TInput.vue";
import AXIOS_ORDER from "@/api/order";
import AXIOS_BILL from "@/api/bill";
import moment from "moment";
import TPaging from "@/components/TPaging.vue";
import TLoading from "@/components/TLoading.vue";
import { FILTER_OPTION } from "@/js/constrant";
import { notification } from "ant-design-vue/lib/components";
import { validateData } from "@/js/validateion";
export default {
  name: "TCustomerAdmin",
  components: {
    TPopupVue,
    TInput,
    TPaging,
    TLoading,
  },
  setup() {
    const validateForm = ref(null);
    const orderData = ref();
    const isShowPopupOrderDetail = ref(false);
    const isShowPopupDelete = ref(false);
    const isLoading = ref(false);
    const isShowNoData = ref(false);
    const totalPage = ref();
    const filterOption = reactive(_.cloneDeep(FILTER_OPTION));
    const dataSources = ref();

    const orderSelected = ref([]);
    const isRejected = ref(false);
    const isPending = ref(false);

    const billDatas = ref([]);
    const isShowReason = ref(false);
    const reasonContent = ref();

    /**
     * Thay đổi từ khóa tìm kiếm gọi lại phân trang
     */
    const keywordSearch = ref();
    watch(
      keywordSearch,
      _.debounce((newVal) => {
        filterOption.keyWord = newVal;
        filterOption.pageNumber = 1;
        getAllOrderByFilter(filterOption);
      }, 300)
    );

    /**
     * Lấy hóa đơn thông qua order id
     */
    const getBillByOrderId = async (param) => {
      try {
        let response = await AXIOS_BILL.getBillById(param);
        if (response) {
          billDatas.value = response.data[0];
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Xác nhận hủy đơn
     */
    const onConfirmReasonCancel = async () => {
      var lstInput = validateForm.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        try {
          let response = await AXIOS_ORDER.updateOrder(
            orderSelected.value.OrderId,
            { Status: 3, Reason: reasonContent.value }
          );
          if (response) {
            notification.success({ message: "Xác nhận thành công" });
            hidePopupReason();
            getAllOrderByFilter(filterOption);
          }
        } catch (error) {
          console.log(error);
        }
      }
    };
    const onCancelOrder = () => {
      isShowReason.value = true;
      // updateStatusOrder(orderSelected.value.OrderId, 3);
    };
    const onUpdateStatusOrder = () => {
      if (orderSelected.value.Status == 0) {
        updateStatusOrder(orderSelected.value.OrderId, 1);
      } else if (orderSelected.value.Status == 1) {
        updateStatusOrder(orderSelected.value.OrderId, 2);
      } else {
        hidePopup();
      }
    };

    /**
     * Cập nhật trạng thái đơn hàng
     */
    const updateStatusOrder = async (id, status) => {
      try {
        let response = await AXIOS_ORDER.updateOrder(id, { Status: status });
        if (response) {
          notification.success({ message: "Xác nhận thành công" });
          hidePopup();
          getAllOrderByFilter(filterOption);
        }
      } catch (error) {
        console.log(err);
      }
    };
    /**
     * Lấy thông tin đơn hàng thông qua id
     */
    const getOrderServiceByOrderId = async (param) => {
      try {
        let response = await AXIOS_ORDER.getOrderServiceByOrderId(param);
        if (response) {
          dataSources.value = response.data;
        }
      } catch (err) {
        console.log(err);
      }
    };

    /**
     * Tính tổng tiền
     */
    const sumPrice = computed(() => {
      var sum = 0;
      for (let key in dataSources.value) {
        sum +=
          dataSources.value[key].Quantity *
          dataSources.value[key].Price *
          (1 - dataSources.value[key].DisCount / 100);
      }
      return sum;
    });
    /**
     * Lấy danh sách người đơn theo phân trang
     */
    const getAllOrderByFilter = async (params) => {
      try {
        let response = await AXIOS_ORDER.getAllOrderServicebyFilter(params);
        isLoading.value = true;
        if (response) {
          orderData.value = response.data.Data;
          totalPage.value = response.data.TotalPage;
          setTimeout(() => {
            isLoading.value = false;
          }, 500);
          if (totalPage.value < 1) {
            isShowNoData.value = true;
          } else {
            isShowNoData.value = false;
          }
        }
      } catch (err) {
        console.log(err);
      }
    };
    getAllOrderByFilter(filterOption);
    /**
     * Ẩn popup
     */
    const hidePopup = () => {
      if (isShowPopupOrderDetail.value) {
        isShowPopupOrderDetail.value = false;
      }
      if (isShowPopupDelete.value) {
        isShowPopupDelete.value = false;
      }
    };

    const hidePopupReason = () => {
      isShowReason.value = false;
    };
    /**
     * Hiện popup chi tiết đơn
     */
    const onShowPopupOrderDetail = (value) => {
      getBillByOrderId(value.OrderId);
      isShowPopupOrderDetail.value = true;
      orderSelected.value = value;
      if (value.Status == 3) {
        isRejected.value = true;
      } else {
        isRejected.value = false;
      }
      if (value.Status == 0) {
        isPending.value = true;
      } else {
        isPending.value = false;
      }
      getOrderServiceByOrderId(value.OrderId);
    };
    /**
     * Hiện popup xóa đơn
     */
    const onShowPopupDelete = (value) => {
      isShowPopupDelete.value = true;
      userIdSelected.value = value.UserId;
    };
    const formatDate = (date) => {
      return moment(date).format("DD/MM/YYYY HH:mm");
    };
    const formatStatus = (value) => {
      if (value == 0) {
        return "Đang xử lý";
      } else if (value == 1) {
        return "Đang giao hàng";
      } else if (value == 2) {
        return "Giao hàng thành công";
      } else {
        return "Đã hủy";
      }
    };
    const onChangPageNumber = (item) => {
      filterOption.pageNumber = item;
      getAllOrderByFilter(filterOption);
    };
    /**
     * Định dạng tiền tệ VND
     */
    const formatter = new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    });
    return {
      keywordSearch,
      billDatas,
      getBillByOrderId,
      onConfirmReasonCancel,
      validateForm,
      reasonContent,
      isShowReason,
      onCancelOrder,
      isPending,
      isRejected,
      orderSelected,
      formatter,
      sumPrice,
      dataSources,
      onChangPageNumber,
      isLoading,
      isShowNoData,
      totalPage,
      filterOption,
      formatStatus,
      formatDate,
      onShowPopupDelete,
      orderData,
      getAllOrderByFilter,
      isShowPopupOrderDetail,
      hidePopup,
      hidePopupReason,
      onShowPopupOrderDetail,
      updateStatusOrder,
      onUpdateStatusOrder,
      isShowPopupDelete,
    };
  },
};
</script>
  
<style>
</style>