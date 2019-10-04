<template>
    <div class="eventattendees">
        <div class="container" v-if="event.ticketViews.length>0" >
            <div class="ticket" v-for="ticket of event.ticketViews">
                <p>Ticketid: {{ticket.id}}</p>
                <p>Email: {{ticket.email}}</p>
                <p>Firstname: {{ticket.firstname}}</p>
                <p>Lastname: {{ticket.lastname}}</p>
                <p v-for="seat of ticket.seats">
                    Seat: {{seat.number}}
                </p>
                <p>Status: {{ticket.status}}</p>
                <p>Vipps id: {{ticket.vippsid}}</p>
            </div>
        </div>

        <div class="container" v-if="event.attendViews.length>0">
            <div class="attendee" v-for="attendee of event.attendViews">
                <p>Email: {{attendee.email}}</p>
                <p>Firstname: {{attendee.firstname}}</p>
                <p>Lastname: {{attendee.lastname}}</p>
            </div>
        </div>
    </div>
</template>

<script>
  import axios from 'axios';
  export default {
    name: 'EventAttendees',
    data(){
      return{
        event:{},
      };
    },
    created() {
      const id = this.$route.params.id;
      const self = this;
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };

      axios
        .get("/api/eventsignup/attendee/get/"+id,config)
        .then(function (response) {
          console.log(response.data);
          self.event = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    }
  };
</script>

<style scoped>
    .container{
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(30%,1fr));
        grid-gap: 1%;
    }
.ticket {
    border-style: dotted;
    display: block;
    object-fit: contain;
    padding: 1%;
    width: auto;
    background-color: lightgray;
}
.attendee {
    border-style: dotted;
    display: block;
    object-fit: contain;
    padding: 1%;
    width: auto;
    background-color: lightgray;
}
</style>
