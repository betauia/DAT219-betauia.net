<template>
    <div id="buyTicket">
        <p>Confirm purchase of seat:</p>
        <p v-for="seat of seats" v-bind:key="seat">{{seat.number}}</p>
        <p>Choose phone number for payment</p>
        <input id="phoneNumber" type="text" v-model="ticket.phoneNumber" required>
        <p>Total price: {{ticket.amount}}</p>
        <button @click="initiateVippsPayment">Pay by vipps</button>
    </div>
</template>

<script>
import axios from 'axios';
  export default {
    name: 'TicketInit',
    data(){
      return{
        ticket:{},
        seats:{},
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
        .get("/api/ticket/get/"+id,config)
        .then(function (response) {
          console.log(response);
          self.ticket = response.data;
          self.seats = response.data.eventSeats;
        })
        .catch(function (error) {
          console.log(error.response);
        });
    },
    methods:{
      initiateVippsPayment(){
        const id = this.$route.params.id;
        const self = this;
        var token = localStorage.getItem("token");
        var config = {
          headers: { Authorization: "bearer " + token }
        };

        var bodyParam = {
          id:id,
          mobileNumber: this.ticket.phoneNumber,
        };

        console.log(bodyParam);
        axios
          .post("/api/ticket/initiatepayment",bodyParam,config)
          .then(function (response) {
            console.log(response.data);
            window.location = response.data;
          })
          .catch(function (error) {
            console.log(error.response);
          });
      }
    }
  };
</script>

<style scoped>

</style>
