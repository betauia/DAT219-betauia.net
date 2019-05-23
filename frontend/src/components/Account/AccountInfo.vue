<<<<<<< HEAD
<template>
  <div class="Userinfo">
    <div class="Userinfo columns padding center">
      <div class="content">
        <ul>
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

=======
<template>
  <div class="Userinfo">
    <p>{{user.firstName}}</p>
    <p>{{user.lastName}}</p>
    <p>{{user.email}}</p>
    <p>{{user.id}}</p>
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

>>>>>>> fd238d6908ec779e8eb6fed2d043c5cc2320fdfb
