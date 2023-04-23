<template>
  <div id="admin__main">
    <TSidebar></TSidebar>
    <div class="admin__main-content-right">
      <THeaderAdmin></THeaderAdmin>
      <div class="table-list">
        <div class="d-flex justify-content-between">
          <h4>Danh sách khách hàng</h4>
          <div class="col-md-3">
            <div class="input-group">
              <span class="input-group-text" id="basic-addon1">
                <i class="bi bi-search"></i>
              </span>
              <input
                type="text"
                class="form-control"
                placeholder="Tìm kiếm"
                aria-label="Input group example"
                aria-describedby="basic-addon1"
              />
            </div>
          </div>
          <button
            type="button"
            class="btn btn-primary"
            data-bs-toggle="modal"
            data-bs-target="#form-add"
          >
            Thêm khác hàng
          </button>
        </div>
        <table class="table mt-3">
          <thead>
            <tr>
              <th scope="col">STT</th>
              <th scope="col">Họ và tên</th>
              <th scope="col">Email</th>
              <th scope="col">Tên tài khoản</th>
              <th scope="col">Tên tài khoản</th>
              <th scope="col">Mật khẩu</th>
              <th scope="col">Role</th>
              <th scope="col">Chức năng</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>Mark</td>
              <td>Otto</td>
              <td>@mdo</td>
              <td>@mdo</td>
              <td>@mdo</td>
              <td>@mdo</td>
              <td>
                <button
                  type="button"
                  class="btn btn-primary btn-sm me-2"
                  data-bs-toggle="modal"
                  data-bs-target="#form-add"
                >
                  <i class="bi bi-pencil-square"></i>
                  Sửa
                </button>
                <button
                  type="button"
                  class="btn btn-outline-danger btn-sm"
                  data-bs-toggle="modal"
                  data-bs-target="#popupdanger"
                >
                  <i class="bi bi-trash"></i>
                  Xóa
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <TPopupVue
      popupId="popupdanger"
      popupTile="Xóa khách hàng"
      popupContent="Bạn có chắc chắn muốn xóa người dùng không"
    ></TPopupVue>

    <div
      class="modal fade"
      id="form-add"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Thêm khách hàng</h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <form class="row g-3">
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Email</label>
                <input type="email" class="form-control" id="inputEmail4" />
              </div>
              <div class="col-md-6">
                <label for="inputPassword4" class="form-label">Password</label>
                <input
                  type="password"
                  class="form-control"
                  id="inputPassword4"
                />
              </div>
              <div class="col-12">
                <label for="inputAddress" class="form-label">Address</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputAddress"
                  placeholder="1234 Main St"
                />
              </div>
              <div class="col-12">
                <label for="inputAddress2" class="form-label">Address 2</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputAddress2"
                  placeholder="Apartment, studio, or floor"
                />
              </div>
              <div class="col-md-6">
                <label for="inputCity" class="form-label">City</label>
                <input type="text" class="form-control" id="inputCity" />
              </div>
              <div class="col-md-4">
                <label for="inputState" class="form-label">State</label>
                <select id="inputState" class="form-select">
                  <option selected>Choose...</option>
                  <option>...</option>
                </select>
              </div>
              <div class="col-md-2">
                <label for="inputZip" class="form-label">Zip</label>
                <input type="text" class="form-control" id="inputZip" />
              </div>
              <div class="col-12">
                <div class="form-check">
                  <input
                    class="form-check-input"
                    type="checkbox"
                    id="gridCheck"
                  />
                  <label class="form-check-label" for="gridCheck">
                    Check me out
                  </label>
                </div>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Đóng
            </button>
            <button type="button" class="btn btn-primary">Lưu</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
  
  <script>
import TSidebar from "@/layout/TSidebar.vue";
import THeaderAdmin from "./THeaderAdmin.vue";
import TPopupVue from "@/components/TPopup.vue";
import { useStore } from "vuex";

export default {
  name: "TCustomerAdmin",
  components: {
    TSidebar,
    THeaderAdmin,
    TPopupVue,
  },
  setup() {
    const $store = useStore();
    return {
      $store,
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