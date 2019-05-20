<template>
  <section class="padding center">
    <b-field label="Email" type="is-danger" message="This email is invalid">
      <b-input
        type="email"
        :value="username"
        name="username"
        placeholder="user@gmail.com"
        maxlength="30"
      ></b-input>
    </b-field>

    <b-field
      label="Password"
      type="is-danger"
      :message="['Password is too short', 'Password must have at least 8 characters']"
    >
      <b-input
        :value="password"
        name="password"
        type="password"
        placeholder="Password1."
        maxlength="120"
      ></b-input>
    </b-field>

    <div class="field" grouped>
      <button v-on:click="login" class="button is-primary">Login</button>
      <b-checkbox>Remember Me!</b-checkbox>
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

      axios
        .post("/api/account/login", {
          username: user,
          password: password
        })
        .then(function(response) {
          //console.log(response["data"]);
          //console.log(JSON.stringify(response));
          localStorage.setItem("token", response["data"]);
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>
