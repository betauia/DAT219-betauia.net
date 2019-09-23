<template>
  <div class="user">
    <div class="Userinfo columns padding center">
      <div class="content">
        <ul>
          <li>Username: {{user.userName}}</li>
          <li>Firstname: {{user.firstName}}</li>
          <li>Lastname: {{user.lastName}}</li>
          <li>Email: {{user.email}}</li>
          <li>ID: {{user.id}}</li>
        </ul>
        <br>
      </div>
    </div>
  </div>
</template>

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
    const body ={}
    var self = this;
    axios
      .post("/api/account/get",body,config)
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

<style>
</style>
