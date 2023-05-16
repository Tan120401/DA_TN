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
  keyWord: null,
};
const CART_OPTIONS = {
  CartId: null,
  ProductId: null,
  UserId: null,
  SizeProduct: null,
  NumProduct: null,
  SizeId: null,
};
const ORDER_OPTIONS = {
  OrderId: null,
  UserId: null,
  Status: 0,
};
const ORDERDETAIL_OPTIONS = {
  OrderDetailId: null,
  OrderId: null,
  ProductId: null,
  Quantity: 0,
  Price: 0,
  Size: 0,
};
const BILL_OPTION = {
  BillId: null,
  OrderId: null,
  UserName: null,
  Address: null,
  PhoneNumber: null,
  SumPrice: 0,
};

const FILTER_OPTION = {
  keyWord: "",
  pageNumber: 1,
  pageSize: 10,
};
const FILTER_USERID = {
  keyWord: "",
  pageNumber: 1,
  pageSize: 10,
  UserId: null,
};
/**
 * Định dạng tiền tệ VND
 */
const FORMAT_MONEY = new Intl.NumberFormat("vi-VN", {
  style: "currency",
  currency: "VND",
});
export {
  formatter,
  FILTER_CATEGORY,
  CART_OPTIONS,
  FORMAT_MONEY,
  ORDER_OPTIONS,
  ORDERDETAIL_OPTIONS,
  BILL_OPTION,
  FILTER_OPTION,
  FILTER_USERID,
};
