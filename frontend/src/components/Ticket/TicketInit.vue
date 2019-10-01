<template>

</template>

<script>
import axios from 'axios';
  export default {
    name: 'TicketInit',
    created() {
      const id = this.$route.params.id;
      const self = this;
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };

      axios
        .
    }
  };
</script>

<style scoped>

</style>
