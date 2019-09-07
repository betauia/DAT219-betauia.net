<template>
    <div class="seatmap">
        <div id="info">
          <div id="eventInfo">
              <p>Total seats for event: {{seatmapmodel.numSeats}}</p>
              <p>Available seats: {{seatmapmodel.numSeatsAvailable}}</p>
          </div>

          <div id="buyInfo">
              <p>Subtotal (inkludert skatt): {{price}} ;-</p>
              <button class="" @click="buyTickets">Buy me</button>
          </div>
        </div>

        <div id="grid">
          <div v-for="seat in seats" v-bind:key="seat">
            <Seat v-bind:seat="seat" @clicked="onSeatClick"></Seat>
          </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
import Vue from "vue";
import Seat from "@/components/Events/Seat.vue";
export default {
  components: {
    Seat: Seat,
  },
  data: function() {
    return {
      seats: [],
      seatmapmodel: {},
      reservedSeats:{},
    };
  },
  methods:{
    onSeatClick(seat,status){
      this.reservedSeats[seat] = status;
    },
    buyTickets: function (event) {
      var seatsToBuy = [];
      const self = this;
      this.seats.forEach(function (seat) {
        if(self.reservedSeats[seat.id]==true){
          seatsToBuy.push(seat.id);
        }
      })
      
      console.log(seatsToBuy);
    },
  },
  created() {
    var eventid = this.$route.params.eventid;
    var mapid = this.$route.params.seatmapid;

    var token = localStorage.getItem("token");
    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var self = this;
    axios
      .get("/api/eventseatmap/" + mapid +"/" + token, config)
      .then(function(response) {
        self.seats = response.data.seats;
        self.seatmapmodel = response.data.seatMapModel;
        console.log(response.data);
        console.log(self.seatmapmodel);

        self.seats.forEach(function(item){
          self.reservedSeats[item.id] = item.isReserved;
        })
        console.log("seats")
        console.log(self.reservedSeats)
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

<style>
#grid {
  background-image: url("https://i.imgur.com/6BryTTm.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  width: 900px;
  height: 900px;
}
#info{
    margin:10px;
    overflow: hidden;
}
#info div{
}
#eventInfo{
    float:left;
    width: 50%;
    overflow:hidden;
}
#buyInfo{
    float: left;
    width: 50%;
    overflow: hidden;
}
#buyInfo button{
    width: 50%;
}
</style>
