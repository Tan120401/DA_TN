<template>
  <div id="main">
    <THeader></THeader>
    <div>
      <iframe
        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3723.4161859350597!2d105.73949047538781!3d21.056033480600174!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x313454fbc97e9359%3A0xc9402d11ceede5f4!2zTmfDoWNoIDI1LzEwNiDEkMaw4budbmcgUGjDuiBNaW5oLCBU4buVIGTDom4gcGjhu5EgVsSDbiBUcsOsLCBNaW5oIEtoYWksIELhuq9jIFThu6sgTGnDqm0sIEjDoCBO4buZaSwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1683824743709!5m2!1svi!2s"
        width="100%"
        height="500"
        style="border: 0; margin-top: 60px"
        allowfullscreen=""
        loading="lazy"
        referrerpolicy="no-referrer-when-downgrade"
      ></iframe>
      <div class="contact-group">
        <form class="contact-form" @submit.prevent="">
          <h2 class="contact-title login-title">LIÊN HỆ CHÚNG TÔI</h2>
          <TInput
            type="text"
            placeholder="Họ và tên"
            name="Họ và tên"
            :rules="['']"
          ></TInput>
          <TInput placeholder="Email" name="Email" :rules="['']"></TInput>
          <textarea
            name=""
            id=""
            placeholder="Thông tin cần gửi"
            cols="42"
            rows="3"
            class="info-contact"
          ></textarea>
          <button
            type="button"
            class="btn btn-secondary"
            style="margin-top: 20px"
          >
            GỬI TIN NHẮN
          </button>
        </form>
      </div>
    </div>
    <TFooter></TFooter>
  </div>
</template> 
  
  <script>
import { useRoute } from "vue-router";
import THeader from "@/layout/THeader.vue";
import TFooter from "@/layout/TFooter.vue";
import TInput from "@/components/TInput.vue";
export default {
  name: "TContact",
  components: {
    THeader,
    TFooter,
    TInput,
  },
  setup() {
    const route = useRoute();
    return {
      route,
    };
  },
};
</script>
  
  <style>
.contact-form {
  position: absolute;
  display: flex;
  justify-content: center;
  flex-direction: column;
  width: 488px;
  height: 480px;
  padding: 64px;
  top: 66px;
  right: 128px;
  background-color: var(--primary-color);
  border-radius: 32px;
}
.contact-title {
}
.info-contact {
  border-radius: 8px;
  padding: 20px;
}
</style>