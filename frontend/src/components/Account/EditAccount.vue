<template>
  <div class="UserEdit center">
    <div class="Input">
      <label>Username:</label>
      <br>
      <input type="text" name="username" v-model="user.userName">
    </div>
    <div class="Input">
      <label>Email:</label>
      <br>
      <input type="text" name="email" v-model="user.email">
    </div>
    <div class="Input">
      <label>Firstname:</label>
      <br>
      <input type="text" name="firstname" v-model="user.firstName">
    </div>
    <div class="Input">
      <label>Lastname:</label>
      <br>
      <input type="text" name="lastname" v-model="user.lastName">
    </div>
    <div class="center">
      <span>
        <button
        v-on:click="updateUser"
        id="savebutton"
        name="savebutton"
        class="button is-info"
      >Save changes</button>
        </span>
      <span>
        <button
        v-on:click="deleteUser"
        id="savebutton"
        name="deletebutton"
        class="button is-danger"
      >Delete Account</button></span>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      user: Object
    };
  },
  methods: {
    updateUser() {
      var token = localStorage.getItem("token");

      var username = document.querySelector("input[name=username]").value;
      var email = document.querySelector("input[name=email]").value;
      var firstname = document.querySelector("input[name=firstname]").value;
      var lastname = document.querySelector("input[name=lastname]").value;
      var profile = {};
      profile["id"] = this.user.id;
      profile["username"] = username;
      profile["email"] = email;
      profile["firstname"] = firstname;
      profile["lastname"] = lastname;
      var config = {
        headers: { Authorization: "bearer " + token }
      };
      var bodyParamters = {
        id:this.user.id,
        username:username,
        email:email,
        firstname:firstname,
        lastname:lastname
      };
      console.log(bodyParamters);
      var self = this;
      axios
        .put("/api/account/edit", bodyParamters, config)
        .then(function(response) {
          self.user = response.data;
          self.$router.push("/account/info");
          console.log(response.data);
        })
        .catch(function(error) {
          var code = error.response.data;
          console.log(code);
          if (code == "202") {
            alert("Username already taken");
          }
          if (code == "201") {
            alert("Email already taken");
          }
          if (code == "204") {
            alert("Not a valid email");
          }
        });

      self.$forceUpdate();
    },
    deleteUser() {
      axios
        .delete("/api/account/delete")
    }
  },
  created() {
    var token = localStorage.getItem("token");

    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var bodyParamters = {};
    var self = this;
    axios
      .post("/api/account/get", bodyParamters, config)
      .then(function(response) {
        console.log(response.data);
        self.user = response.data;
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

<style>
</style>
