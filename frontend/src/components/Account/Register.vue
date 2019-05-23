<template>
  <section class="padding center">
    <b-field label="Firstname">
      <b-input :value="firstname" name="firstname" placeholder="kevin"></b-input>
    </b-field>

    <b-field label="Lastname">
      <b-input :value="lastname" name="lastname" placeholder="bacon"></b-input>
    </b-field>

    <b-field label="Email" type="is-danger" message="This email is invalid">
      <b-input
        type="email"
        :value="username"
        name="email"
        placeholder="kevin@bacon.com"
        maxlength="30"
      ></b-input>
    </b-field>

    <b-field label="Username" type="is-danger" message="This Username is invalid">
      <b-input
        type="username"
        :value="username"
        name="username"
        placeholder="mrbacon"
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
        type="password"
        name="password"
        placeholder="bacon15mylyf3"
        maxlength="120"
      ></b-input>
    </b-field>

    <b-field
      label="Password Again"
      type="is-danger"
      :message="['Password is too short', 'Password must have at least 8 characters']"
    >
      <b-input
        :value="again_password"
        type="password"
        name="again_password"
        placeholder="bacon15mylyf3"
        maxlength="120"
      ></b-input>
    </b-field>

    <div class="is-3 padding">Insert Recaptcha here for auth of humanoids</div>

    <p class="control">
      <button v-on:click="register" class="button is-primary">Register</button>
    </p>
  </section>
</template>

<script>
import axios from "axios";
import { constants } from "crypto";

export default {
  data() {
    return {};
  },
  methods: {
    register() {
      var username = document.querySelector("input[name=username]").value;
      var firstname = document.querySelector("input[name=firstname]").value;
      var lastname = document.querySelector("input[name=lastname]").value;
      var email = document.querySelector("input[name=email]").value;
      var password = document.querySelector("input[name=password]").value;
      var confirmpassword = document.querySelector("input[name=again_password]")
        .value;

      alert(username);
      axios
        .post("/api/account/register", {
          username: username,
          firstname: firstname,
          lastname: lastname,
          email: email,
          password: password,
          confirmpassword: confirmpassword
        })
        .then(function(response) {
          console.log(response["data"]);
          console.log(response);
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>
