<template>
  <nav class="navbar has-navbar-fixed-top" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <router-link class="navbar-item" to="/">
        <img src="@/assets/img/logo.png">
      </router-link>
      <div class="navbar-burger burger" @click="isActive = !isActive">
        <span></span>
        <span></span>
        <span></span>
      </div>
    </div>
    <div class="navbar-menu" :class="{'is-active': isActive}">
      <div class="navbar-start">
        <router-link @click.native="isActive = false" class="navbar-item" to="/blog">Blog</router-link>
        <hr class="navbar-divider">
        <router-link @click.native="isActive = false" class="navbar-item" to="/events">Events</router-link>
        <hr class="navbar-divider">
        <router-link
          @click.native="isActive = false"
          class="navbar-item is-danger"
          to="/admin/dashboard"
        >Admin</router-link>
        <hr class="navbar-divider">
        <router-link
          @click.native="isActive = false"
          class="navbar-item is-danger"
          to="/sponsors"
        >Sponsors</router-link>
      </div>

      <div class="navbar-end">
        <router-link
          @click.native="isActive = false"
          class="navbar-item"
          to="/account/register"
        >Register</router-link>

        <router-link @click.native="isActive = false" class="navbar-item" to="/account/login">Login</router-link>
      </div>
    </div>
  </nav>
</template>


<script>
export default {
  data: () => ({
    isActive: false
  })
};
</script>

<style lang="scss">
.navbar {
  width: auto;
  border-bottom: 1px solid black;
}
</style>
