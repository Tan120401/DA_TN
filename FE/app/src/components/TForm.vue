<template>
  <div class="modal1">
    <form id="form-control">
      <div class="form__header">
        <h2>Thông tin cá nhân</h2>
      </div>
      <i class="fa-solid fa-xmark t-icon-close"></i>
      <TInput
        type="text"
        placeholder="Tên đăng nhập"
        name="Tên đăng nhập"
        :rules="['Empty']"
      ></TInput>
    </form>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>