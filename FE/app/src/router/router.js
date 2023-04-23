import { createRouter, createWebHistory } from "vue-router";

import THome from "@/views/THome";
import TAbout from "@/views/TAbout";
import TProductdetail from "@/views/TProductDetail";
import TCart from "@/views/TCart";
import TPay from "@/views/TPay";
import TLogin from "@/views/TLogin";
import TRegister from "@/views/TRegister";
import TProfile from "@/views/TProfile";
import TCategory from "@/views/TCategory";

import THomeAdmin from "@/views/admin/THomeAdmin";
import TCustomerAdmin from "@/views/admin/TCustomerAdmin";
// import TProductAdmin from "@/views/admin/TProductAdmin"
const routes = [
  {
    path: "/",
    name: "Home",
    component: THome,
  },
  {
    path: "/About",
    name: "About",
    component: TAbout,
  },
  {
    path: "/Category/ProductDetail/:id",
    name: "Productdetail",
    component: TProductdetail,
  },
  {
    path: "/ProductDetail/:id",
    name: "Productdetail",
    component: TProductdetail,
  },
  {
    path: "/Cart",
    name: "Cart",
    component: TCart,
  },
  {
    path: "/Login",
    name: "Login",
    component: TLogin,
  },
  {
    path: "/Register",
    name: "Register",
    component: TRegister,
  },
  {
    path: "/Profile/:id",
    name: "Profile",
    component: TProfile,
  },
  {
    path: "/Pay",
    name: "Pay",
    component: TPay,
  },
  {
    path: "/Category/:id",
    name: "Category",
    component: TCategory,
  },
  {
    path: "/HomeAdmin",
    name: "HomeAdmin",
    component: THomeAdmin,
  },
  {
    path: "/CustomerAdmin",
    name: "CustomerAdmin",
    component: TCustomerAdmin,
  },
  // {
  //   path: "/ProductAdmin",
  //   name: "ProductAdmin",
  //   component: TProductAdmin,
  // },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
