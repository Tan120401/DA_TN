/**
 * Định dạng tiền tệ VND
 */
const formatter = new Intl.NumberFormat("vi-VN", {
  style: "currency",
  currency: "VND",
});

const FILTER_CATEGORY = {
  categoryId: null,
  order: null,
}
const CART_OPTIONS = {
  CartId: null,
  ProductId: null,
  UserId: null,
  SizeProduct: null,
  NumProduct: null
}
export  {formatter,FILTER_CATEGORY,CART_OPTIONS}