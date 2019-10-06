<template>
    <div>
        <section class="modal-card-body">
            <b-field label="Email">
                <b-input
                    type="email"
                    placeholder="Your email"
                    :value="username"
                    name="email"
                    required>
                </b-input>
            </b-field>

            <b-field label="Password">
                <b-input
                    type="password"
                    name="password"
                    password-reveal
                    placeholder="Your password"
                    required>
                </b-input>
            </b-field>

            <b-checkbox>Remember me</b-checkbox>

        </section>
        <footer class="modal-card-foot level">
            <b-button class="button is-primary" v-on:click="login">Login</b-button>
            <b-button class="has-text-black-bis" v-model="forgotPassword">Forgot Password?</b-button>
        </footer>
    </div>
</template>
<!--<template>
  <section class="card">
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

    <div class="field" grouped v-if="forgotPassword==false">
      <button v-on:click="forgotPasswordClick" class="button is-primary">Forgot password</button>
    </div>

      <div class="field" grouped v-if="forgotPassword==true">
          <input type="email" placeholder="Account email" class="input" v-model="email">
          <div v-if="message!=null">{{message}}</div>
          <button v-on:click="resetPassword" class="button is-primary">Send reset password email</button>
      </div>

  </section>
</template>
-->
<script>
import axios from"@/axios.js";
export default {
  data() {
    return {
      input: {
        username: "",
        password: ""
      },
      forgotPassword: false,
      email:"",
      message:"",
    };
  },
  methods: {
    login() {
      var user = document.querySelector("input[name=email]").value;
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
          self.$forceUpdate();
        })
        .catch(function(error) {
          console.log(error.response.data);
        });
    },
    forgotPasswordClick(){
      this.forgotPassword = true;
    },
    resetPassword(){
      if(this.email == ""){
        alert("Please type in your email");
      }

      var bodyParam ={
        email:this.email
      };
      const self = this;
      axios
        .post("/api/resetpassword/get",bodyParam)
        .then(function (response) {
          console.log(response.data);
          self.message = "An email was sent with instruction."
        })
        .catch(function (error) {
          console.log(error.response)
          self.message = "An error occurred. Please try again later."
        })
    }
  }
};
</script>
