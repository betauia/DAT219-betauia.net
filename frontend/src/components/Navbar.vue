<template>
    <div id="navbar">
    <link rel="stylesheet" href="https://unpkg.com/buefy/dist/buefy.min.css">
    <link rel="stylesheet" href="https://cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css">
        <div id="nav">
            <nav class="navbar is-info is-fixed-top">
      <div class="navbar-brand">
        <a class="navbar-item" ><router-link to="/">
          <img src="@/assets/img/logo.png" width="112" height="28">
          </router-link>
        </a>
        <div class="navbar-burger burger" data-target="navbar">
          <span></span>
          <span></span>
          <span></span>
        </div>
      </div>

      <div id="navbar" class="navbar-menu">
        <div class="navbar-start">
          <br><br>
            <a class="navbar-item">
            <router-link to="/">Home</router-link>
          </a>

          <a class="navbar-item">
            <router-link to="/about">About</router-link>
          </a>

          <a class="navbar-item">
            <router-link to="/for-bedrifter">For Bedrifter</router-link>
          </a>

          <div class="navbar-item has-dropdown is-hoverable">
            <a class="navbar-link">
              <router-link to="/events">Events</router-link>
            </a>
            <div class="navbar-dropdown is-boxed">
              <a class="navbar-item">
                <router-link to="/events/betalan">Betalan</router-link>
              </a>
              <a class="navbar-item">
                <router-link to="/events/retro">Retrokveld</router-link>
              </a>
              <a class="navbar-item">
                <router-link to="/events/bedpress">Bedpress</router-link>
              </a>
            </div>
          </div>
        </div>

        <div class="navbar-end">
          <div class="navbar-item">
            <router-link to="/login">
            <LoginModal></LoginModal>
            </router-link>
          </div>
          <div class="navbar-item">
            <SponsorsGallery CompanyName="Anzyz" />
          </div>
        </div>
      </div>
    </nav>
    </div>
    </div>
</template>

<script>
import LoginModal from '@/components/LoginModal.vue';
import SponsorsGallery from '@/components/SponsorsGallery.vue';

export default {
  name: 'Intro',
  components: {
    LoginModal,
    SponsorsGallery,
  },
};
</script>
<!--style lang="scss">
@import "~bulma/sass/utilities/_all";

// Set your colors
$primary: #6356AB;
$primary-invert: findColorInvert($primary);

// Setup $colors to use as bulma classes
$colors: (
    "white": ($white, $black),    color: #f5f8fc;

    "black": ($black, $white),    color: #f5f8fc;

    "light": ($light, $light-i    color: #f5f8fc;

    "dark": ($dark, $dark-inve    color: #f5f8fc;

    "primary": ($primary, $primary-invert),
    "info": ($info, $info-invert),
    "success": ($success, $success-invert),
    "warning": ($warning, $warning-invert),
    "danger": ($danger, $danger-invert),
);

@import "~bulma";
@import "~buefy/src/scss/buefy";

@import './assets/css/global.css';
</style-->
<style lang="scss">
#nav {
  #navbar{
  color: #448ef6;
}
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
