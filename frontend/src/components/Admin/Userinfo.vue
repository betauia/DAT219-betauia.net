<template>
  <div class="User">
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
    <div class="Input">
      <label>Id:</label>
      <br>
      <input type="text" name="id" v-model="user.id" disabled>
    </div>
    <div class="Input">
      <label>Banned account:</label>
      <br>
      <input type="text" name="banned" v-model="user.banned">
    </div>
    <div class="Input">
      <label>VerifiedEmail:</label>
      <br>
      <input type="text" name="verifiedemail" v-model="user.verifiedEmail">
    </div>
    <button
      v-on:click="updateUser"
      id="savebutton"
      name="savebutton"
      class="button is-success"
    >Save Changes</button>
    <button
      v-on:click="deleteUser"
      id="savebutton"
      name="savebutton"
      class="button is-danger"
    >Delete User</button>
    <button
        v-on:click="banUser"
        id="savebutton"
        name="savebutton"
        class="button is-danger"
        >Ban user</button>

  </div>
</template>

<script>
import axios from"@/axios.js";

export default {
  name: "User",
  data() {
    return {
      dUser: Object
    };
  },
  props: {
    user: Object
  },
  methods: {
    updateUser() {
      var self = this;
      var token = localStorage.getItem("token");

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var username = document.querySelector("input[name=username]").value;
      var email = document.querySelector("input[name=email]").value;
      var firstname = document.querySelector("input[name=firstname]").value;
      var lastname = document.querySelector("input[name=lastname]").value;
      var id = document.querySelector("input[name=id]").value;
      var banned = document.querySelector("input[name=banned]").value;
      var verifiedemail = document.querySelector("input[name=verifiedemail]").value;

      var bodyParamters = {
        username: username,
        email: email,
        firstname: firstname,
        lastname: lastname,
        id: id,
        verifiedemail: verifiedemail,
        banned: banned
      };

      axios
        .put("/api/user", bodyParamters, config)
        .then(function(response) {
          self.user = response.data;
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
      var self = this;
      var token = localStorage.getItem("token");

      var config = {
        headers: { Authorization: "bearer " + token }
      };
      var id = document.querySelector("input[name=id]").value;

      axios
        .delete("/api/user/delete/"+id,config)
        .then(function(response){
          console.log(response);
        })
        .catch(function(response){
          console.log(response);
        })
      self.$forceUpdate();
    },
    banUser(){
      var self = this;
      var token = localStorage.getItem("token");
      var id = document.querySelector("input[name=id]").value;

      var config = {
        headers: { Authorization: "bearer " + token }
      };
      axios
        .get("/api/user/ban/"+id,config)
        .then(function(response){
          console.log(response);
        })
        .catch(function(response){
          console.log(response);
        })
      self.$forceUpdate();
    }
  },
};
</script>

<style>
.User {
  background-color: rgb(167, 167, 192);
  margin: 12px;
  border-block-color: black;
  border-width: thin;
  border-style: solid;
  color: black;
}
input {
  font-size: 16px;
  width: 100%;
}
.Input {
  margin-left: 10px;
  margin-bottom: 4px;
  border-block-color: black;
  border-width: thin;
  border-style: solid;
  color: black;
  width: 50%;
}
#savebutton {
  margin-left: 10px;
  margin-bottom: 4px;
}
</style>
