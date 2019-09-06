<template>
  <div class="Events">
    <div class="column padding">
      <div class="card">
        <header class="card-header">
          <p class="card-header-title">{{event.title}}</p>
        </header>
        <div class="card-content">
          <div class="content">
            <ul>
              <li>subTitle: {{event.subTitle}}</li>
              <li>Description: {{event.description}}</li>
              <li>Content: {{event.content}}</li>
              <li>EventTime: {{event.eventTime}}</li>
              <li v-if="event.sponsorId">Event sponsor: {{event.sponsorId}}</li>
            </ul>
            <br>
            <div v-if="event.seatMap!=null">
                <p>Number of seats: {{event.seatMap.numSeats}}</p>
                <p>Atendees: {{event.seatMap.numSeats - event.seatMap.numSeatsAvailable}}</p>
                <p>Seats left: {{event.seatMap.numSeatsAvailable}}</p>
            </div>
            <button v-if="event.seatMapId" v-on:click="buyticket">Buy ticket</button>
              <button v-if="event.seatMapId" v-on:click=""
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
  export default {
  name: "Events",
  props: {
    event: Object
  },
  created() {
    console.log(this.event);
  },
  methods: {
    buyticket() {
      var token = localStorage.getItem("token");
      if (token == null) {
        alert("please login to buy a ticket");
        return;
      }
      var self = this;
      axios
        .post("/api/token/valid/" + token, {})
        .then(function(response) {
        })
        .catch(function(error) {
            self.isLoggedIn = false;
            alert("plaese login to buy a ticket")
        });

      this.$router.push(
        "/events/seatmap/" + this.event.id + "/" + this.event.seatMapId
      );
    }
  }
};
</script>

<style>
.italic {
  padding: 5px;
  font-style: italic;
  font-size: 12px;
}
</style>
