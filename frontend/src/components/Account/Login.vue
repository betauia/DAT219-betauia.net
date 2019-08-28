<template>
  <section class="padding center">
    <b-field label="Email">
      <b-input
        type="email"
        :value="username"
        name="username"
      ></b-input>
    </b-field>

    <b-field
      label="Password">
      <b-input
        :value="password"
        name="password"
        type="password"
      ></b-input>
    </b-field>

    <div class="field" grouped>
      <button v-on:click="login" class="button is-primary">Login</button>
    </div>
  </section>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      input: {
        username: "",
        password: ""
      }
    };
  },
  methods: {
    login() {
      var user = document.querySelector("input[name=username]").value;
      var password = document.querySelector("input[name=password]").value;
      var jsond = new Object();
      jsond["username"] = user;
      jsond["password"] = password;
      var jsondata = JSON.stringify(jsond);

      var self = this;
      axios
        .post("/api/account/login", {
          username: user,
          password: password
        })
        .then(function(response) {
          console.log(response["data"]);
          //console.log(JSON.stringify(response));
          localStorage.setItem("token", response["data"]);
          self.$router.push({ path: "/" }, () => {
            location.reload();
          });
          self.$$forceUpdate();
        })
        .catch(function(error) {
          console.log(error.response.data);
        });
    }
  }
};
</script>
