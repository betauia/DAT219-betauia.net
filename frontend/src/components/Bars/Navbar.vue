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
    <div class="navbar-menu whitebarbug" :class="{'is-active': isActive}">
      <div class="navbar-start">
        <router-link @click.native="isActive = false" class="navbar-item" to="/blog">News</router-link>
        <hr class="navbar-divider">
        <router-link @click.native="isActive = false" class="navbar-item" to="/events">Events</router-link>
        <hr class="navbar-divider">
        <router-link
          @click.native="isActive = false"
          class="navbar-item"
          to="/sponsors"
        >Sponsors</router-link>
        <hr class="navbar-divider">
        <router-link @click.native="isActive = false" class="navbar-item" to="/admin/dashboard">Admin</router-link>
        <hr class="navbar-divider">
      </div>
      
       <template class="navbar-end"  v-if="isLoggedIn == false">
      <!--div class="navbar-end" v-if="isLoggedIn == false"-->
        <router-link @click.native="isActive = false" class="navbar-item" to="/account/register">Register</router-link>
        <router-link @click.native="isActive = false" class="navbar-item" to="/account/login">Login</router-link>
        <!--/div-->
      </template>
      
      <template v-else>
      <!--div class="navbar-end" v-else-->
        <router-link @click.native="isActive = false" class="navbar-item" to="/account/info">Your account</router-link>
        <router-link @click.native="logout" class="navbar-item" to="/">Logout</router-link>
      <!--/div-->
      </template>
    </div>
  </nav>
</template>


<script>
import axios from "axios";
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
      .post("/api/token/valid/" + token, {})
      .then(function(response) {
        console.log("is logged in");
        self.isLoggedIn = true;
        self.$forceUpdate();
      })
      .catch(function(error) {
        console.log(error);
        self.isLoggedIn = false;
        self.$router.push("/account/login");
        return;
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
