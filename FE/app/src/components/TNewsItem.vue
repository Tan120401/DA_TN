<template>
  <div class="news-item">
    <div
      class="news-img"
      :style="
        `background-image: url(${newpic})`
      "
    ></div>
    <h3>
      {{ newcontent }}
    </h3>
    <div class="news__show-detail tbutton">Xem chi tiết</div>
  </div>
</template>

<script>
export default {
  name: "TNewitem",
  props: ["newcontent", "newpic"],
};
</script>

<style>
</style>