<template>
    <div class="seatmap">
        <div id="info">
          <div id="eventInfo">
              <p>Plasser for dette arrangementet: {{seatmapmodel.numSeats}}</p>
              <p>Ledige plasser: {{seatmapmodel.numSeatsAvailable}}</p>
          </div>

          <div id="buyInfo">
              <button class="button is-link" @click="buyTickets">Til betaling</button>
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
import axios from 'axios';
import Vue from 'vue';
import Seat from '@/components/Events/Seat.vue';

export default {
  components: {
    Seat,
  },
  data() {
    return {
      seats: [],
      seatmapmodel: {},
      reservedSeats: {},
    };
  },
  methods: {
    onSeatClick(seat, status) {
      this.reservedSeats[seat] = status;
    },
    buyTickets(event) {
      const seatsToBuy = [];
      const self = this;
      this.seats.forEach((seat) => {
        if (self.reservedSeats[seat.id] == true) {
          seatsToBuy.push(seat.id);
        }
      });


      const token = localStorage.getItem('token');
      const config = {
        headers: { Authorization: "bearer " + token },
      };

      const bodyParam = {
        eventId:this.seatmapmodel.eventId,
        seats: seatsToBuy
      };

      console.log(bodyParam)
      axios
        .post('/api/ticket/newticket',bodyParam,config)
        .then(function (response) {
          console.log(response);
          self.$router.push("/ticket/" + response.data.id);
        })
        .catch(function (error) {
          console.log(error.response);
        });
    },
  },
  created() {
    const eventid = this.$route.params;
    const mapid = this.$route.params.seatmapid;

    const token = localStorage.getItem('token');
    const config = {
      headers: { Authorization: "bearer " + token },
    };
    const self = this;
    axios
      .get(`/api/eventseatmap/${mapid}`, config)
      .then((response) => {
        self.seats = response.data.seats;
        self.seatmapmodel = response.data.seatMapModel;
        console.log(response.data);
        console.log(self.seatmapmodel);

        self.seats.forEach((item) => {
          self.reservedSeats[item.id] = item.isReserved;
        });
        console.log('seats');
        console.log(self.reservedSeats);
      })
      .catch((error) => {
        console.log(error);
      });
  },
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
