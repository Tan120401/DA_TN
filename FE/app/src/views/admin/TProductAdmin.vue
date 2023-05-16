<template>
  <div class="table-list">
    <div class="d-flex justify-content-between">
      <h4>Danh sách sản phẩm</h4>
      <button type="button" class="btn btn-primary" @click="onShowAddProduct">
        Thêm sản phẩm
      </button>
    </div>
    <div class="input-group" style="width: 300px">
      <span class="input-group-text" id="basic-addon1">
        <i class="bi bi-search"></i>
      </span>
      <input
        type="text"
        class="form-control"
        placeholder="Tìm kiếm..."
        aria-label="Input group example"
        aria-describedby="basic-addon1"
        v-model="keywordSearch"
      />
    </div>
    <div class="table-record-list product-table-list">
      <table class="table mt-3">
        <thead>
          <tr>
            <th scope="col">STT</th>
            <th scope="col" style="width: 80px">Hình ảnh</th>
            <th scope="col">Danh mục</th>
            <th scope="col">Tên sản phẩm</th>
            <th scope="col">Giá</th>
            <th scope="col">Giảm giá</th>
            <th scope="col" style="text-align: center">Tình trạng</th>
            <th scope="col">Mô tả</th>
            <th scope="col">Chức năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in productData" :key="index">
            <th scope="row">{{ index + 1 }}</th>
            <td>
              <img
                style="width: 100%"
                :src="require(`@/assets/img/product/${item.ImgProduct}`)"
                alt=""
              />
            </td>
            <td>
              <div>{{ item.CategoryName }}</div>
            </td>
            <td>{{ item.ProductName }}</td>
            <td>{{ item.Price }}</td>
            <td>{{ item.Discount }}%</td>
            <td style="text-align: center">
              <div
                :class="[
                  item.Status == 1 ? 'product-selling' : 'product-soldout',
                ]"
              >
                {{ formatStatus(item.Status) }}
              </div>
            </td>
            <td>{{ item.Description.substring(0, 28) }}...</td>
            <td>
              <button
                type="button"
                class="btn btn-primary me-2"
                @click="onClickEdit(item.ProductId)"
              >
                Sửa
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div v-if="!isLoading">
      <t-paging
        :totalPage="totalPage"
        @onChangPageNumber="onChangPageNumber"
        @onSelectPageNumber="onChangPageNumber"
      ></t-paging>
    </div>
  </div>
  <TPopup
    popupTile="Thêm sản phẩm"
    @hidePopup="hidePopup"
    v-if="isShowPopupAdd"
  >
    <template #content>
      <div ref="validateFormAdd" class="d-flex" style="width: 900px">
        <div style="width: 260px" class="me-2">
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
                :src="item"
                alt="Avatar"
                class="col-md-5 m-2"
              />
            </div>
          </div>
        </div>
        <div style="flex: 1">
          <div>
            <div class="row">
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Tên sản phẩm</label>
                <TInput
                  type="text"
                  name="Tên sản phẩm"
                  v-model="productOption.ProductName"
                  :rules="['Empty']"
                ></TInput>
              </div>
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label"
                  >Danh mục sản phẩm</label
                >
                <select
                  id="inputState"
                  class="form-select"
                  style="border: 1px solid var(--primary-color)"
                  v-model="productOption.CategoryId"
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
            </div>
            <div class="row">
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Giá bán</label>
                <TInput
                  type="text"
                  name=""
                  v-model="productOption.Price"
                  :rules="['Empty']"
                ></TInput>
              </div>
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Giảm giá</label>
                <TInput
                  name=""
                  type="text"
                  v-model="productOption.Discount"
                  :rules="['Empty']"
                ></TInput>
              </div>
            </div>
            <div class="row"></div>
            <div class="row">
              <div class="col-md-12">
                <label for="inputEmail4" class="form-label">Mô tả</label>
                <TInput
                  type="text"
                  name="Mô tả sản phẩm"
                  v-model="productOption.Description"
                  :rules="['Empty']"
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
                <label for="inputEmail4" class="form-label">Kích thước</label>
                <TInput
                  name=""
                  type="text"
                  v-model="item.SizeNumber"
                  :rules="['Empty']"
                ></TInput>
              </div>
              <div class="col-md-5">
                <label for="inputEmail4" class="form-label">Số lượng</label>
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
          <button type="button" class="btn btn-sm btn-secondary" @click="onAddSize">
            <i class="bi bi-plus-lg me-1"></i> Thêm kích thước
          </button>
        </div>
      </div>
    </template>
    <template #footer>
      <button type="button" class="btn btn-secondary" @click="hidePopup">
        Đóng
      </button>
      <button type="button" class="btn btn-primary" @click="onAddProduct">
        Thêm
      </button>
    </template>
  </TPopup>
  <TLoading v-if="isLoading"></TLoading>
  <div v-if="isShowNoData" class="text-nodata">
    Không có sản phẩm nào thỏa mãn.
  </div>
</template>
  <script>
import { watch, reactive, ref } from "vue";
import _ from "lodash";
import { v4 as uuidv4 } from "uuid";

import TInput from "@/components/TInput.vue";
import TPaging from "@/components/TPaging.vue";
import TPopup from "@/components/TPopup.vue";
import TLoading from "@/components/TLoading.vue";

import router from "@/router/router";
import AXIOS_PRODUCT from "@/api/Product";
import AXIOS_CATEGORY from "@/api/category";
import { FILTER_OPTION } from "@/js/constrant";
import { PRODUCT_OPTION } from "@/js/data";

import { validateData } from "@/js/validateion";
import AXIOS_SIZE from "@/api/size";
import AXIOS_IMAGE from "@/api/image";
export default {
  components: {
    TPopup,
    TInput,
    TPaging,
    TLoading,
  },
  setup() {
    const isLoading = ref(false);
    const isShowNoData = ref(false);
    const filterOption = reactive(_.cloneDeep(FILTER_OPTION));
    const productData = ref([]);
    const totalPage = ref();
    const isShowPopupAdd = ref(false);
    const productOption = ref([]);
    const lstCategory = ref([]);
    const validateFormAdd = ref(null);
    const lstUrl = ref(null);

    const lstImageProduct = ref([]);
    const lstImagePath = ref([]);
    const sizeOption = ref([
      {
        SizeId: null,
        SizeNumber: null,
        Quantity: null,
        ProductId: null,
      },
    ]);

    const onClickEdit = (value) => {
      router.push({ path: `ProductUpdate/${value}` });
    };
    /**
     * Thay đổi từ khóa tìm kiếm gọi lại phân trang
     */
    const keywordSearch = ref();
    watch(
      keywordSearch,
      _.debounce((newVal) => {
        filterOption.keyWord = newVal;
        filterOption.pageNumber = 1;
        getProductByFilter(filterOption);
      }, 300)
    );

    /**
     * Thêm kích thước và số lượng
     */
    const onAddSize = () => {
      sizeOption.value.push({
        SizeId: null,
        SizeNumber: null,
        Quantity: null,
        ProductId: productOption.value.ProductId,
      });
    };
    /**
     * Bỏ kích thước và số lượng
     */

    const onRemoveSize = (index) => {
      sizeOption.value.splice(index, 1);
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
     * Hàm bắt sự kiện khi chọn file
     * Created by NVTAN(10/04/2023)
     */
    const onChoseFile = (event) => {
      const file = event.target.files;
      var lstUrls = [];
      productOption.value.ImgProduct = file[0].name;
      for (let i = 0; i < file.length; i++) {
        lstUrls.push(URL.createObjectURL(file[i]));
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
          ProductId: productOption.value.ProductId,
        });
      }
      lstUrl.value = lstUrls;
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
     * Thêm sản phẩm
     */
    const onAddProduct = async () => {
      var lstInput = validateFormAdd.value.querySelectorAll("input");
      var inValid = validateData(lstInput);
      if (inValid.isValidate) {
        await addProduct(productOption.value);
        for (var i = 0; i < lstImageProduct.value.length; i++) {
          await uploadFile(lstImageProduct.value[i]);
          await insertToImage(lstImagePath.value[i]);
        }
        insertToSize(sizeOption.value);
        hidePopup();
        getProductByFilter(filterOption);
      }
    };
    /**
     * Hàm thêm sản phẩm
     */
    const addProduct = async (param) => {
      try {
        let response = await AXIOS_PRODUCT.insertToProduct(param);
        if (response) {
          alert("thanh cong");
        }
      } catch (error) {
        console.log(error);
      }
    };
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
     * Hiện form thêm
     */
    const onShowAddProduct = () => {
      lstUrl.value = [];
      sizeOption.value = [
        {
          SizeId: null,
          SizeNumber: null,
          Quantity: null,
          ProductId: null,
        },
      ];
      lstImageProduct.value = [];
      productOption.value = _.cloneDeep(PRODUCT_OPTION);
      productOption.value.ProductId = uuidv4();
      sizeOption.value[0].ProductId = productOption.value.ProductId;
      isShowPopupAdd.value = true;
    };
    /**
     * Ẩn form thêm
     */
    const hidePopup = () => {
      if (isShowPopupAdd.value) {
        isShowPopupAdd.value = false;
      }
    };
    /**
     * Lấy danh sách sản phẩm theo phân trang
     */
    const getProductByFilter = async (params) => {
      try {
        let response = await AXIOS_PRODUCT.getProductByFilter(params);
        isLoading.value = true;

        if (response) {
          productData.value = response.data.Data;
          totalPage.value = response.data.TotalPage;
          setTimeout(() => {
            isLoading.value = false;
          }, 500);
          if (totalPage.value < 1) {
            isShowNoData.value = true;
          } else {
            isShowNoData.value = false;
          }
        }
      } catch (error) {
        console.log(error);
      }
    };
    getProductByFilter(filterOption);
    /**
     * Thay đổi số trang
     */
    const onChangPageNumber = (value) => {
      filterOption.pageNumber = value;
      getProductByFilter(filterOption);
    };
    /**
     * Định dạng tình trạng sản phẩm
     */
    const formatStatus = (value) => {
      if (value == 0) {
        return "Ngưng bán";
      } else if (value == 1) {
        return "Đang bán";
      }
    };
    return {
      keywordSearch,
      lstImagePath,
      lstImageProduct,
      sizeOption,
      lstUrl,
      validateFormAdd,
      lstCategory,
      isLoading,
      isShowNoData,
      totalPage,
      filterOption,
      productData,
      isShowPopupAdd,
      productOption,
      onClickEdit,
      insertToSize,
      onRemoveSize,
      onAddSize,
      uploadFile,
      insertToImage,
      addProduct,
      onChoseFile,
      getAllCategory,
      onChangPageNumber,
      formatStatus,
      getProductByFilter,
      onShowAddProduct,
      hidePopup,
      onAddProduct,
    };
  },
};
</script>
  
  <style>
.product-selling {
  padding: 8px 0;
  border: 1px solid #11aa7a;
  color: #11aa7a;
  background-color: #b9f8e4;
  border-radius: 4px;
}
.product-soldout {
  padding: 8px 0;
  border: 1px solid #6153df;
  color: #6153df;
  background-color: rgb(97, 83, 223, 0.3);
  border-radius: 4px;
}
.product-table-list tr {
  vertical-align: middle;
}
</style>