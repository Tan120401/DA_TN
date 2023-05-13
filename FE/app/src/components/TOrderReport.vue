<template>
  <div class="d-flex flex-column p-4 orderitem-report" >
    <span name="header">{{ title }}</span>
    <div class="d-flex align-items-center mt-2">
        <i class="bi bi-bar-chart-steps me-2"></i>
        <span name="content">{{ content }} Đơn</span>
    </div>
  </div>
</template>

<script>
export default {
  name: "TOrderReport",
  props: ["title", "content"],
};
</script>

<style>
.orderitem-report{
    background-color: var(--primary-color);
    border-radius: 8px;
    max-width: 19%;
}
.orderitem-report span,
.orderitem-report i{
    font-size: 18px;
    font-weight: 500;
}
</style>