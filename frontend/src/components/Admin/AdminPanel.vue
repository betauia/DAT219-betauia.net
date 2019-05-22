<template>
  <div id="adminPanel">
    <div id="adminSidebar">
      <div class="panel">
        <hr class="navbar-divider">
        <router-link @click.native="isActive = false" class="navbar-item" to="/admin/users">Users</router-link>
      </div>
      <div class="panel">
        <p>Administer users</p>
      </div>
    </div>
  </div>
</template>

<style>
#adminSidebar {
  background-color: #737477;
  width: 20%;
}
</style>


<script>
import axios from "axios";

export default {
  data() {
    return {
      role: String
    };
  },
  created() {
    var token = localStorage.getItem("token");
    console.log(token);
    var self = this;
    axios
      .post("/api/token/role/" + token, {})
      .then(function(response) {
        var roles = response["data"];
        if (roles == null || roles == "User") {
          //window.location.href = "/";
          console.log("No access");
          self.$router.push("/");
        }
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>