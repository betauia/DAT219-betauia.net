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
      <nav class="navbar-end" v-if="!isLoggedIn">

          <hr class="navbar-divider">
          <div
              class="navbar-item">
              <Register></Register>
          </div>
          <hr class="navbar-divider">
          <b-dropdown position="is-bottom-left" aria-role="menu" trap-focus>
              <a
                  class="navbar-item"
                  slot="trigger"
                  role="button">
                  <span>Login</span>
                  <b-icon icon="menu-down"></b-icon>
              </a>
              <Login></Login>
          </b-dropdown>
      </nav>
      <nav class="navbar-end" v-else>
        <!--<router-link
          @click.native="isActive = false"
          class="navbar-item"
          to="/account/info"
        >Your account</router-link>-->
        <AccountDropdown></AccountDropdown>
          <!--
        <router-link @click.native="logout" class="navbar-item" to="/">Logout</router-link>
        -->
      </nav>
    </div>
  </nav>
</template>


<script>
import EventBus from '@/eventBus.js'
import axios from"@/axios.js";
import Login from "../Account/Login";
import AccountDropdown from "../Account/AccountDropdown";
import Register from "../Account/Register";

export default {
    components: {
        AccountDropdown,
        Login,
        Register,
    },
  data: () => ({
    isActive: false,
    isLoggedIn: false,
  }),
  created() {
      var self = this;
      console.log("creatededed");
    var token = localStorage.getItem("token");
    if (token == null) {
      console.log("no token");
      self.isLoggedIn = false;
      return;
    }
    console.log("logged in status: " + self.isLoggedIn);

    axios
      .get("/api/token/valid/" + token, {})
      .then(function(response) {
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
  mounted(){
    EventBus.$on('LOGGED_IN',(payload)=>{
      this.isLoggedIn = payload;
    })
  },
  methods: {
    logout() {
      localStorage.removeItem("token");
      this.isLoggedIn = false;
      console.log("Logged out");
      this.$forceUpdate();
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
