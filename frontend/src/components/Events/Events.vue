<template>
  <div class="Events">
    <div class="column is-8 is-offset-2">
      <div class="card">
        <header class="card-header">
          <h1 class="card-header-title title">{{event.eventModel.title}}</h1>
        </header>
          <div class="card-img">
              <figure class="image is-grouped-centered">
                  <image-widget v-bind:image-id="event.eventModel.image"></image-widget>
              </figure>
          </div>
        <div class="card-content columns">
          <div class="content column is-two-thirds">
              <div class="label">{{event.eventModel.description}}</div>
              <div class="content">{{event.eventModel.content}}</div>
              <li>Starter : {{event.eventModel.startdate}}</li>
                <li>ending at: {{event.eventModel.enddate}}</li>
              <div class="column">
                  <div class="label" v-if="event.sponsors.length>0">Sponset av:
                      <div class="content" v-for="sponsor of event.sponsors">
                          <p>{{sponsor.title}}</p>
                          <p>{{sponsor.description}}</p>
                          <p>{{sponsor.url}}</p>
                      </div>
                  </div>
              </div>

            <div class="column is-right" v-if="event.eventModel.maxAtendees>=0">
                <div class="label">Bli med!</div>
                <div v-if="event.eventModel.atendees < event.eventModel.maxAtendees">
                    <p>Antall påmeldte: {{event.eventModel.atendees}}</p>
                    <p>Ledige plasser: {{event.eventModel.maxAtendees - event.eventModel.atendees}}</p>
                    <span>
                        <button class="button is-link" v-on:click="joinEventByUser">Reserve by account</button>
                        <button class="button is-valid" v-on:click="emailClick">Reserve by email</button>
                    </span>
                    <div id="emailSignup" v-if="showEmail==true">
                        <form>
                            <item><input type="text" name="firstname" placeholder="Fornavn"></item>
                            <item><input type="text" name="lastname" placeholder="Etternavn"></item>
                            <item><input type="text" name="email" placeholder="email@address.com"></item>
                        </form>
                </div>
                    <button class="button is-link" v-on:click="joinEventByEmail">Sign me up!!</button>
                </div>

            </div>
            <div v-if="event.eventModel.seatMap!=null">
                <p>Number of seats: {{event.eventModel.seatMap.numSeats}}</p>
                <p>Atendees: {{event.eventModel.seatMap.numSeats - event.eventModel.seatMap.numSeatsAvailable}}</p>
                <p>Seats left: {{event.eventModel.seatMap.numSeatsAvailable}}</p>
                <button class="button is-link" v-on:click="buyticket">Buy ticket</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Sponsors from "../Sponsors/Sponsors";
  export default {
  name: "Events",
      components: {Sponsors},
      props: {
    event: Object
  },
  created() {
    console.log(this.event);
  },
    data(){
        return{
            showEmail:false,
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
        "/events/seatmap/" + this.event.eventModel.seatMapId
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
            .post("api/eventsignup/user/"+this.event.eventModel.id,bodyParameter,config)
            .then(function(response){
              console.log(response.data);
              self.event.eventModel.atendees++;
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
        .post("/api/eventsignup/email/"+this.event.eventModel.id,{
          firstname:firstname,
          lastname:lastname,
          email:email,
          eventid:this.event.eventModel.id
        })
        .then(function(response){
          console.log(response);
          self.event.eventModel.atendees++;
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
