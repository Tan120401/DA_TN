<template>
  <div
    class="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark"
    style="width: 260px; height: 100%"
  >
    <router-link to="/Admin/Home">
      <a
        href="/"
        class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
      >
        <span class="fs-4">Quản lý chung</span>
      </a>
    </router-link>
    <hr />
    <ul class="nav nav-pills flex-column mb-auto">
      <router-link to="/Admin/Home">
        <li
          class="nav-link text-white"
          :class="{ active: $route.name == 'HomeAdmin' }"
          aria-current="page"
        >
          <i class="bi me-2 bi-house"></i>
          Thống kê báo cáo
        </li></router-link
      >
      <router-link to="/Admin/Customer">
        <li v-if="store.state.userInfo.Role == 'master admin'">
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'CustomerAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-person-circle"></i>
            Quản lý người dùng
          </a>
        </li></router-link
      >
      <router-link to="/Admin/Order">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'OrderAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-table"></i>
            Quản lý đơn hàng
          </a>
        </li></router-link
      >
      <router-link to="/Admin/Product">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{
              active:
                $route.name == 'ProductAdmin' || $route.name == 'ProductUpdate',
            }"
            aria-current="page"
          >
            <i class="bi me-2 bi-grid-fill"></i>
            Quản lý sản phẩm
          </a>
        </li></router-link
      >
    </ul>
  </div>
</template>

<script>
import { useStore } from "vuex";
export default {
  name: "TSidebar",
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