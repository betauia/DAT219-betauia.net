<template>
  <div class="Userinfo">
    <div class="columns">
      <div class="column is-narrow">
        <aside class="meun">
          <p class="menu-label">User</p>
          <ul class="menu-list">
            <li>
              <router-link to="/account/info">Account info</router-link>
            </li>
            <li>
              <router-link to="/account/edit/${user.id}">Edit</router-link>
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

<style lang="sass">

</style>

<script>
import axios from "axios";

export default {
  data() {
    return {
      user: [],
      errors: []
    };
  },
  created() {
    var token = localStorage.getItem("token");

    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var bodyParamters = {
      token: token
    };
    var self = this;
    console.log(localStorage.getItem("token"));
    axios
      .post("/api/account/get", bodyParamters, config)
      .then(function(response) {
        //console.log(response["data"]);
        self.user = response["data"];
        console.log(self.user);
      })
      .catch(function(error) {
        console.log(error.response.data);
      });
  }
};
</script>
