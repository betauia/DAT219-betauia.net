<template>
  <div class="seatinfo">
    <p></p>
  </div>
</template>

<script>
import axios from"@/axios.js";
export default {
  data: function() {
    return {
      seatid: String,
      seatmapid: String,
      seat: Object
    };
  },
  created() {
    this.seatid = this.$route.params.seatid;
    this.seatmapid = this.$route.params.id;
    var token = localStorage.getItem("token");

    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var self = this;
    axios
      .get("/api/seat/" + this.seatmapid + this.seatid, config)
      .then(function(response) {
        self.seat = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

<style>
</style>
