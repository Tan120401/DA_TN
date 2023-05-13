<template>
  <header class="p-3 bg-dark text-white" style="width: 100%">
    <div class="container">
      <div class="d-flex flex-wrap align-items-center justify-content-end">
        <div class="dropdown text-end">
          <a
            class="d-block link-dark text-decoration-none dropdown-toggle"
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
            <strong class="text-white">Nguyễn Văn Tân</strong>
          </a>
          <ul
            class="dropdown-menu dropdown-menu text-small shadow"
            aria-labelledby="dropdownUser1"
          >
            <li><a class="dropdown-item">Cài đặt</a></li>
            <router-link :to="`/Profile/${store.state.userInfo.UserId}`"
              ><li>
                <a class="dropdown-item">Thông tin cá nhân</a>
              </li></router-link
            >
            <li><hr class="dropdown-divider" /></li>
            <li><a class="dropdown-item" @click="logout">Đăng xuất</a></li>
          </ul>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { useStore } from "vuex";
import { watch, ref } from "vue";
import router from "@/router/router";
export default {
  name: "THeaderAdmin",
  setup() {
    const store = useStore();
    const logout = () => {
      store.dispatch("setLogout");
      router.push({ path: "/" });
    };
    return {
      logout,
      store,
    };
  },
};
</script>

<style>
</style>