<template>
  <div class="AdminSide padding">
    <div class="columns">
      <div class="column is-narrow">
        <aside class="menu">
          <p class="menu-label">General</p>
          <ul class="menu-list">
            <li>
              <router-link to="/admin/dashboard">Dashboard</router-link>
            </li>
            <li>
              <router-link to="/blog/add">Add Post</router-link>
            </li>
            <li>
              <router-link to="/event/add">Add Event</router-link>
            </li>
            <li>
              <router-link to="/admin/seatmapeditor">Edit Seatmap</router-link>
            </li>
            <li>
              <router-link to="/admin/addsponsor">Add sponsor</router-link>
            </li>
          </ul>
          <p class="menu-label">Administration</p>
          <ul class="menu-list">
            <li>
              <router-link to="/admin/users">User Management</router-link>
            </li>
            <li>
              <router-link to="/admin/roles">Role Management</router-link>
            </li>
          </ul>
        </aside>
      </div>
      <div class="column">
        <router-view/>
      </div>
    </div>
  </div>
</template>

<style>
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
    if (token == null) {
      alert("No access");
      this.$router.push("/");
      return;
    }
    console.log(token);
    var self = this;
    axios
      .post("/api/token/role/" + token, {})
      .then(function(response) {
        var roles = response["data"];
        if (roles == null || roles == "User" || roles == "") {
          //window.location.href = "/";
          alert("No access");
          self.$router.push("/");
        }
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>