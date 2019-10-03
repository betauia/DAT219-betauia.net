<template>
    <div id="TicketDetails">
        <p>Confirm purchase of seat:</p>
        <p v-for="seat of seats" v-bind:key="seat">{{seat.number}}</p>
        <p>Choose phone number for payment</p>
        <input id="phoneNumber" type="text" v-model="ticket.phoneNumber" required>
        <p>Total price: {{ticket.amount}}</p>
        <p>Payment status: {{ticket.status}}</p>
        <p>Time purchased: {{ticket.timePurchased}}</p>
    </div>
</template>

<script>
  import axios from 'axios';

  export default {
    name: 'TicketDetails',
    data(){
      return{
        ticketModel:{},
      }
    },
    created() {
      const id = this.$route.params.id;
      const self = this;
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };

      axios
        .get("/api/ticket/ticketpaymentstatus/"+id,config)
        .then(function(response){
          console.log(response.data);
          self.ticketModel = response.data;
        })
        .catch(function(error){
          console.log(error.response.data);
        })
    }
  };
</script>

<style scoped>

</style>
