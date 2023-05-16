<template>
  <div class="table-list d-flex flex-col justify-content-between">
    <div ref="validateFormUpdate">
      <h3>Sửa thông tin sản phẩm</h3>
      <div class="d-flex">
        <div style="width: 360px">
          <div class="row">
            <div class="col-md-12 me-3" style="text-align: center">
              <input
                type="file"
                id="files"
                hidden
                @change="onChoseFile"
                multiple="multiple"
              />
              <label for="files" class="btn btn-secondary btn-sm"
                >Chọn hình ảnh</label
              >
            </div>
            <div class="row">
              <div class="row ms-2">
                <img
                  style="
                    border: 1px solid var(--primary-color);
                    border-radius: 4px;
                  "
                  v-for="(item, index) in lstUrl"
                  :key="index"
                  :src="[
                    item.ImageLink
                      ? require(`@/assets/img/product/${item.ImageLink}`)
                      : item,
                  ]"
                  alt="Avatar"
                  class="col-md-5 m-2"
                />
              </div>
            </div>
          </div>
        </div>
        <div style="flex: 1">
          <div>
            <div class="row">
              <div class="col-md-6">
                <label class="form-label">Tên sản phẩm</label>
                <TInput
                  type="text"
                  name="Tên sản phẩm"
                  v-model="productData.ProductName"
                  :rules="['Empty']"
                ></TInput>
              </div>
              <div class="col-md-3">
                <label for="inputEmail" class="form-label"
                  >Danh mục sản phẩm</label
                >
                <select
                  id="inputState"
                  class="form-select"
                  style="border: 1px solid var(--primary-color)"
                  v-model="productData.CategoryId"
                >
                  <option
                    v-for="(item, index) in lstCategory"
                    :key="index"
                    :value="item.CategoryId"
                  >
                    {{ item.CategoryName }}
                  </option>
                </select>
              </div>
              <div class="col-md-3">
                <label class="form-label">Tình trạng</label>
                <select
                  id="inputState"
                  class="form-select"
                  style="border: 1px solid var(--primary-color)"
                  v-model="productData.Status"
                >
                  <option :value="1">Đang bán</option>
                  <option :value="0">Ngưng bán</option>
                </select>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6">
                <label class="form-label">Giá bán</label>
                <TInput
                  type="text"
                  name=""
                  :rules="['Empty']"
                  v-model="productData.Price"
                ></TInput>
              </div>
              <div class="col-md-6">
                <label class="form-label">Giảm giá</label>
                <TInput
                  type="text"
                  :rules="['Empty']"
                  v-model="productData.Discount"
                ></TInput>
              </div>
            </div>
            <div class="row">
              <div class="col-md-12">
                <label class="form-label">Mô tả</label>
                <TInput
                  type="text"
                  name="Mô tả sản phẩm"
                  :rules="['Empty']"
                  v-model="productData.Description"
                ></TInput>
              </div>
            </div>
          </div>
          <hr />
          <div
            style="
              width: 100%;
              height: 300px;
              overflow-x: hidden;
              overflow-y: auto;
            "
          >
            <div
              class="row d-flex align-items-center"
              v-for="(item, index) in sizeOption"
              :key="index"
            >
              <div class="col-md-5">
                <label class="form-label">Kích thước</label>
                <TInput
                  name=""
                  type="text"
                  v-model="item.SizeNumber"
                  :rules="['Empty']"
                ></TInput>
              </div>
              <div class="col-md-5">
                <label class="form-label">Số lượng</label>
                <TInput
                  type="text"
                  name=""
                  v-model="item.Quantity"
                  :rules="['Empty']"
                ></TInput>
              </div>
              <button
                type="button"
                class="btn-close"
                @click="onRemoveSize(index)"
              ></button>
            </div>
          </div>
          <div class="d-flex justify-content-between">
            <button
              type="button"
              class="btn btn-sm btn-secondary"
              @click="onAddSize"
            >
              <i class="bi bi-plus-lg me-1"></i> Thêm kích thước
            </button>
            <div>
              <button
                type="button"
                class="btn btn-secondary me-2"
                @click="onBack"
              >
                Đóng
              </button>
              <button
                type="button"
                class="btn btn-primary"
                @click="onUpdateProduct"
              >
                Sửa
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AXIOS_CATEGORY from "@/api/category";
import TInput from "@/components/TInput.vue";
import { ref, watch, watchEffect } from "vue";
import { useRoute } from "vue-router";
import AXIOS_PRODUCT from "@/api/Product";
import AXIOS_SIZE from "@/api/size";
import router from "@/router/router";
import AXIOS_IMAGE from "@/api/image";

import { validateData } from "@/js/validateion";
import { notification } from "ant-design-vue";
import { reactive } from "@vue/reactivity";
export default {
  name: "TProductUpdate",
  components: {
    TInput,
  },
  setup() {
    const lstCategory = ref([]);
    const route = useRoute();
    const productData = ref([]);
    const sizeOption = ref([]);
    const sizeOptionDefault = reactive([]);
    const lstUrl = ref([]);
    const lstImageProduct = ref([]);
    const lstImagePath = ref([]);
    const validateFormUpdate = ref(null);
    const productId = route.params.id;
    const isChangeFile = ref(false);
    const isChangeSize = ref(false);
    /**
     * Trở về
     */
    const onBack = () => {
      router.push({ path: "/Admin/Product" });
    };

    watch(
      sizeOption,
      (newValue) => {
        if (_.isEqual(sizeOptionDefault.value, newValue)) {
          isChangeSize.value = false;
        } else {
          isChangeSize.value = true;
        }
      },
      { deep: true }
    );

    /**
     * Lấy tất cả danh mục sản phẩm
     */
    const getAllCategory = async () => {
      try {
        let response = await AXIOS_CATEGORY.getAllCategory();
        if (response) {
          lstCategory.value = response.data;
        }
      } catch (error) {
        console.log(error);
      }
    };
    getAllCategory();
    /**
     * Lấy sản phẩm thông qua id
     */
    const getProductById = async (param) => {
      try {
        let response = await AXIOS_PRODUCT.getProductById(param);
        if (response) {
          productData.value = response.data[0];
        }
      } catch (error) {
        console.log(error);
      }
    };
    getProductById(productId);
    /**
     * Hàm lấy danh sách kích thước
     */
    const getSizeByProductId = async (param) => {
      try {
        let response = await AXIOS_SIZE.getSizeByProductId(param);
        if (response) {
          sizeOption.value = response.data;
          sizeOptionDefault.value = [...response.data];
        }
      } catch (error) {
        console.log(error);
      }
    };
    getSizeByProductId(productId);
    /**
     *
     * @param {Id sản phẩm} id
     * @param {Thông tin sản phẩm} params
     */
    const updateProduct = async (id, params) => {
      try {
        let response = await AXIOS_PRODUCT.updateToProduct(id, params);
        if (response) {
          notification.success({ message: "Sửa sản phẩm thành công" });
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Ấn nút sửa
     */

    const onUpdateProduct = async () => {
      var lstInput = validateFormUpdate.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        if (isChangeFile.value) {
          await deleteImageByProductId(productId);
          for (var i = 0; i < lstImageProduct.value.length; i++) {
            await uploadFile(lstImageProduct.value[i]);
            await insertToImage(lstImagePath.value[i]);
          }
        }
        if (isChangeSize.value) {
          console.log(isChangeSize.value);
          await deleteSizeByProductId(productId);
          await insertToSize(sizeOption.value);
        }
        await updateProduct(productId, productData.value);
        router.push({ path: "/Admin/Product" });
      }
    };
    /**
     * Thêm size của sản phẩm vào bảng size
     */
    const insertToSize = async (params) => {
      try {
        let response = await AXIOS_SIZE.insertMulptySize(params);
        if (response) {
          console.log("success");
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Thêm ảnh vào máy
     */
    const uploadFile = async (params) => {
      try {
        let response = await AXIOS_PRODUCT.uploadFile(params);
        if (response) {
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Xóa ảnh cũ
     */
    const deleteImageByProductId = async (param) => {
      try {
        let response = await AXIOS_IMAGE.deleteImageByProductId(param);
        if (response) {
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Xóa size cũ
     */
    const deleteSizeByProductId = async (param) => {
      try {
        let response = await AXIOS_SIZE.deleteSizeByProductId(param);
        if (response) {
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Thêm ảnh sản phẩm vào bảng ảnh
     */
    const insertToImage = async (params) => {
      try {
        let response = await AXIOS_IMAGE.insertToImage(params);
        if (response) {
        }
      } catch (error) {
        console.log(error);
      }
    };
    /**
     * Hàm lấy danh sách ảnh thông qua id
     */
    const getImageByProductId = async (param) => {
      try {
        let response = await AXIOS_IMAGE.getImageByProductId(param);
        if (response) {
          lstUrl.value = response.data;
        }
      } catch (error) {
        console.log(error);
      }
    };
    getImageByProductId(productId);
    /**
     * Hàm bắt sự kiện khi chọn file
     * Created by NVTAN(10/04/2023)
     */
    const onChoseFile = (event) => {
      const file = event.target.files;
      lstUrl.value = [];
      productData.value.ImgProduct = file[0].name;
      for (let i = 0; i < file.length; i++) {
        lstUrl.value.push(URL.createObjectURL(file[i]));
        /**
         * Danh sách để thêm ảnh vào folder
         */
        lstImageProduct.value.push({
          File: file[i],
          FileName: file[i].name,
        });
        /**
         * Danh sách để chèn vào bảng ảnh
         */
        lstImagePath.value.push({
          ImageProductId: null,
          ImageLink: file[i].name,
          ProductId: productId,
        });
      }
      isChangeFile.value = true;
    };
    /**
     * Thêm kích thước và số lượng
     */
    const onAddSize = () => {
      sizeOption.value.push({
        SizeId: null,
        SizeNumber: null,
        Quantity: null,
        ProductId: productId,
      });
    };
    /**
     * Bỏ kích thước và số lượng
     */
    const onRemoveSize = (index) => {
      sizeOption.value.splice(index, 1);
    };
    return {
      route,
      productId,
      productData,
      lstCategory,
      sizeOption,
      sizeOptionDefault,
      lstUrl,
      lstImageProduct,
      lstImagePath,
      validateFormUpdate,
      isChangeFile,
      isChangeSize,
      updateProduct,
      onUpdateProduct,
      onBack,
      getSizeByProductId,
      getAllCategory,
      getProductById,
      getImageByProductId,
      uploadFile,
      insertToImage,
      deleteImageByProductId,
      deleteSizeByProductId,
      insertToSize,
      onChoseFile,
      onAddSize,
      onRemoveSize,
    };
  },
};
</script>

<style>
</style>