<template>
  <nav class="navbar is-primary">
    <div class="navbar-brand">
      <a class="navbar-item">
        <router-link to="/">
          <img src="@/assets/img/logo.png">
        </router-link>
      </a>

      <a class="navbar-item">
      <router-link to="/blog">  Blog</router-link>
      </a>
      <a class="navbar-item">
        <router-link to="/events">Events</router-link>
      </a>
    </div>

    <div class="navbar-menu">
      <div class="navbar-end">
        <div class="navbar-item">
          <div class="field is-grouped">
            <p class="control">
              <a class="button is-medium">
                <router-link to="/account/register">Register</router-link>
              </a>
            </p>
            <p class="control">
              <a class="button is-small is-medium">
                <router-link to="/account/login">Login</router-link>
              </a>
            </p>
          </div>
        </div>
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
