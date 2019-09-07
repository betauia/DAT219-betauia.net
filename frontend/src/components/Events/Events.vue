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
              <li>Description: {{event.description}}</li>
              <li>Content: {{event.content}}</li>
              <li>EventTime: {{event.eventTime}}</li>
              <li v-if="event.sponsorId">Event sponsor: {{event.sponsorId}}</li>
            </ul>
            <br>
            <div v-if="event.maxAtendees>=0">
                <h1>Join our event</h1>
                <p>People joining: {{event.atendees}}</p>
                <button v-on:click="joinEventByUser">Reserve by account</button>
                <button v-on:click="emailClick">Reserve by email</button>
                <div id="emailSignup" v-if="showEmail==true">
                    Firstname: <input type="text" name="firstname"><br>
                    Lastname: <input type="text" name="lastname"><br>
                    Email: <input type="text" name="email"><br>
                    <button v-on:click="joinEventByEmail">Sign me up!!</button>
                </div>
            </div>
            <div v-if="event.seatMap!=null">
                <p>Number of seats: {{event.seatMap.numSeats}}</p>
                <p>Atendees: {{event.seatMap.numSeats - event.seatMap.numSeatsAvailable}}</p>
                <p>Seats left: {{event.seatMap.numSeatsAvailable}}</p>
                <button v-on:click="buyticket">Buy ticket</button>
            </div>
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
    data(){
        return{
            showEmail:true,
        }
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
    },
    joinEventByUser(){
        if(this.loggedInUser()==true){
          var token = localStorage.getItem("token");
          var bodyParameter = {};
          var config = {
            headers: { Authorization: "bearer " + token }
          };
          var self = this;
          axios
            .post("api/eventsignup/user/"+this.event.id,bodyParameter,config)
            .then(function(response){
              console.log(response.data);
              self.event.atendees++;
            })
            .catch(function(error){
              console.log(error);
            });
        }
    },
    emailClick(){
        this.showEmail=true;
        console.log(this.showEmail);
    },
    joinEventByEmail(){
      var firstname = document.querySelector("input[name=firstname]").value;
      var lastname = document.querySelector("input[name=lastname]").value;
      var email = document.querySelector("input[name=email]").value;

      const self = this;
      axios
        .post("/api/eventsignup/email/"+this.event.id,{
          firstname:firstname,
          lastname:lastname,
          email:email,
          eventid:this.event.id
        })
        .then(function(response){
          console.log(response);
          self.event.atendees++;
        })
        .catch(function(error){
          console.log(error);
        })
    },
    loggedInUser(){
      var token = localStorage.getItem("token");
      var self = this;
      axios
        .post("/api/token/valid/" + token, {})
        .then(function(response) {
            return true;
        })
        .catch(function(error) {
          console.log(error);
          self.isLoggedIn = false;
          self.$router.push("/account/login");
        });
      return true;
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
#emailSignup{
    margin-top:5px;
    width: 25%;
    background-color: aliceblue;
}
#emailSignup input{
    float: right;
}
#emailSignup button{
    width: 50%;
    position: relative;
    right: -50%;
}
</style>
