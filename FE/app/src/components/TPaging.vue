<template>
  <nav aria-label="Page navigation example">
    <ul class="pagination">
      <li class="page-item" @click="onPrevPage">
        <a class="page-link" href="#" aria-label="Previous">
          <span aria-hidden="true">&laquo;</span>
        </a>
      </li>
      <li
        class="page-item"
        v-for="(page, index) in listPage"
        :key="index"
        :class="{ active: page == pageNumber }"
        @click="changePage(page)"
      >
        <a class="page-link" href="#">{{ page }}</a>
      </li>
      <li class="page-item" @click="onNextPage">
        <a class="page-link" href="#" aria-label="Next">
          <span aria-hidden="true">&raquo;</span>
        </a>
      </li>
    </ul>
  </nav>
</template>

<script>
import { toRef, ref, reactive } from "vue";
export default {
  name: "TPaging",
  props: ["totalPage"],
  emits: ["onChangePageSize", "onChangPageNumber", "onSelectPageNumber"],
  setup(props, { emit }) {
    const pageNumber = ref(1);
    const listPage = ref([]);
    /**
     * Ấn chọn trang trước
     * Created By NVTAN (07/03/2023)
     */
    const onPrevPage = () => {
      if (pageNumber.value > 1) {
        pageNumber.value -= 1;
        emit("onChangPageNumber", parseInt(pageNumber.value));
      }
    };
    /**
     * Ấn chọn trang tiếp theo
     * Created By NVTAN (07/03/2023)
     */
    const onNextPage = () => {
      if (pageNumber.value < props.totalPage) {
        pageNumber.value += 1;
        emit("onChangPageNumber", parseInt(pageNumber.value));
      }
    };
    const changePage = (value) => {
      pageNumber.value = value;
      emit("onSelectPageNumber", value);
    };
    const pages = () => {
      let pages = []; //Chứa danh sách số trang
      for (let i = 1; i <= props.totalPage; i++) {
        pages.push(i);
        // if (
        //   i == 1 ||
        //   i == props.totalPage ||
        //   (i < pageNumber.value + 2 && i > pageNumber.value - 2)
        // ) {

        // } else if (i == pageNumber.value + 3 || i == pageNumber.value - 3) {
        //   pages.push("...");
        // }
      }
      listPage.value = pages;
    };
    pages();
    return {
      listPage,
      pages,
      pageNumber,
      onPrevPage,
      onNextPage,
      changePage,
    };
  },
};
</script>

<style>
.pagination {
  margin-top: -12px;
}
</style>