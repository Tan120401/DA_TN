<template>
  <div id="header">
    <nav>
      <div class="logo">
        <router-link to="/"><h1>Nine Store</h1></router-link>
      </div>
      <ul class="header__nav">
        <router-link
          :class="{ active: $route.name == 'Home' }"
          :to="{
            path: `/`,
            query: { keyword: keyword },
          }"
          ><li>Trang chủ</li></router-link
        >
        <router-link
          v-for="(item, index) in lstCategory"
          :key="index"
          :class="{ active: $route.path == `/Category/${item.CategoryId}` }"
          :to="{
            path: `/Category/${item.CategoryId}`,
            query: { keyword: keyword },
          }"
          ><li>{{ item.CategoryName }}</li></router-link
        >
        <router-link
          :class="{ active: $route.path == '/Contact' }"
          to="/Contact"
          ><li>Liên hệ</li></router-link
        >
      </ul>
      <div class="d-flex align-items-center">
        <form class="col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3">
          <div class="input-group">
            <span class="input-group-text" id="basic-addon1">
              <i class="bi bi-search"></i>
            </span>
            <input
              type="text"
              class="form-control"
              placeholder="Tìm kiếm..."
              aria-label="Input group example"
              aria-describedby="basic-addon1"
              v-model="keyword"
            />
          </div>
        </form>

        <strong v-if="!$store.state.isLogin"
          ><router-link to="/Login">Đăng nhập</router-link>/<router-link
            to="/Register"
            >Đăng ký</router-link
          ></strong
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
              >Hi! {{ $store.state.userInfo.FullName }}</strong
            >
            <img
              :src="
                require(`../assets/img/user/${
                  $store.state.userInfo.ImgName || 'avatar-null.jpeg'
                }`)
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
            <router-link :to="`/Cart`"
              ><li>
                <a class="dropdown-item" href="#">Giỏ hàng của tôi</a>
              </li></router-link
            >
            <router-link :to="`/Order/0`"
              ><li>
                <a class="dropdown-item" href="#">Đơn hàng của tôi</a>
              </li></router-link
            >
            <router-link to="/Admin/Home"
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

import AXIOS_CATEGORY from "@/api/category";
import { ref, watch } from "vue";
export default {
  setup() {
    const $store = useStore();
    const logout = () => {
      $store.dispatch("setLogout");
      router.push({ path: "/" });
    };
    const lstCategory = ref([]);
    const keyword = ref();
    watch(keyword, (newVal) => {
      router.push({ query: { keyword: keyword.value } });
    });
    const getAllCategory = async () => {
      try {
        let response = await AXIOS_CATEGORY.getAllCategory();
        if (response) {
          lstCategory.value = response.data;
        }
      } catch (err) {
        console.log(err);
      }
    };
    getAllCategory();
    return {
      keyword,
      logout,
      lstCategory,
      getAllCategory,
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