<template>
  <div id="viewSeatMap">
    <div id="grid">
      <div v-for="seat in seats" v-bind:key="seat">
        <Seat v-bind:seat="seat"></Seat>
      </div>
    </div>

  </div>
</template>

<script>
import axios from "axios";
import Seat from "@/components/Events/Seat.vue";

export default {
  components:{
    Seat: Seat
  },
  data: function () {
    return{
      seats:[],
      seatmapmodel:{},
    }
  },
  created() {
    var mapid = this.$route.params.id;
    var token = localStorage.getItem("token");
    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var self = this;
    axios
      .get("/api/seatmap/" + mapid, config)
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
}
</script>

<style>
  #grid {
    background-image: url("https://i.imgur.com/6BryTTm.jpg");
    background-repeat: no-repeat;
    background-size: cover;
    width: 900px;
    height: 900px;
  }
</style>
