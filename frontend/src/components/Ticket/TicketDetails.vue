<template>
    <div id="TicketDetails" class="column is-8 is-offset-2">
        <p class="label">Purchased seat:</p>
        <p v-for="seat of ticketModel.seats" v-bind:key="seat">{{seat.number}}</p>
        <p>Total price: {{ticketModel.amount /100}} kr (NOK)</p>
        <p>Payment status: {{ticketModel.status}}</p>
        <p>Time purchased: {{ticketModel.timePurchased}}</p>
        <img class="is-center" :src="image">
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'TicketDetails',
    data(){
      return{
        image:'',
        ticketModel:{},
      }
    },
    async created() {
      const id = this.$route.params.id;
      const self = this;
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };

      await axios
        .get("/api/ticket/paymentstatus/"+id,config)
        .then(function(response){
          console.log(response.data);
          self.ticketModel = response.data;
          self.image = 'data:image/png;base64,'+response.data.qr
        })
        .catch(function(error){
          console.log(error.response.data);
        })
    }
  };
</script>

<style scoped>

</style>
