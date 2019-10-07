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
        <div class="card-content">
          <div class="content">
              <div class="label">{{event.eventModel.description}}</div>
              <div class="content">{{event.eventModel.content}}</div>
              <li>Starter: {{event.eventModel.startdate}}</li>
                <li>Slutter: {{event.eventModel.enddate}}</li>
              <div>
                  <div class="label" v-if="event.sponsors.length>0">Sponset av:

                      <div class="content" v-for="sponsor of event.sponsors">
                          <p>{{sponsor.title}}</p>
                          <p>{{sponsor.description}}</p>
                          <p>{{sponsor.url}}</p>
                      </div>
                  </div>
              </div>
                <div v-if="event.eventModel.maxAtendees>=0">
                    <p class="label">Bli med!</p>
                    <span v-if="event.eventModel.atendees < event.eventModel.maxAtendees">
                            <p>Antall påmeldte: {{event.eventModel.atendees}}</p>
                            <p>Ledige plasser: {{event.eventModel.maxAtendees - event.eventModel.atendees}}</p>
                    </span>
                    <footer class="card-footer">
                        <div class="card-footer-item">
                            <b-button class="is-primary" v-on:click="joinEventByUser">Reserver med BETA konto</b-button>
                        </div>
                        <div class="card-footer-item">
                            <b-button class="is-primary" v-on:click="showEmail=!showEmail">Reserver med epost</b-button>
                        </div>
                        <span v-if="showEmail">
                            <p><input class="input is-primary" type="text" name="firstname" placeholder="Fornavn"/></p>
                            <p><input class="input is-primary" type="text" name="lastname" placeholder="Etternavn"/></p>
                            <p><input class="input is-primary" type="text" name="email" placeholder="email@address.com"/></p>
                            <button class="button is-link" v-on:click="joinEventByEmail">Sign me up!!</button>
                            <div v-if="emailsignupResponse!=null">{{emailsignupResponse}}</div>
                        </span>
                    </footer>
                    <button class="button is-link" v-if="showEmail==true" v-on:click="joinEventByEmail">Sign me up!!</button>
                </div>

            <div v-if="event.eventModel.seatMap!=null">
                <p>Number of seats: {{event.eventModel.seatMap.numSeats}}</p>
                <p>Atendees: {{event.eventModel.seatMap.numSeats - event.eventModel.seatMap.numSeatsAvailable}}</p>
                <p>Seats left: {{event.eventModel.seatMap.numSeatsAvailable}}</p>
                <button class="button is-link" v-on:click="buyticket">Kjøp billett</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from"@/axios.js";
import Sponsors from "../Sponsors/Sponsors";
  export default {
  name: "Events",
      components: {
      showEmail: false,
      Sponsors,
      },
      props: {
    event: Object
  },
  created() {
    console.log(this.event);
  },
    data(){
        return{
            showEmail:false,
            emailsignupResponse:null,
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
        .get("/api/token/valid/" + token, {})
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
    async joinEventByUser(){
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };
      var exit = false;
      var self = this;

      await axios
        .get("/api/token/accountverified", config)
        .then(function(response) {

        })
        .catch(function(error) {
          if(error.response.data == 603){
            self.$router.push("/account/notverified");
            exit = true;
          }else{
            self.isLoggedIn = false;
            self.$router.push("/account/login");
          }
        });

      if(exit)return ;

      axios
        .get("api/eventsignup/user/"+this.event.eventModel.id,config)
        .then(function(response){
          console.log(response.data);
          self.event.eventModel.atendees++;
        })
        .catch(function(error){
          console.log(error);
        });

    },
    emailClick(){
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
          self.emailsignupResponse = "A confirmation email has been sent, it expires in 1 hour";
        })
        .catch(function(error){
          console.log(error.response);
          if(error.response.data == "Email already registered"){
            self.emailsignupResponse = "Email already registered for event, please use another"
          }else{
            self.emailsignupResponse = "An error occured, please try again later";
          }
        })
    },
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
