<template>
  <div class="verifiedEmail">
    <p>{{message}}</p>
  </div>
</template>

<script>
import axios from"@/axios.js";

export default {
  data() {
    return {
      message: ""
    };
  },
  created() {
    var token = this.$route.params.id;
    const self = this;
    axios
      .get("/api/verifyemail/" + token)
      .then(function(response) {
        self.message = "Successful";
      })
      .catch(function(error) {
        self.message = "An error occured, lease contact support";
      });
  }
};
</script>
