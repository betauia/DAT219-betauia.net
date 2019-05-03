<template>
  <nav class="navbar is-primary">
    <div class="navbar-brand">
      <a class="navbar-item">
        <router-link to="/">
          <img src="@/assets/img/logo.png">
        </router-link>
      </a>

      <a class="navbar-item">
      <router-link to="/blog">Blog</router-link>
      </a>
      <a class="navbar-item">
        <router-link to="/events">Events</router-link>
      </a>
    </div>

    <div class="navbar-menu">
    <div class="navbar-end">
      <a class="navbar-item">
        <router-link to="/account/register">Register</router-link>
      </a>

      <a class="navbar-item">
        <router-link to="/account/login">Login</router-link>
      </a>
      </div>
    </div>
  </nav>
</template>


<script>
export default {

};
</script>

<style lang="scss">

</style>
