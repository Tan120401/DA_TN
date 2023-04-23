<template>
  <div
    class="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark"
    style="width: 260px; height: 100%"
  >
    <router-link to="/">
      <a
        href="/"
        class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
      >
        <span class="fs-4">Quản lý chung</span>
      </a>
    </router-link>
    <hr />
    <ul class="nav nav-pills flex-column mb-auto">
      <router-link to="/HomeAdmin">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'HomeAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-house"></i>
            Trang chủ
          </a>
        </li></router-link
      >
      <router-link to="/OrderAdmin">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'OrderAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-table"></i>
            Đơn hàng
          </a>
        </li></router-link
      >
      <router-link to="/ProductAdmin">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'ProductAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-grid-fill"></i>
            Sản phẩm
          </a>
        </li></router-link
      >
      <router-link to="/CustomerAdmin">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'CustomerAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-person-circle"></i>
            Người dùng
          </a>
        </li></router-link
      >
      <router-link to="/ReportAdmin">
        <li>
          <a
            href="#"
            class="nav-link text-white"
            :class="{ active: $route.name == 'ReportAdmin' }"
            aria-current="page"
          >
            <i class="bi me-2 bi-pie-chart"></i>
            Thống kê báo cáo
          </a>
        </li></router-link
      >
    </ul>
  </div>
</template>

<script>
export default {
  name: "TSidebar",
};
</script>

<style>
</style>