<template>
  <div class="seatmap">
    <div>
      <p>{{seatmapmodel.id}}</p>
      <p>{{seatmapmodel.numseats}}</p>
      <p>{{seatmapmodel.numseatsavailable}}</p>
    </div>

    <div id="grid">
      <div v-for="seat in seats" v-bind:key="seat">
        <Seat v-bind:seat="seat"></Seat>
      </div>
    </div>
  </div>
</template>

<script>
import axios from"@/axios.js";
import Vue from "vue";
import Seat from "@/components/SeatMap/Seat.vue";
export default {
  components: {
    Seat: Seat
  },
  data: function() {
    return {
      seats: [],
      seatmapmodel: {}
    };
  },
  created() {
    var id = this.$route.params.id;
    var self = this;
    var token = localStorage.getItem("token");
    var config = {
      headers: { Authorization: "bearer " + token }
    };
    axios
      .get("/api/seatmap/" + id,config)
      .then(function(response) {
        self.seats = response.data.seats;
        self.seatmapmodel = response.data.seatMapModel;
        console.log(response.data);
        console.log(self.seatmapmodel);
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

<style>
#grid {
    display: flex;
    justify-content: center;
    align-items: center;
  background-image: url("https://i.imgur.com/6BryTTm.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  width: 900px;
  height: 900px;
}
</style>
