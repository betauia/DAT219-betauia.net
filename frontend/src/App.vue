<template>
  <div class='app'>
    <title>Betauia.net</title>
        <Navbar></Navbar>
        <router-view/>
    <cookie-law>
  <div slot="message">
    This website uses cookies to ensure you get the best experience on our website. for more info <router-link to="/privacy" class="black">Click here</router-link>
  </div>
</cookie-law>
    <Footer></Footer>
  </div>
</template>

<script>
import Navbar from '@/components/Bars/Navbar.vue';
import Footer from '@/components/Bars/Footer.vue';
import CookieLaw from 'vue-cookie-law';

export default {
  components: {
    Navbar,
    Footer,
    CookieLaw,
  },
};
</script>

<style lang='scss'>
.center {
  margin: auto;
  margin-left: auto;
  margin-right: auto;
  width: 60%;
}

.padding {
  padding: 10px;
}

.black{
  color: #0085ff;
}

a {
    color: #0085ff;
}

// Imports
@import '~bulma/sass/utilities/_all';

// Variables
$primary: #0085ff;
$primary-accent: #0077e5;
$primary-invert: findColorInvert($primary);
$twitter: #4099ff;
$twitter-invert: findColorInvert($twitter);
$link: $primary-invert;
$link-invert: $primary-invert;
$link-focus-border: $primary;

// Navbar
$navbar-item-hover-background-color: $primary-accent;
$navbar-background-color: $primary;
$navbar-item-color: $white;


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

@import '~bulma';
@import '~buefy/src/scss/buefy';
</style>
