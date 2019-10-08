<template>
  <section class="padding center">
    <b-field label="Firstname">
      <b-input :value="firstname" name="registerfirstname"></b-input>
    </b-field>

    <b-field label="Lastname">
      <b-input :value="lastname" name="registerlastname"></b-input>
    </b-field>

    <b-field label="Email">
      <b-input
        type="email"
        :value="username"
        name="registeremail"
      ></b-input>
    </b-field>

    <b-field label="Username">
      <b-input
        type="username"
        :value="username"
        name="registerusername"
      ></b-input>
    </b-field>

    <b-field
      label="Password"
    >
      <b-input
        :value="password"
        type="password"
        name="registerpassword"
      ></b-input>
    </b-field>

    <b-field
      label="Password Again"
    >
      <b-input
        :value="again_password"
        type="password"
        name="registeragain_password"
      ></b-input>
    </b-field>

    <div class="is-3 padding">Insert Recaptcha here for auth of humanoids</div>

    <p class="control">
      <button v-on:click="register" class="button is-primary">Register</button>
    </p>
  </section>
</template>

<script>
import axios from"@/axios.js";
import { constants } from "crypto";

export default {
  data() {
    return {};
  },
  methods: {
    register() {
      var username = document.querySelector("input[name=registerusername]").value;
      var firstname = document.querySelector("input[name=registerfirstname]").value;
      var lastname = document.querySelector("input[name=registerlastname]").value;
      var email = document.querySelector("input[name=registeremail]").value;
      var password = document.querySelector("input[name=registerpassword]").value;
      var confirmpassword = document.querySelector("input[name=registeragain_password]")
        .value;

      var self = this;
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
                localStorage.setItem("token", response["data"]);
                var config = {
                  headers: { Authorization: "bearer " + response["data"] }
                };
                    axios
                      .get("/api/getemailverification",config)
                      .then(function(response) {
                      })
                      .catch(function(error) {
                        console.log(error);
                      });
                self.$router.push({ path: "/account/registered" }, () => {
                location.reload();
          });
        })
        .catch(function(error) {
          console.log(error.response);
        });
    }
  }
};
</script>
