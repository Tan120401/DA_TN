<template>
  <div id="header">
    <nav>
      <div class="logo">
        <router-link to="/"><h1>Nine Store</h1></router-link>
      </div>
      <ul>
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
      <div class="icons">
        <i class="fa-solid fa-heart"></i>
        <i class="fa-solid fa-cart-shopping"></i>
        <div class="user__icon">
          <i class="fa-solid fa-user" v-if="!$store.state.isLogin"></i>
          <div class="user__img" v-if="$store.state.isLogin">
            <img src="" alt="" />
          </div>
          <div
            class="user__infor"
            :class="{ 'user__img-avatar': $store.state.isLogin }"
          >
            <router-link to="/Login"
              ><span v-if="!$store.state.isLogin" class="flex-item flex-col">Login</span></router-link
            >
            <div v-if="$store.state.isLogin" class="flex-item flex-col">
              <router-link :to = "`/Profile/${$store.state.userId}`"><span>Thông tin</span></router-link>
              <span @click="logout">Logout</span>
            </div>
          </div>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
import router from '@/router/router';
import { useStore } from "vuex";
export default {
  setup() {
    const $store = useStore();
    const logout = () => {
      $store.dispatch("setLogout");
      router.push({path: "/"})
    };
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
.user__infor span{
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