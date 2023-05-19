<template>
  <div id="main">
    <THeader></THeader>
    <div id="cart">
      <h2>Đơn hàng</h2>
      <div class="cart__note mb-3">
        <i class="fa-solid fa-circle-exclamation m-r-8"></i>
        <span
          >Nine store thông báo: quý khách vui lòng kiểm tra thông tin đầy đủ
          trước khi đặt hàng</span
        >
      </div>
      <div
        class="d-flex justify-content-between p-2 mb-3"
        style="width: 400px; background-color: #fff; border-radius: 8px"
      >
        <router-link
          :class="{ 'order-active': $route.path == `/Order/0` }"
          to="/Order/0"
          >Chờ xác nhận</router-link
        >
        <router-link
          :class="{ 'order-active': $route.path == `/Order/1` }"
          to="/Order/1"
          >Đang giao hàng</router-link
        >
        <router-link
          :class="{ 'order-active': $route.path == `/Order/2` }"
          to="/Order/2"
          >Đã giao</router-link
        >
        <router-link
          :class="{ 'order-active': $route.path == `/Order/3` }"
          to="/Order/3"
          >Đã hủy</router-link
        >
        <router-link
          :class="{ 'order-active': $route.path == `/Order/4` }"
          to="/Order/4"
          >Tất cả</router-link
        >
      </div>
      <div style="overflow-y: auto; max-height: 600px;">
        <table class="cart__table" style="padding: 0; margin: 0">
          <tbody style="padding: 20px;">
            <tr >
              <th style="width: 180px; text-align: center">STT đơn hàng</th>
              <th style="width: 240px; text-align: center">Ngày đặt</th>
              <th style="width: 380px; text-align: center">Tình trạng</th>
              <th style="width: 380px; text-align: center">Xem chi tiết</th>
              <th style="width: 280px"></th>
            </tr>
            <tr v-for="(item, index) in dataOrders" :key="index">
              <td class="d-flex align-items-center justify-content-center">
                {{ index + 1 }}
              </td>
              <td>{{ formatDate(item.CreatedDate) }}</td>
              <td style="text-align: center">
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
                  class="btn btn-sm btn-primary"
                  @click="onShowPopupOrderDetail(item)"
                >
                  Xem chi tiết
                </button>
              </td>
              <td>
                <button
                  :class="[item.Status == 0 ? '' : 'disabled']"
                  type="button"
                  class="btn btn-outline-danger btn-sm"
                  @click="onShowPopupReason(item)"
                >
                  <i class="bi bi-trash"></i>
                  Hủy đặt hàng
                </button>
              </td>
            </tr>
            <tr v-if="isShowNoData">
              <td></td>
              <td></td>
              <td class="order-nodata">Bạn chưa có đơn hàng.</td>
              <td></td>
              <td></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <TPopup
      popupTile="Lý do hủy đơn"
      v-if="isShowPopupReason"
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
        <button
          type="button"
          class="btn btn-secondary"
          @click="hidePopupReason"
        >
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
    </TPopup>
    <TPopup
      v-if="isShowPopupOrderDetail"
      @handleClick="handleClickPopup"
      @hidePopup="hidePopup"
    >
      <template #header>
        <h4>Chi tiết đơn hàng</h4>
        <button
          type="button"
          class="btn-close"
          @click="hidePopupOrderDetail"
        ></button>
      </template>
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
        <button
          type="button"
          class="btn btn-secondary"
          @click="hidePopupOrderDetail"
        >
          Đóng
        </button>
      </template></TPopup
    >
    <TFooter></TFooter>
  </div>
</template>
    
    <script>
import { useRoute } from "vue-router";
import { computed, ref, reactive, watch } from "vue";
import { useStore } from "vuex";
import moment from "moment";
import _ from "lodash";
import router from "@/router/router";
import { notification } from "ant-design-vue";

import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TInput from "@/components/TInput.vue";
import AXIOS_BILL from "@/api/bill";
import TPopup from "@/components/TPopup.vue";
import AXIOS_ORDER from "@/api/order";
import { FILTER_USERID } from "@/js/constrant";
import { validateData } from "@/js/validateion";

import { Enum } from "@/js/base/enum";
export default {
  name: "TCart",
  components: {
    THeader,
    TFooter,
    TPopup,
    TInput,
  },
  setup() {
    const route = useRoute();
    const store = useStore();
    const dataSources = ref();
    const dataOrders = ref();
    const isShowPopupReason = ref(false);
    const isShowPopupOrderDetail = ref(false);
    const billDatas = ref([]);
    const orderSelected = ref([]);
    const filterByUserId = reactive(_.cloneDeep(FILTER_USERID));
    const reasonContent = ref();
    const validateForm = ref(null);
    const isShowNoData = ref(false);
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
    /**
     * Ẩn lý do hủy
     */
    const hidePopupReason = () => {
      isShowPopupReason.value = false;
    };
    /**
     * Bỏ sản phẩm
     */
    const removeProduct = (value) => {
      if (typeof value.NumProduct == "string") {
        value.NumProduct = parseInt(value.NumProduct);
      }
      if (value.NumProduct > 1) {
        value.NumProduct -= 1;
      }
    };
    /**
     * Thêm sản phẩm
     */
    const addProduct = (value) => {
      if (typeof value.NumProduct == "string") {
        value.NumProduct = parseInt(value.NumProduct);
      }
      value.NumProduct += 1;
    };
    /**
     * Lấy đơn hàng thông qua id người dùng
     * @param {Id người dùng} param
     */
    const getOrderServiceByUserId = async (param) => {
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
     * Lấy thông tin từ bảng order
     */
    const getAllOrderServicebyUserId = async (param) => {
      try {
        let response = await AXIOS_ORDER.getAllOrderServicebyUserId(param);
        if (response) {
          dataOrders.value = response.data.Data;
          if (response.data.ErrorCode == Enum.NODATA) {
            isShowNoData.value = true;
          } else {
            isShowNoData.value = false;
          }
        }
      } catch (err) {
        console.log(err);
      }
    };
    filterByUserId.userId = store.state.userInfo.UserId;
    filterByUserId.keyWord = route.params.id;
    getAllOrderServicebyUserId(filterByUserId);
    /**
     * Theo dõi id order thay đổi gọi lại lấy dữ liệu
     */
    watch(
      () => route.params.id,
      (newValue) => {
        if (newValue == 4) {
          filterByUserId.keyWord = "";
        } else {
          filterByUserId.keyWord = newValue;
        }
        getAllOrderServicebyUserId(filterByUserId);
      }
    );

    /**
     * Lấy hóa đơn thông qua id đơn hàng
     * @param {Id đơn hàng} param
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
     * Hiện popup
     */
    const onShowPopupReason = (item) => {
      isShowPopupReason.value = true;
      orderSelected.value = item;
    };

    /**
     * Ẩn popup
     */
    const hidePopup = () => {
      isShowPopup.value = false;
    };
    /**
     * Ẩn popup
     */
    const hidePopupOrderDetail = () => {
      isShowPopupOrderDetail.value = false;
    };

    /**
     * Xóa sản phẩm giỏ hàng
     * @param {Cart id} param
     */
    const updateOrderById = async (id, param) => {
      try {
        let response = await AXIOS_ORDER.updateOrder(id, param);
        if (response) {
          getOrderByUserId(store.state.userInfo.UserId);
          hidePopup();
        }
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Khi ấn nút xóa
     */
    const handleClickPopup = () => {
      orderSelected.value.Status = 2;
      updateOrderById(orderSelected.value.OrderId, orderSelected.value);
    };
    /**
     * Hiện popup chi tiết đơn hàng
     * @param {*} value
     */
    const onShowPopupOrderDetail = (value) => {
      isShowPopupOrderDetail.value = true;
      getOrderServiceByUserId(value.OrderId);
      getBillByOrderId(value.OrderId);
    };
    /**
     * Định dạng trạng thái đơn hàng
     * @param {*} value
     */
    const formatStatus = (value) => {
      if (value == Enum.STATUS_ORDER.PENDDING) {
        return "Chờ xác nhận";
      } else if (value == Enum.STATUS_ORDER.SHIPPING) {
        return "Đang giao hàng";
      } else if (value == Enum.STATUS_ORDER.SUCCESS) {
        return "Giao hàng thành công";
      } else {
        return "Đã hủy";
      }
    };
    /**
     * Định dạng tiền tệ VND
     */
    const formatter = new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    });
    /**
     * Định dạng ngày tháng
     * @param {*} date
     */
    const formatDate = (date) => {
      return moment(date).format("DD/MM/YYYY HH:mm");
    };
    return {
      isShowNoData,
      validateForm,
      reasonContent,
      onConfirmReasonCancel,
      onShowPopupReason,
      hidePopupReason,
      isShowPopupReason,
      getAllOrderServicebyUserId,
      filterByUserId,
      billDatas,
      getBillByOrderId,
      formatDate,
      formatStatus,
      dataOrders,
      onShowPopupOrderDetail,
      isShowPopupOrderDetail,
      orderSelected,
      handleClickPopup,
      updateOrderById,
      hidePopup,
      hidePopupOrderDetail,
      onShowPopupReason,
      store,
      route,
      dataSources,
      removeProduct,
      addProduct,
      formatter,
      getOrderServiceByUserId,
      sumPrice,
    };
  },
};
</script>
<style>
.status-type {
  max-width: max-content;
  padding: 6px;
  border-radius: 4px;
  min-width: 8 0px;
}
.approved-status {
  border: 1px solid #11aa7a;
  color: #11aa7a;
  background-color: #b9f8e4;
}
.pending-status {
  border: 1px solid #6153df;
  color: #6153df;
  background-color: #ebe9fb;
}
.refuse-status {
  border: 1px solid #ef292f;
  color: #ef292f;
  background-color: #fee8e7;
}
.shipping-status {
  border: 1px solid rgba(78, 148, 228, 0.8);
  color: #1f7de8;
  background-color: rgb(31, 125, 232, 0.2);
}
.order-active {
  color: var(--primary-color) !important;
}
.order-nodata {
  font-size: 18px;
  font-style: italic;
  opacity: 0.6;
}
</style>