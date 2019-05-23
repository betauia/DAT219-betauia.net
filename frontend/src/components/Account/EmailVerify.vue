<template>
  <div class="verifiedEmail">
    <p>{{message}}</p>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      message: String
    };
  },
  created() {
    var token = this.$route.params.id;
    axios
      .get("/api/verifyemail/" + token)
      .then(function(response) {
        this.message = "Successful";
      })
      .catch(function(error) {
        this.message = "An error occured";
      });
  }
};
</script>
