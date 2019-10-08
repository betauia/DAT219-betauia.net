<template>
    <div id="TicketDetails">
    <div class="form-horizontal padding center addevent" enctype="multipart/form-data">
        <div class="is-1 title">Ticket info</div>
        <p>Purchased seat:</p>
        <p v-for="seat of ticketModel.seats" v-bind:key="seat">{{seat.number}}</p>
        <p>Total price: {{ticketModel.amount}}</p>
        <p>Payment status: {{ticketModel.status}}</p>
        <p>Time purchased: {{ticketModel.timePurchased}}</p>
        <p>Verified: {{ticketModel.verified}}</p>
        <b-button class="is-primary" v-on:click="verifyTicket">Verify</b-button>
        <b-button class="is-warning" v-on:click="refundTicket">Refund ticket</b-button>
    </div>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'TicketVerify',
    data(){
      return{
        image:'',
        ticketModel:{}
      }
    },
    created() {
      var id = this.$route.params.id;
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };
      var self = this;
      axios
        .get("/api/ticket/verify/"+id,config)
        .then(function (response) {
          self.ticketModel = response.data;
          self.image = 'data:image/png;base64,'+response.data.qr
        })
        .catch(function(error){
          console.log(error.response);
        });
    },
    methods:{
      verifyTicket(){
        var id = this.$route.params.id;
        var token = localStorage.getItem("token");
        var config = {
          headers: { Authorization: "bearer " + token }
        };
        var self = this;
        var body = {
          id:id
        };
        axios
          .post("/api/ticket/verify",body,config)
          .then(function (response) {
            self.ticketModel = response.data;
            self.image = 'data:image/png;base64,'+response.data.qr
          })
          .catch(function(error){
            console.log(error.response);
          });
      },
      refundTicket(){
        var id = this.ticketModel.id;
        var token = localStorage.getItem("token");
        var config = {
          headers: { Authorization: "bearer " + token }
        };
        var self = this;
        axios
          .delete("/api/ticket/delete/"+id,config)
          .then(function (response) {
            self.ticketModel = response.data;
            self.image = 'data:image/png;base64,'+response.data.qr
          })
          .catch(function(error){
            console.log(error.response);
          });
      }
    }
  };
</script>

<style scoped>

</style>
