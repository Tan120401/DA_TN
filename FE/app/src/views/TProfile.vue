<template>
  <div id="main">
    <THeader></THeader>
    <section id="user__profile" style="background-color: #f4f5f7">
      <div class="container py-5 h-100 m-t-60">
        <div class="row d-flex justify-content-center align-items-center h-100">
          <div class="col col-lg-6 mb-4 mb-lg-0">
            <div class="card mb-3" style="border-radius: 0.5rem">
              <div class="row g-0">
                <div
                  class="col-md-4 gradient-custom text-center text-white d-flex flex-column align-items-center"
                  style="
                    border-top-left-radius: 0.5rem;
                    border-bottom-left-radius: 0.5rem;
                  "
                >
                  <img
                    v-bind:src="imgUser"
                    alt="Avatar"
                    class="img-fluid my-5"
                    style="width: 80px"
                  />
                  <button
                    class="btn btn-secondary btn-sm"
                    @click="onShowFormEdit"
                  >
                    Chỉnh sửa
                  </button>
                </div>
                <div class="col-md-8">
                  <div class="card-body p-4">
                    <h6>Thông tin cá nhân</h6>
                    <hr class="mt-0 mb-4" />
                    <div class="row pt-1">
                      <div class="col-6 mb-3">
                        <h6>Email</h6>
                        <p class="text-muted">{{ userInfo.Email }}</p>
                      </div>
                      <div class="col-6 mb-3">
                        <h6>Họ và tên</h6>
                        <p class="text-muted">{{ userInfo.FullName }}</p>
                      </div>
                      <div class="col-6 mb-3">
                        <h6>Tên đăng nhập</h6>
                        <p class="text-muted">{{ userInfo.UserName }}</p>
                      </div>
                      <div class="col-6 mb-3">
                        <h6>Mật khẩu</h6>
                        <p class="text-muted">{{ userInfo.PassWord }}</p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <div class="modal1" v-if="isShowFormEdit">
      <form id="form-control" @submit.prevent="onUpdateProfile">
        <div class="form__header">
          <h2>Thông tin cá nhân</h2>
        </div>
        <i class="fa-solid fa-xmark t-icon-close" @click="onHideFormEdit"></i>
        <input type="file" @change="onChoseFile" />
        <TInput
          type="text"
          placeholder="Email"
          name="Email"
          :rules="['Empty']"
          v-model="userInfor.Email"
        ></TInput>
        <TInput
          type="text"
          placeholder="Họ và Tên"
          name="Họ và Tên"
          :rules="['Empty']"
          v-model="userInfor.FullName"
        ></TInput>
        <TInput
          type="text"
          placeholder="Tên đăng nhập"
          name="Tên đăng nhập"
          :rules="['Empty']"
          v-model="userInfor.UserName"
        ></TInput>
        <TInput
          type="text"
          placeholder="Mật khẩu"
          name="Mật khẩu"
          :rules="['Empty']"
          v-model="userInfor.PassWord"
        ></TInput>
        <button type="submit" class="tbutton btn-login">Sửa</button>
      </form>
    </div>
    <TFooter></TFooter>
  </div>
</template>

<script>
import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TInput from "@/components/TInput.vue";
import USER_AXIOS from "@/api/user";
import { FILE_OPTIONS } from "@/js/data";
import _ from "lodash";
import { reactive, ref } from "vue";
import { useRoute } from "vue-router";
export default {
  name: "TProfile",
  components: {
    THeader,
    TFooter,
    TInput,
  },
  setup() {
    const userInfo = ref([]);
    const userInfor = ref();
    const route = useRoute();
    const imgUser = ref("");
    const isShowFormEdit = ref(false);
    const fileChange = reactive(_.cloneDeep(FILE_OPTIONS));

    /**
     * Hàm lấy dữ liệu user
     * Created by NVTAN(10/04/2023)
     */
    const getUserInfor = async (param) => {
      try {
        let response = await USER_AXIOS.getUserById(param);
        if (response) {
          userInfo.value = response.data[0];
          imgUser.value = true
            ? require("../assets/img/user/" + response.data[0].ImgName)
            : require("../assets/img/user/avatar-null.jpeg");
        }
      } catch (err) {
        console.log(err);
      }
    };
    getUserInfor(route.params.id);

    /**
     * Hàm hiển thị form sửa
     * Created by NVTAN(10/04/2023)
     */
    const onShowFormEdit = () => {
      isShowFormEdit.value = true;
      userInfor.value = _.cloneDeep(userInfo.value);
    };
    /**
     * Hàm ẩn form sửa
     * Created by NVTAN(10/04/2023)
     */
    const onHideFormEdit = () => {
      isShowFormEdit.value = false;
    };

    /**
     * Hàm sửa thông tin
     * Created by NVTAN(10/04/2023)
     */
    const onUpdateProfile = async () => {
      try {
        if (fileChange.FileName != null) {
          userInfor.value.ImgName = fileChange.FileName;
        }
        let response = await USER_AXIOS.updateUser(
          route.params.id,
          userInfor.value
        );
        if (response) {
          if (fileChange.FileName != null && fileChange.File != null) {
            console.log(fileChange)
            await uploadFile(fileChange);
          }
          getUserInfor(route.params.id);
          onHideFormEdit();
        }
      } catch (err) {
        console.log(err);
      }
    };

    /**
     * Hàm bắt sự kiện khi chọn file
     * Created by NVTAN(10/04/2023)
     */
    const onChoseFile = (event) => {
      fileChange.FileName = event.target.files[0].name;
      fileChange.File = event.target.files[0];
    };
    /**
     * Hàm thêm file vào database
     * Created by NVTAN(10/04/2023)
     */
    const uploadFile = async (params) => {
      try {
        let response = await USER_AXIOS.uploadFile(params);
        console.log(response);
      } catch (err) {
        console.log(err);
      }
    };

    return {
      onChoseFile,
      route,
      userInfo,
      userInfor,
      isShowFormEdit,
      imgUser,
      fileChange,
      getUserInfor,
      onShowFormEdit,
      onHideFormEdit,
      onUpdateProfile,
      uploadFile,
    };
  },
};
</script>

<style>
#user__profile {
  height: calc(100vh - 60px - 232px);
}
#form-control {
  min-width: 400px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: var(--bg1);
  border-radius: 8px;
  padding: 20px;
}
.form__header {
  display: flex;
  align-items: center;
  justify-content: center;
}
.t-icon-close {
  position: absolute;
  right: 20px;
  top: 10px;
}
</style>