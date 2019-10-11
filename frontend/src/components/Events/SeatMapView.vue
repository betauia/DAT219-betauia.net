<template>
    <div class="seatmap">
        <div id="grid">
            <div class="card-img">
                <figure class="image is-grouped-centered" >
                    <img src="https://i.imgur.com/6BryTTm.jpg" width="900px" height="900px" id="img-info"/>
                    <div>
                        <div v-for="seat in seats" v-bind:key="seat">
                            <Seat v-bind:seat="seat" @clicked="onSeatClick"></Seat>
                        </div>
                    </div>
                </figure>
            </div>
        </div>
        <div  class="column is-7 card">
            <div class="card-content" id="buyTab">
                <div class="content">
                    <div>
                        <p>Plasser for dette arrangementet: {{seatmapmodel.numSeats}}</p>
                        <p>Ledige plasser: {{seatmapmodel.numSeatsAvailable}}</p>
                    </div>
                </div>
                <div class="content">
                    <div id="buyInfo">
                        <button class="button is-link" @click="buyTickets">Til betaling</button>
                        <button class="button is-warning" v-if="free==true" v-on:click="freeClick">Gratis til crew</button>
                    </div>
                </div>
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
      free:false,
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
    freeClick(){
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

      axios
        .post('/api/ticket/free',bodyParam,config)
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error.response);
        });
    }
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

    axios
      .get("/api/token/freeticket",config)
      .then(function(response){
        self.free = true;
      })
      .catch(function (error) {
        console.log(error.response);
        self.free = false;
      })
  },
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
#buyInfo{
    float: left;
    width: 50%;
    overflow: hidden;
}
#buyInfo button{
    width: 50%;
}
</style>
