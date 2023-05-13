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
      <router-link to="/Admin/Order">
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
      <router-link to="/Admin/Product">
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

      <!-- <router-link to="/Admin/Report">
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
      > -->
      <!-- <li class="mb-1">
        <button
          class="btn btn-toggle align-items-center rounded nav-link text-white"
          data-bs-toggle="collapse"
          data-bs-target="#home-collapse"
          aria-expanded="true"
        >
          <i class="bi me-2 bi-grid-fill"></i>
          Home
        </button>
        <div class="collapse show" id="home-collapse" style="">
          <ul class="btn-toggle-nav list-unstyled" style="margin-left: 42px">
            <li class="mb-1"><a href="#" class="text-white">Danh sách sản phẩm</a></li>
            <li class="mb-1"><a href="#" class="text-white">Thêm sản phẩm</a></li>
            <li class="mb-1"><a href="#" class="text-white">Reports</a></li>
          </ul>
        </div>
      </li> -->
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