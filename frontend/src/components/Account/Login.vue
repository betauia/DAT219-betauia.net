<template>
    <b-dropdown position="is-bottom-left" aria-role="menu" trap-focus>
        <a
            class="navbar-item"
            slot="trigger"
            role="button">
            <span>Login</span>
            <b-icon icon="menu-down"></b-icon>
        </a>

        <b-dropdown-item
            aria-role="menu-item"
            :focusable="false"
            custom
            paddingless>
            <form action="">
                <div class="modal-card" style="width:300px;">
                    <div>
                        <section class="modal-card-body">
                            <b-field label="Email">
                                <b-input
                                    id="email"
                                    type="email"
                                    placeholder="Your email"
                                    v-model="loginProps.username"
                                    name="email"
                                    required>
                                </b-input>
                            </b-field>

                            <b-field label="Password">
                                <b-input
                                    id="password"
                                    type="password"
                                    name="password"
                                    v-model="loginProps.password"
                                    password-reveal
                                    placeholder="Your password"
                                    required>
                                </b-input>
                            </b-field>

                            <b-checkbox>Remember me</b-checkbox>

                        </section>
                        <footer class="modal-card-foot level">
                            <b-button class="button is-primary" v-on:click="login">Login</b-button>
                            <b-button class="" v-on:click="forgotPasswordClick" >Forgot Password?</b-button>
                        </footer>

                        <section v-if="forgotPassword">
                            <section class="modal-card-body">
                                <b-field label="Email">
                                    <b-input
                                        type="email"
                                        placeholder="Account email"
                                        v-model="forgotProps.email"
                                        required>
                                    </b-input>
                                </b-field>
                            </section>

                            <footer class="modal-card-foot">
                                <b-button class="is-primary" v-on:click="resetPassword">Reset Password</b-button>
                            </footer>
                        </section>
                    </div>
                </div>
            </form>
        </b-dropdown-item>
    </b-dropdown>
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
                loginProps: {
                    username: "",
                    password: ""
                },
                forgotPassword: false,
                LoggedIn: false,
                forgotProps: {
                    email:"",
                    message:"",
                },
            };
        },
        methods: {
            login() {
                var self = this;
                var body ={
                  username: this.loginProps.username,
                  password: this.loginProps.password,
                };
                console.log(body)

                axios
                    .post("/api/account/login",body,{})
                    .then(function(response) {
                        console.log("Logged in");
                        console.log(response["data"]);
                        //console.log(JSON.stringify(response));
                        localStorage.setItem("token", response["data"]);
                        location.reload();
                        self.$forceUpdate();
                    })
                    .catch(function(error) {
                        console.log(error);
                    });
                self.$forceUpdate();
            },
            forgotPasswordClick(){
                this.forgotPassword = !this.forgotPassword;
            },
            resetPassword(){
                if(this.forgotProps.email == ""){
                    alert("Please type in your email");
                }

                var bodyParam ={
                    email:this.forgotProps.email,
                };
                const self = this;
                axios
                    .post("/api/resetpassword/get",bodyParam)
                    .then(function (response) {
                        console.log(response.data);
                        self.forgotProps.message = "An email was sent with instruction."
                    })
                    .catch(function (error) {
                        console.log(error.response)
                        self.forgotProps.message = "An error occurred. Please try again later."
                    })
            }
        }
    };
</script>
