<template>
    <div id="passwordreset">
        <b-field label="Password">
            <b-input
                :value="password"
                type="password"
                name="password"
            ></b-input>
        </b-field>

        <b-field label="Password Again">
            <b-input
                :value="again_password"
                type="password"
                name="again_password"
            ></b-input>
        </b-field>
        <button @click="resetPassword">Reset password</button>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'ResetPassword',
    methods:{
      resetPassword(){
        var password = document.querySelector("input[name=password]").value;
        var confirmpassword = document.querySelector("input[name=again_password]").value;

        const id = this.$route.params.id;

        var bodyParamters = {
          token:id,
          password: password,
          againpassword: confirmpassword
        };

        var self = this;
        axios
          .post('/api/resetpassword/post',bodyParamters)
          .then(function (response) {
            console.log(response.data);
            localStorage.removeItem("token");
            self.isLoggedIn = false;
            window.location.href = "/account/login"
          })
          .catch(function (errors) {
            console.log(errors.response.data)
          })
      }
    }
  };
</script>

<style scoped>

</style>
