<template>
  <header class="p-3 bg-dark text-white" style="width: 100%">
    <div class="container">
      <div class="d-flex flex-wrap align-items-center justify-content-between">
        <form class="col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3">
          <input
            type="search"
            class="form-control form-control-dark"
            placeholder="Tìm kiếm..."
            aria-label="Search"
          />
        </form>
        <div class="dropdown">
          <a
            href="#"
            class="d-flex align-items-center text-white text-decoration-none dropdown-toggle"
            id="dropdownUser1"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            <img
              :src="
                require(`../../assets/img/user/${
                  store.state.userInfo.ImgName || 'avatar-null.jpeg'
                }`)
              "
              alt=""
              width="32"
              height="32"
              class="rounded-circle me-2"
            />
            <strong>Nguyễn Văn Tân</strong>
          </a>
          <ul
            class="dropdown-menu dropdown-menu-dark text-small shadow"
            aria-labelledby="dropdownUser1"
          >
            <li><a class="dropdown-item" href="#">Cài đặt</a></li>
            <router-link :to="`/Profile/${store.state.userInfo.UserId}`"
              ><li>
                <a class="dropdown-item" href="#">Thông tin cá nhân</a>
              </li></router-link
            >
            <li><hr class="dropdown-divider" /></li>
            <li><a class="dropdown-item" href="#">Đăng xuất</a></li>
          </ul>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { useStore } from "vuex";
export default {
  name: "THeaderAdmin",
  setup() {
    const store = useStore();
    return {
      store,
    };
  },
};
</script>

<style>
</style>