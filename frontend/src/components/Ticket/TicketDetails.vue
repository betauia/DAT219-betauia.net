<template>
    <div id="TicketDetails">
        <p>Purchased seat:</p>
        <p v-for="seat of ticketModel.seats" v-bind:key="seat">{{seat.number}}</p>
        <p>Phone number {{ticketModel.mobileNumber}}</p>
        <p>Total price: {{ticketModel.amount}}</p>
        <p>Payment status: {{ticketModel.status}}</p>
        <p>Time purchased: {{ticketModel.timePurchased}}</p>
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
        .get("/api/ticket/paymentstatus/"+id,config)
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
