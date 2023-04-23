<template>
  <div id="content" v-if="!isLoading">
    <TBanner></TBanner>
    <h1 class="product-title">Sản phẩm mới nhất</h1>
    <div class="product-list">
      <TProductitem
        v-for="(item, index) in productLatests"
        :id="item.ProductId"
        :key="index"
        :picture="item.ImgProduct"
        :productname="item.ProductName"
        :price="item.Price"
        :pricediscount="(item.Price * (100 -item.Discount)/100)"
      ></TProductitem>
    </div>
    <h1 class="product-title">Sản phẩm đang giảm giá</h1>
    <div class="product-list">
      <TProductitem
        v-for="(item, index) in productSales"
        :id="item.ProductId"
        :key="index"
        :picture="item.ImgProduct"
        :productname="item.ProductName"
        :price="item.Price"
        :pricediscount="(item.Price * (100 -item.Discount)/100)"
      ></TProductitem>
    </div>
    <h1 class="product-title">Sản phẩm bán nhiều nhất</h1>
    <div class="product-list">
      <TProductitem
        v-for="(item, index) in productBestSells"
        :id="item.ProductId"
        :key="index"
        :picture="item.ImgProduct"
        :productname="item.ProductName"
        :price="item.Price"
        :pricediscount="(item.Price * (100 -item.Discount)/100)"
      ></TProductitem>
    </div>
    <h1 class="news-title">Tin tức</h1>
    <div class="news-list">
      <TNewitem
        v-for="(item, index) in newsList"
        :key="index"
        :newpic="item.newpic"
        :newcontent="item.newcontent"
      ></TNewitem>
    </div>
  </div>
</template>

<script>
import TProductitem from "@/components/TProductItem.vue";
import TBanner from "@/components/TBanner.vue";
import TNewitem from "@/components/TNewsItem.vue";
import { PRODUCTS, NEWS } from "@/js/data";
import AXIOS_PRODUCT from "@/api/Product";
import { FILTER_CATEGORY } from "@/js/constrant";

import { reactive, ref, watch, watchEffect } from "vue";
import _ from "lodash";
export default {
  name: "TContent",
  components: {
    TProductitem,
    TBanner,
    TNewitem,
  },
  props: {
    categoryId: {
      type: String,
      default: null,
    },
  },

  setup(props) {
    const productList = reactive(PRODUCTS);
    const newsList = reactive(NEWS);
    const productLatests = ref([]);
    const productSales = ref([]);
    const productBestSells = ref([]);
    const isLoading = ref(false);

    const filterCategory = reactive(_.cloneDeep(FILTER_CATEGORY));
    /**
     * Hàm lấy danh sách sản phẩm tương ứng với danh mục
     * @param {id danh mục} param
     */
    const getProductLatest = async () => {
      try {
        filterCategory.categoryId = props.categoryId;
        filterCategory.order = "CreatedDate";
        let response = await AXIOS_PRODUCT.getProductByCategory(filterCategory);
        productLatests.value = response.data;
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Hàm lấy danh sách sản phẩm tương ứng với danh mục
     * @param {id danh mục} param
     */
    const getProductSale = async () => {
      try {
        filterCategory.categoryId = props.categoryId;
        filterCategory.order = "DisCount";
        let response = await AXIOS_PRODUCT.getProductByCategory(filterCategory);
        productSales.value = response.data;
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Hàm lấy danh sách sản phẩm tương ứng với danh mục
     * @param {id danh mục} param
     */
    const getProductBestSell = async () => {
      try {
        filterCategory.categoryId = props.categoryId;
        filterCategory.order = "Quantity";
        let response = await AXIOS_PRODUCT.getProductByCategory(filterCategory);
        productBestSells.value = response.data;
      } catch (err) {
        console.log(err);
      }
    };
    /**
     * Danh mục thay đổi gọi lại dữ liệu
     */
    watchEffect(() => {
      getProductLatest();
      getProductSale();
      getProductBestSell();
    });
    return {
      productList,
      newsList,
      productLatests,
      productSales,
      productBestSells,
      isLoading,
      filterCategory,
      getProductLatest,
      getProductSale,
      getProductBestSell,
    };
  },
};
</script>

<style>
</style>