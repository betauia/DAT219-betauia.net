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
              <li>
                  <router-link to="/account/accountorders">Orders</router-link>
              </li>
              <li>
                  <button @click="resetPassword">Reset password</button>
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
import axios from"@/axios.js";export default {
  data() {
    return {
      user: [],
      errors: [],
      passwordReset: "",
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
  },
  methods:{
    resetPassword(){
        const self = this;
          var token = localStorage.getItem("token");

          var config = {
            headers: { Authorization: "bearer " + token }
          };
        axios
          .get('/api/resetpassword/get',config)
          .then(function (response) {
            self.passwordReset = "An email was sent with instruction for resetting you password";
          })
          .catch(function (error) {
            console.log(error.response);
            self.passwordReset = "An error occurred, please try again later or contact support"
          });
      alert(self.passwordReset);
    }
  }
};
</script>
