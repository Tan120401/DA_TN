/**
 * Định dạng tiền tệ VND
 */
const formatter = new Intl.NumberFormat("vi-VN", {
  style: "currency",
  currency: "VND",
});

export  {formatter}