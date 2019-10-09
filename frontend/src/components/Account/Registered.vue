<template>
    <div class="column is-6 is-offset-3">
        <div class="box" id="Registered">
            <p>A verification email was sent to you.</p>
            <p>Please verify your email</p>
            <p>Please check your spam mail</p>
            <b-button class="is-primary " @click="resendEmail">Resend verification email</b-button>
            <p>{{message}}</p>
        </div>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'Registered',
    data(){
      return{
        message:""
      }
    },
    methods: {
      resendEmail(){
        var token = localStorage.getItem("token");
        var config = {
          headers: { Authorization: "bearer " + token }
        };
        const self = this;
        axios
          .get("/api/getemailverification",config)
          .then(function(response) {
            self.message = "Successful";
          })
          .catch(function(error) {
            self.message = "An error occured, please contact support";
          });
      }
    }
  };

</script>

<style scoped>

</style>
