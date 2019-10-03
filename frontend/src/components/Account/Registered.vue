<template>
    <div id="Registered">
        <p>A verification email was sent to you.</p>
        <p>Please verify your email</p>
        <p>Please check your spam mail</p>
        <button @click="resendEmail">Resend verification email</button>
        <p>{{message}}</p>
    </div>
</template>

<script>
  import axios from "axios";

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
