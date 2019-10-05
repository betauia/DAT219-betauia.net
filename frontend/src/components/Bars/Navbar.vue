<template>
  <nav class="navbar is-fixed-top" role="navigation" aria-label="main navigation">
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
    <div class="navbar-menu whitebarbug" :class="{'is-active': isActive}">
      <div class="navbar-start">
        <router-link @click.native="isActive = false" class="navbar-item" to="/blog">News</router-link>
        <hr class="navbar-divider">
        <router-link @click.native="isActive = false" class="navbar-item" to="/events">Events</router-link>
        <hr class="navbar-divider">
          <router-link @click.native="isActive = false" class="navbar-item" to="/jobs">Jobs</router-link>
          <hr class="navbar-divider">
        <router-link
          @click.native="isActive = false"
          class="navbar-item"
          to="/sponsors"
        >Sponsors</router-link>
        <hr class="navbar-divider">
        <router-link @click.native="isActive = false" class="navbar-item" to="/admin/dash">Admin</router-link>
        <hr class="navbar-divider">
      </div>
      <template class="navbar-end" v-if="isLoggedIn == false">
        <router-link
          @click.native="isActive = false"
          class="navbar-item"
          to="/account/register"
        >Register</router-link>

          <b-dropdown position="is-bottom-left" aria-role="menu" trap-focus>
              <a
                  class="navbar-item"
                  slot="trigger"
                  role="button">
                  <span>Login</span>
                  <b-icon icon="menu-down"></b-icon>
              </a>

              <b-dropdown-item
                  aria-role="menu-item"
                  :focusable="false"
                  custom
                  paddingless>
                  <form action="">
                      <div class="modal-card" style="width:300px;">
                          <section class="modal-card-body">
                              <b-field label="Email">
                                  <b-input
                                      type="email"
                                      placeholder="Your email"
                                      required>
                                  </b-input>
                              </b-field>

                              <b-field label="Password">
                                  <b-input
                                      type="password"
                                      password-reveal
                                      placeholder="Your password"
                                      required>
                                  </b-input>
                              </b-field>

                              <b-checkbox>Remember me</b-checkbox>
                          </section>
                          <footer class="modal-card-foot">
                              <button class="button is-primary">Login</button>
                          </footer>
                      </div>
                  </form>
              </b-dropdown-item>
          </b-dropdown>
      </template>
      <template class="navbar-end" v-else>
        <router-link
          @click.native="isActive = false"
          class="navbar-item"
          to="/account/info"
        >Your account</router-link>
        <router-link @click.native="logout" class="navbar-item" to="/">Logout</router-link>
      </template>
    </div>
  </nav>
</template>


<script>
import axios from"@/axios.js";
export default {
  data: () => ({
    isActive: false,
    isLoggedIn: false
  }),
  created() {
    console.log("creatededed");
    var token = localStorage.getItem("token");
    if (token == null) {
      console.log("no token");
      this.isLoggedIn = false;
      return;
    }
    var self = this;
    axios
      .get("/api/token/valid/" + token, {})
      .then(function(response) {
        console.log("is logged in");
        self.isLoggedIn = true;
        self.$forceUpdate();
      })
      .catch(function(error) {
        console.log(error.response);
        localStorage.removeItem("token");
        self.isLoggedIn = false;
        self.$router.push("/account/login");
      });
  },
  methods: {
    logout() {
      localStorage.removeItem("token");
      this.isLoggedIn = false;
      this.$router.push("/");
      this.$forceUpdate();
      this.$session.destroy();
    }
  }
};
</script>

<style lang="scss">
.navbar {
  width: auto;
  border-bottom: 1px solid black;
  color: #0085ff;
}

.whitebarbug {
  background-color: #0085ff;
}
</style>
