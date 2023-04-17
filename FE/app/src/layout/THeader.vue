<template>
  <div id="header">
    <nav>
      <div class="logo">
        <router-link to="/"><h1>Nine Store</h1></router-link>
      </div>
      <ul class="header__nav">
        <router-link :class="{ active: $route.name == 'Home' }" to="/"
          ><li>Trang chủ</li></router-link
        >
        <router-link :class="{ active: $route.name == 'About' }" to="/About"
          ><li>Sản phẩm</li></router-link
        >
        <router-link :class="{ active: $route.name == 'About' }" to="/"
          ><li>Thông tin</li></router-link
        >
        <router-link :class="{ active: $route.name == 'About' }" to="/"
          ><li>Đánh giá</li></router-link
        >
        <router-link :class="{ active: $route.name == 'About' }" to="/"
          ><li>Liên hệ</li></router-link
        >
      </ul>
      <div class="d-flex align-items-center">
        <form class="col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3">
          <input
            type="search"
            class="form-control"
            placeholder="Tìm kiếm..."
            aria-label="Search"
          />
        </form>
        <router-link to="/Login"
          ><strong v-if="!$store.state.isLogin">Đăng nhập</strong></router-link
        >
        <div class="dropdown text-end" v-if="$store.state.isLogin">
          <a
            href="#"
            class="d-block link-dark text-decoration-none dropdown-toggle"
            id="dropdownUser1"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            <strong class="me-2"
              >Hi! {{ $store.state.userInfo.UserName }}</strong
            >
            <img
              :src="
                require(`../assets/img/user/${$store.state.userInfo.ImgName}`)
              "
              alt="mdo"
              width="32"
              height="32"
              class="rounded-circle"
            />
          </a>
          <ul
            class="dropdown-menu text-small"
            aria-labelledby="dropdownUser1"
            style=""
          >
            <router-link :to="`/Profile/${$store.state.userInfo.UserId}`"
              ><li>
                <a class="dropdown-item" href="#">Thông tin cá nhân</a>
              </li></router-link
            >
            <li><a class="dropdown-item" href="#">Giỏ hàng của tôi</a></li>
            <router-link to="/HomeAdmin"
              ><li>
                <a
                  class="dropdown-item"
                  href="#"
                  v-if="$store.state.userInfo.Role == 'admin'"
                  >Quản lý</a
                >
              </li></router-link
            >
            <li><hr class="dropdown-divider" /></li>
            <li>
              <a class="dropdown-item" href="#" @click="logout">Đăng xuất</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
import router from "@/router/router";
import { useStore } from "vuex";
export default {
  setup() {
    const $store = useStore();
    const logout = () => {
      $store.dispatch("setLogout");
      router.push({ path: "/" });
    };

    console.log($store.state.userInfo.UserId);
    return {
      logout,
    };
  },
};
</script>

<style>
.icons {
  display: flex;
  align-items: center;
}
.user__icon {
  position: relative;
  display: flex;
  align-items: center;
  flex-wrap: wrap;
  padding-bottom: 2px;
}
.user__infor {
  min-width: 92px;
  position: absolute;
  top: 20px;
  right: -19px;
  background-color: var(--primary-color);
  padding: 20px 10px;
  border-radius: 8px;
  cursor: pointer;
  display: none;
}
.user__infor span {
  text-align: center;
}
.user__img-avatar {
  top: 26px !important;
}
.user__icon:hover .user__infor {
  display: block;
}
.user__img {
  width: 24px;
  height: 24px;
  background-color: #ddd;
  border-radius: 50%;
}
</style>