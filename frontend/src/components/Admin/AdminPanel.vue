<template>
  <div class="AdminSide padding">
    <div class="columns">
      <div class="column is-narrow-desktop is-full-mobile">
        <aside class="menu">
          <p class="menu-label">General</p>
          <ul class="menu-list">
            <li>
              <router-link to="/admin/dash">Dashboard</router-link>
            </li>
          </ul>
          <p class="menu-label">User Administration</p>
          <ul class="menu-list">
            <li>
              <router-link to="/admin/users">User Management</router-link>
            </li>
            <li>
              <router-link to="/admin/roles">Role Management</router-link>
            </li>
          </ul>
            <p class="menu-label">Seatmap</p>
            <ul class="menu-list">
                <li>
                    <router-link to="/admin/seatmapeditor">Add Seatmap</router-link>
                </li>
                <li>
                    <router-link to="/admin/seatmaps">Edit seatmaps</router-link>
                </li>
            </ul>
            <p class="menu-label">Blog</p>
            <ul class="menu-list">
                <li>
                    <router-link to="/admin/blog/add">Add Post</router-link>
                </li>
                <li>
                    <router-link to="/admin/blog">Edit blog posts</router-link>
                </li>
            </ul>
            <p class="menu-label">Event</p>
            <ul class="menu-list">
                <li>
                    <router-link to="/admin/event/add">Add Event</router-link>
                </li>
                <li>
                    <router-link to="/admin/event">Edit events</router-link>
                </li>
            </ul>
            <p class="menu-label">Sponsor</p>
            <ul class="menu-list">
                <li>
                    <router-link to="/admin/addsponsor">Add sponsor</router-link>
                </li>
                <li>
                    <router-link to="/admin/sponsors">Edit sponsors</router-link>
                </li>
            </ul>
            <p class="menu-label">Job</p>
            <ul class="menu-list">
                <li>
                    <router-link to="/admin/addjob">Add job</router-link>
                </li>
                <li>
                    <router-link to="/admin/jobs">Edit job</router-link>
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
  import axios from "@/axios.js";

export default {
  data() {
    return {
      role: String
    };
  },
  created() {
    var token = localStorage.getItem("token");
    if (token == null) {
      this.$router.push("/account/login");
      return;
    }
    var config = {
      headers: { Authorization: "bearer " + token }
    };
    console.log(token);
    var self = this;
    axios
      .get("/api/token/adminpanel/",config)
      .then(function(response) {
      })
      .catch(function(error) {
      });
  }
};
</script>
