import { createRouter, createWebHistory } from "vue-router";

import THome from "@/views/THome";
import TAbout from "@/views/TAbout";
import TProductdetail from "@/views/TProductDetail";
import TCart from "@/views/TCart";
import TPay from "@/views/TPay";
import TLogin from "@/views/TLogin";
import TRegister from "@/views/TRegister";
import TProfile from "@/views/TProfile";
import THomeAdmin from "@/views/admin/THomeAdmin";
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
    path: "/ProductDetail/:id",
    name: "Productdetail",
    component: TProductdetail,
  },
  {
    path: "/ProductDetail/Cart",
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
    path: "/HomeAdmin",
    name: "HomeAdmin",
    component: THomeAdmin,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
