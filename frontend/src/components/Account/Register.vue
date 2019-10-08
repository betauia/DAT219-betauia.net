<template>
      <span class="navbar-item">
          <span v-on:click="isRegisterModalActive=true">
            <a>Register</a>
          </span>
          <b-modal
              class="is-centered"
              :active.sync="isRegisterModalActive"
              full-screen
              has-modal-card
              :can-cancel="true">

                          <div class="modal-card" style="width: auto">
                              <header class="modal-card-head">
                                  <p class="modal-card-title">Register</p>
                              </header>
                              <section class="modal-card-body">
                                  <b-field label="Firstname">
                                      <b-input
                                          v-model="registerProps.firstname"
                                          name="registerfirstname"
                                          required>
                                      </b-input>
                                  </b-field>

                                  <b-field label="Lastname">
                                      <b-input
                                          :value="lastname"
                                          name="registerlastname"
                                          required></b-input>
                                  </b-field>

                                  <b-field label="Username">
                                      <b-input
                                          type="username"
                                          :value="username"
                                          name="registerusername"
                                          required
                                      ></b-input>
                                  </b-field>

                                  <b-field label="Email">
                                      <b-input
                                          type="email"
                                          :value="email"
                                          placeholder="Your email"
                                          name="registeremail"
                                          required>
                                      </b-input>
                                  </b-field>

                                  <b-field label="Password">
                                      <b-input
                                          type="password"
                                          :value="password"
                                          password-reveal
                                          placeholder="Your password"
                                          name="registerpassword"
                                          required>
                                      </b-input>
                                  </b-field>

                                  <b-field label="Password Again">
                                      <b-input
                                          :value="again_password"
                                          type="password"
                                          name="registeragain_password"
                                          required
                                      ></b-input>
                                  </b-field>

                                  <div class="is-3 padding">Insert Recaptcha here for auth of humanoids</div>

                              </section>
                              <footer class="modal-card-foot">
                                  <button class="button" type="button" @click="isRegisterModalActive=false">Close</button>
                                  <button class="button is-primary" v-on:click="register">Register</button>
                              </footer>
                          </div>
          </b-modal>
      </span>
</template>

<script>
import axios from"@/axios.js";
import { constants } from "crypto";

export default {
    name: 'register',
  data() {
    return {
        isRegisterModalActive: false,
        registerProps: {
            firstname: '',
            lastname: '',
            username: '',
            email: '',
            password: '',
            password_again: '',
        }
    };
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
