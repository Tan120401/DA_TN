<template>
  <div class="table-list">
    <div class="d-flex justify-content-between">
      <h4>Danh sách người dùng</h4>
      <button
        type="button"
        class="btn btn-sm btn-primary"
        @click="onShowPopupAdd"
      >
        Thêm người dùng
      </button>
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
            <th scope="col">STT</th>
            <th scope="col">Họ và tên</th>
            <th scope="col">Email</th>
            <th scope="col">Mật khẩu</th>
            <th scope="col">Role</th>
            <th scope="col">Chức năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in userData" :key="index">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ item.FullName }}</td>
            <td>{{ item.UserName }}</td>
            <td>{{ item.PassWord }}</td>
            <td>{{ item.Role }}</td>
            <td>
              <button
                type="button"
                class="btn btn-primary me-2"
                @click="onShowPopupUpdate(item)"
              >
                Sửa
              </button>
              <button
                type="button"
                class="btn btn-danger"
                @click="onShowPopupDelete(item)"
              >
                Xóa
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
    popupTile="Thêm người dùng"
    @hidePopup="hidePopup"
    v-if="isShowPopupAdd"
  >
    <template #content>
      <div ref="validateForm">
        <div class="row">
          <div class="col-md-12">
            <label for="inputEmail4" class="form-label">Họ và tên</label>
            <TInput
              type="text"
              placeholder="Họ và tên"
              name="Họ và tên"
              v-model="userOptions.FullName"
              :rules="['Empty']"
            ></TInput>
          </div>
          <div class="col-md-12">
            <label for="inputEmail4" class="form-label">Email</label>
            <TInput
              type="text"
              placeholder="Email"
              name="Email"
              v-model="userOptions.UserName"
              :rules="['Email']"
            ></TInput>
            <div style="position: relative">
              <span class="register-fail" v-if="isShowDuplicateUser"
                >Tên đăng nhập đã tồn tại</span
              >
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12">
            <label for="inputEmail4" class="form-label">Mật khẩu</label>
            <TInput
              type="password"
              placeholder="Mật khấu"
              name="Mật khấu"
              v-model="userOptions.PassWord"
              :rules="['Password']"
            ></TInput>
          </div>
          <div class="col-md-4">
            <label class="form-label">Role</label>
            <select
              id="inputState"
              class="form-select"
              v-model="userOptions.Role"
            >
              <option selected>user</option>
              <option>admin</option>
              <option>master admin</option>
            </select>
          </div>
        </div>
      </div>
    </template>
    <template #footer>
      <button type="button" class="btn btn-secondary" @click="hidePopup">
        Đóng
      </button>
      <button type="button" class="btn btn-primary" @click="onAddCustomer">
        Thêm
      </button>
    </template>
  </TPopupVue>
  <TPopupVue
    popupTile="Sửa thông tin người dùng"
    @hidePopup="hidePopup"
    v-if="isShowPopupUpdate"
  >
    <template #content>
      <div ref="validateForm">
        <div class="row">
          <div class="col-md-12">
            <label for="inputEmail4" class="form-label">Họ và tên</label>
            <TInput
              type="text"
              placeholder="Họ và tên"
              name="Họ và tên"
              v-model="userEditData.FullName"
              :rules="['Empty']"
            ></TInput>
          </div>
          <div class="col-md-12">
            <label for="inputEmail4" class="form-label">Email</label>
            <TInput
              type="text"
              placeholder="Email"
              name="Email"
              v-model="userEditData.UserName"
              :rules="['Email']"
            ></TInput>
            <div style="position: relative">
              <span class="register-fail" v-if="isShowDuplicateUser"
                >Tên đăng nhập đã tồn tại</span
              >
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12">
            <label for="inputEmail4" class="form-label">Mật khẩu</label>
            <TInput
              type="password"
              placeholder="Mật khấu"
              name="Mật khấu"
              v-model="userEditData.PassWord"
              :rules="['Password']"
            ></TInput>
          </div>
          <div class="col-md-6">
            <label class="form-label">Role</label>
            <select
              id="inputState"
              class="form-select"
              v-model="userEditData.Role"
            >
              <option selected>user</option>
              <option>admin</option>
              <option>master admin</option>
            </select>
          </div>
        </div>
      </div>
    </template>
    <template #footer>
      <button type="button" class="btn btn-secondary" @click="hidePopup">
        Đóng
      </button>
      <button type="button" class="btn btn-primary" @click="onUpdateUser">
        Sửa
      </button>
    </template>
  </TPopupVue>
  <TPopupVue
    popupTile="Xóa người dùng"
    popupContent="Bạn có chắc chắn muốn xóa người dùng này không?"
    v-if="isShowPopupDelete"
    @handleClick="deleteUserById"
    @hidePopup="hidePopup"
  ></TPopupVue>
  <TLoading v-if="isLoading"></TLoading>
  <div v-if="isShowNoData" class="text-nodata">
    Không có người dùng nào thỏa mãn.
  </div>
</template>
  
<script>
import { reactive, ref, watch } from "vue";
import _ from "lodash";

import TPopupVue from "@/components/TPopup.vue";
import TInput from "@/components/TInput.vue";

import AXIOS_USER from "@/api/user";
import { USER_REGISTER } from "@/js/data";
import TLoading from "@/components/TLoading.vue";

import { validateData } from "@/js/validateion";

import { FILTER_OPTION } from "@/js/constrant";
import TPaging from "@/components/TPaging.vue";
import { notification } from "ant-design-vue";
export default {
  name: "TCustomerAdmin",
  components: {
    TPopupVue,
    TInput,
    TPaging,
    TLoading,
  },
  setup() {
    const isShowDuplicateUser = ref(false);
    const userData = ref();
    const isShowPopupAdd = ref(false);
    const userOptions = ref([]);
    const isShowPopupDelete = ref(false);
    const isShowPopupUpdate = ref(false);
    const userIdSelected = ref();
    const validateForm = ref();
    const userEditData = ref([]);
    const totalPage = ref();
    const isLoading = ref(true);
    const isShowNoData = ref(false);
    const filterOption = reactive(_.cloneDeep(FILTER_OPTION));

    /**
     * Thay đổi từ khóa tìm kiếm gọi lại phân trang
     */
    const keywordSearch = ref();
    watch(
      keywordSearch,
      _.debounce((newVal) => {
        filterOption.keyWord = newVal;
        filterOption.pageNumber = 1;
        resetPaging();
      }, 300)
    );

    const onChangPageNumber = (item) => {
      filterOption.pageNumber = item;
      getAllUser(filterOption);
    };

    /**
     * Hiện poup sửa
     */
    const onShowPopupUpdate = (value) => {
      isShowDuplicateUser.value = false;
      isShowPopupUpdate.value = true;
      getUserById(value.UserId);
    };
    /**
     * Lấy thông tin người dùng thông qua id
     */
    const getUserById = async (param) => {
      try {
        let response = await AXIOS_USER.getUserById(param);
        if (response) {
          userEditData.value = response.data[0];
        }
      } catch (error) {
        console.log(err);
      }
    };
    /**
     * Gọi api sửa thông tin người dùng
     */
    const updateToUser = async (id, params) => {
      try {
        let response = await AXIOS_USER.updateUser(id, params);
        if (response) {
          hidePopup();
          resetPaging();
          notification.success({ message: "Sửa thành công" });
        }
      } catch (err) {
        console.log(err);
        if (err.response.data.ErrorCode == 4) {
          isShowDuplicateUser.value = true;
        } else {
          isShowDuplicateUser.value = false;
        }
      }
    };
    /**
     * validate dữ liệu và thêm người dùng
     */
    const onUpdateUser = () => {
      var lstInput = validateForm.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        updateToUser(userEditData.value.UserId, userEditData.value);
      }
    };

    /**
     * Ấn thêm
     */
    const onAddCustomer = () => {
      var lstInput = validateForm.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        insertToUser(userOptions.value);
      }
    };
    /**
     * Hàm thêm tài khoản
     */
    const insertToUser = async (params) => {
      try {
        let response = await AXIOS_USER.setRegister(params);
        if (response) {
          hidePopup();
          resetPaging();
          notification.success({ message: "Thêm thành công" });
        }
      } catch (err) {
        console.log(err);
        if (err.response.data.ErrorCode == 4) {
          isShowDuplicateUser.value = true;
        } else {
          isShowDuplicateUser.value = false;
        }
      }
    };
    /**
     * Lấy danh sách người dùng
     */
    const getAllUser = async (params) => {
      try {
        let response = await AXIOS_USER.getUserByFilter(params);
        isLoading.value = true;
        if (response) {
          totalPage.value = response.data.TotalPage;
          userData.value = response.data.Data;
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
    getAllUser(filterOption);
    const resetPaging = () => {
      filterOption.pageNumber = 1;
      getAllUser(filterOption);
    };
    /**
     * Ẩn popup thêm
     */
    const hidePopup = () => {
      if (isShowPopupAdd.value) {
        isShowPopupAdd.value = false;
      }
      if (isShowPopupDelete.value) {
        isShowPopupDelete.value = false;
      }
      if (isShowPopupUpdate.value) {
        isShowPopupUpdate.value = false;
      }
    };
    /**
     * Hiện popup
     */
    const onShowPopupAdd = () => {
      isShowDuplicateUser.value = false;
      isShowPopupAdd.value = true;
      userOptions.value = _.cloneDeep(USER_REGISTER);
    };

    /**
     * Hiện popup xóa
     */
    const onShowPopupDelete = (value) => {
      isShowPopupDelete.value = true;
      userIdSelected.value = value.UserId;
    };
    /**
     * Xóa người dùng
     */
    const deleteUserById = async () => {
      try {
        let response = await AXIOS_USER.deleteUserById(userIdSelected.value);
        if (response) {
          hidePopup();
          resetPaging();
          notification.error({ message: "Xóa thành công" });
        }
      } catch (err) {
        console.log(err);
      }
    };

    return {
      isShowNoData,
      keywordSearch,
      isShowDuplicateUser,
      resetPaging,
      onChangPageNumber,
      isLoading,
      filterOption,
      updateToUser,
      onUpdateUser,
      onShowPopupUpdate,
      userEditData,
      getUserById,
      validateForm,
      deleteUserById,
      onShowPopupDelete,
      userData,
      userOptions,
      getAllUser,
      isShowPopupAdd,
      hidePopup,
      onShowPopupAdd,
      onAddCustomer,
      insertToUser,
      isShowPopupDelete,
      isShowPopupUpdate,
      userIdSelected,
      totalPage,
    };
  },
};
</script>
  
  <style>
</style>