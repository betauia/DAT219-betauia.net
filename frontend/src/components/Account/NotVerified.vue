<template>
    <div class="columns is-mobile is-multiline is-centered">
        <div class="column is-three-quarters is-offset-is-narrow">
            <div>
                <div class="card padding">
                    <header>
                        <p class="card-header-title">Your account is not verified</p>
                    </header>
                    <div>
                        <div class="content">
                            Please verify your account by confirming your email.
                            <br>
                            <b-button @click="resendEmail" class="is-primary">Resend confirmation email.</b-button>
                        </div>
                        <div v-if="message!=null">
                            {{message}}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'NotVerified',
    data(){
      return{
        message:null,
      }
    },
    methods:{
      async resendEmail(){
        var token = localStorage.getItem("token");
        var config = {
          headers: { Authorization: "bearer " + token }
        };
        const self = this;
        axios
          .get("/api/getemailverification",config)
          .then(function (response) {
            self.message = "Verification email was sent"
          })
          .catch(function (error) {
            self.message = "An error occured"
          })
      }
    }
  };
</script>

<style scoped>

</style>
