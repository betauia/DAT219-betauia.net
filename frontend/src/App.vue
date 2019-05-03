<template>
  <div class='app'>
    <title>Betauia.net</title>
        <Navbar></Navbar>
        <router-view/>
    <Footer></Footer>
  </div>
</template>


<script>
import Navbar from '@/components/Bars/Navbar.vue';
import Footer from '@/components/Bars/Footer.vue';

export default {
  components: {
    Navbar,
    Footer,
  },
};
</script>

<style lang='scss'>
.center {
  margin: auto;
  margin-left: auto;
  margin-right: auto;
  width: 40%;
}

.padding {
  padding: 20px;
}

a {
    color: #0085ff;
  }

@import '~bulma/sass/utilities/_all';
$primary: #0085ff;
$primary-invert: findColorInvert($primary);
$twitter: #4099ff;
$twitter-invert: findColorInvert($twitter);

$colors: (
  'white': (
    $white,
    $black
  ),
  'black': (
    $black,
    $white
  ),
  'light': (
    $light,
    $light-invert
  ),
  'dark': (
    $dark,
    $dark-invert
  ),
  'primary': (
    $primary,
    $primary-invert
  ),
  'info': (
    $info,
    $info-invert
  ),
  'success': (
    $success,
    $success-invert
  ),
  'warning': (
    $warning,
    $warning-invert
  ),
  'danger': (
    $danger,
    $danger-invert
  ),
  'twitter': (
    $twitter,
    $twitter-invert
  )
);

$link: #ffffff;
$link-invert: $primary-invert;
$link-focus-border: $primary;

@import '~bulma';
@import '~buefy/src/scss/buefy';
</style>
