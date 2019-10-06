<template>
    <div class="verifyeventemail">
        <p>{{message}}</p>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'ConfirmEventEmail',
    data(){
      return{
        message: ""
      };
    },
    created() {
      var token = this.$route.params.id;
      const self = this;
      axios
        .get("/api/eventsignup/confirmeventemail/" + token)
        .then(function(response) {
          self.message = "Successfully signed up for event";
        })
        .catch(function(error) {
          self.message = "An error occured, lease contact support";
        });
    }
  };
</script>

<style scoped>

</style>
