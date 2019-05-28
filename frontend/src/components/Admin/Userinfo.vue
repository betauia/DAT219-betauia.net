<template>
  <div class="User padding center">

<div class="field">
  <label class="label" for="username">Username</label>
  <div class="control">
    <input id="username" name="username" type="text" class="input "  v-model="user.userName">
    
  </div>
</div>

<!-- Text input-->
<div class="field">
  <label class="label" for="email">Email</label>
  <div class="control">
    <input id="email" name="email" type="text" class="input "  v-model="user.email">
    
  </div>
</div>

<!-- Text input-->
<div class="field">
  <label class="label" for="firstname">Firstname</label>
  <div class="control">
    <input id="firstname" name="firstname" type="text" class="input " v-model="user.firstName">
    
  </div>
</div>

<!-- Text input-->
<div class="field">
  <label class="label" for="lastname">Lastname</label>
  <div class="control">
    <input id="lastname" name="lastname" type="text" class="input " v-model="user.lastName">
    
  </div>
</div>

<!-- Text input-->
<div class="field">
  <label class="label" for="id">ID</label>
  <div class="control">
    <input id="id" name="id" type="text" class="input " v-model="user.id" disabled>
    
  </div>
</div>

<!-- Select Basic -->
<div class="field">
  <label class="label" for="active">Active Account</label>
  <div class="control">
  	<div class="select">
	    <select id="active" name="active" class="" v-model="user.active">
	      <option>true</option>
	      <option>false</option>
	    </select>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="field">
  <label class="label" for="verifiedemail">Verified Email</label>
  <div class="control">
  	<div class="select">
	    <select id="verifiedemail" name="verifiedemail" class="" v-model="user.verifiedEmail">
	      <option>true</option>
	      <option>false</option>
	    </select>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="field">
  <label class="label" for="forcelogout">Force Logout</label>
  <div class="control">
  	<div class="select">
	    <select id="forcelogout" name="forcelogout" class="" v-model="user.forceLogout">
	      <option>true</option>
	      <option>false</option>
	    </select>
	</div>
  </div>
</div>

    <button
      v-on:click="updateUser"
      id="savebutton"
      name="savebutton"
      class="button is-info"
    >Save Changes</button>
</div>
</template>

<script>
import axios from "axios";

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
      var active = document.querySelector("select[name=active]").value;
      var verifiedemail = document.querySelector("select[name=verifiedemail]")
        .value;
      var forcelogout = document.querySelector("select[name=forcelogout]").value
      var bodyParamters = {
        username: username,
        email: email,
        firstname: firstname,
        lastname: lastname,
        id: id,
        active: active,
        verifiedemail: verifiedemail,
        forcelogout: forcelogout
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
    }
  }
};
</script>

<style>

</style>