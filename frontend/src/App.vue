<template>
<div class="app">
  <title>Betauia.net</title>
  <Navbar></Navbar>
    <router-view/>
  <Footer></Footer>
</div>
</template>


<script>
import Navbar from '@/components/Navbar.vue';
import Footer from '@/components/Footer.vue';

export default {
  components: {
    Navbar,
    Footer,
  },
};
</script>

<style lang="scss">

#nav {
  padding: 26px;
  height: 30px;
  a {
    font-weight: bold;
    color: #8bc7ff;
    font-size: 18px;
    &.router-link-exact-active {
      color: #003d74;
    }
  }
}
</style>
