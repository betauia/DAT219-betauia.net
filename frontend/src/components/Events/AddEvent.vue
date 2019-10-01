<!--
Title = title;
SubTitle = subTitle;
Description = description;
Content = content;
EventTime = eventTime;
Author = author;
Image = image;
Atendees = atendees;
IsPublic = isPublic;
-->

<template>
<div id="addEvent">
    <div class="form-horizontal padding center addevent" enctype="multipart/form-data">
    <div class="is-1 title">Add Event</div>

    <!-- Text input-->
    <div class="field">
      <label class="label" for="title">Title</label>
      <div class="control">
        <input
          id="title"
          name="title"
          type="text"
          placeholder="Betalan #420"
          class="input"
          required
        >
      </div>
    </div>

    <!-- Text input-->
    <div class="field">
      <label class="label" for="description">Description</label>
      <div class="control">
        <input
          id="description"
          name="description"
          type="text"
          placeholder="Semesterets feeteste LAN"
          class="input"
          required
        >
      </div>
    </div>

    <!-- Textarea -->
    <div class="field">
      <label class="label" for="content">Content</label>
      <div class="control">
        <textarea class="textarea" id="textcontent" name="content"></textarea>
      </div>
    </div>

    <!-- Select Basic -->
    <div class="field">
      <label class="label" for="eventIs">Is Public Event</label>
      <div class="control">
        <div class="select">
          <select id="eventIs" name="eventIs" v-model="ispublic">
            <option>true</option>
            <option>false</option>
          </select>
        </div>
      </div>
    </div>

    <div class="field" id="startdate">
        <label class="label" for="eventIs">Startdate</label>
        <div class="control">
            <datetime
                type="datetime"
                v-model="startdate"
                input-class="my-class"
                value-zone="Europe/Oslo"
                format="dd-MM-yyyy HH:mm"
                zone="Europe/Oslo"
                :phrases="{ok: 'Continue', cancel: 'Exit'}"
                :hour-step="1"
                :minute-step="5"
                :week-start="1"
                use24-hour
                auto
            ></datetime>
        </div>
    </div>
    <div class="field" id="enddate">
        <label class="label" for="eventIs">Enddate</label>
        <div class="control">
            <datetime
                type="datetime"
                v-model="enddate"
                input-class="my-class"
                value-zone="Europe/Oslo"
                format="dd-MM-yyyy HH:mm"
                zone="Europe/Oslo"
                :phrases="{ok: 'Continue', cancel: 'Exit'}"
                :hour-step="1"
                :minute-step="5"
                :min-datetime="startdate"
                :week-start="1"
                use24-hour
                auto
            ></datetime>
        </div>
    </div>

    <div class="eventOptions">
        <label class="Event options">Choose event options</label>
        <div>
            <label class="sidebyside30">Has Sponsor
                <button :style="sponsorStyle" type="button" @click="hasSponsorClick">Click me</button>
            </label>
            <label class="sidebyside30">Is bookable
                <button :style="bookableStyle"type="button" @click="isBookableClick">Click me</button>
            </label>
            <label class="sidebyside30">Has seatmap
                <button :style="seatmapStyle" type="button" @click="hasSeatMapClick">Click me</button>
            </label>
        </div>
    </div>

    <div class="field sidebyside50" v-if="hasSponsor.state==true">
      <label class="label" for="sponsor">Added sponsors</label>
        <div v-for="sponsor of selectedSponsors">
            <button class="addSponsorB" v-on:click="removeSponsor(sponsor.id)">{{sponsor.id}}</button>
        </div>
      <label class="lable">Sponsors to choose from</label>
        <div v-for="sponsor of sponsors">
            <button class="addSponsorB" v-on:click="addSponsor(sponsor.id)">{{sponsor.id}}</button>
        </div>
    </div>

    <div class="field sidebyside50" v-if="isBookable.state==true">
        <label class="label" for="atendees">Choose how many atendees</label>
        <div class="control chooser">
            <input id="numberAtendees" type="number" value="1" min="1">
        </div>
    </div>

    <div class="field sidebyside50" v-if="hasSeatMap.state==true">
      <label class="label" for="seatmap" >Select seatmap</label>
      <div class="control chooser">
        <select v-model="selectedSeatmap" name="seatmap" class="chooser">
          <option disabled value>Select a seatmap</option>
          <option
            v-for="item in seatmaps"
            :value="item"
            v-bind:key="item"
          >{{item.id}}, seatmap has {{item.numSeats}} seats</option>
        </select>
      </div>
    </div>
    <!-- Button -->
    <button id="publish" name="publish" class="button is-primary" v-on:click="addEvent">Publish</button>
  </div>
</div>
</template>

<script>
import axios from "axios";
import { Datetime } from 'vue-datetime';

export default {
  comments:{
    datetime: Datetime,
  },
  data() {
    return {
      seatmaps: [],
      sponsors: [],
      selectedSeatmap: null,
      ispublic: null,
      isBookable:{
        state:false,
        color:"red",
      },
      hasSeatMap:{
        state:false,
        color:"red",
      },
      hasSponsor:{
        state:false,
        color:"red",
      },
      selectedSponsors:[],
      startdate:"0000-00-00T00:00:00.000Z",
      enddate:"2019-10-01T16:50:00.000Z",
    };
  },
  methods: {
    addEvent() {
      var token = localStorage.getItem("token");
      var title = document.querySelector("input[name=title]").value;
      var description = document.querySelector("input[name=description]").value;
      var content = document.querySelector("#textcontent").value;
      const startdatedoc = document.querySelector('#startdate');
      const enddatedoc = document.querySelector('#enddate');
      const enddate = enddatedoc.querySelector('.vdatetime-input').value;
      const startdate = startdatedoc.querySelector('.vdatetime-input').value;
      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var eventModel = {
        title: title,
        description: description,
        content: content,
        isPublic: this.ispublic,
        enddate:enddate,
        startdate:startdate
      };



      var bodyParameters = {
        eventModel:eventModel,
        sponsors:[],
      };

      if(this.hasSponsor.state == true){
        if (this.selectedSponsors.length >0) {
          bodyParameters.sponsors = this.selectedSponsors;
        }
      }
      if(this.hasSeatMap.state==true){
        if (this.selectedSeatmap != null) {
          bodyParameters.eventModel.seatmapid = this.selectedSeatmap.id;
        }
      }
      if(this.isBookable.state==true){
        const atendees = document.getElementById("numberAtendees").value;
        bodyParameters.eventModel.MaxAtendees = atendees;
      }

      var self = this;
      axios
        .post("/api/event", bodyParameters, config)
        .then(function(response) {
          self.$router.push("/events/");
          console.log(response.data);
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    isBookableClick(){
        this.isBookable.state = !this.isBookable.state;
        if(this.isBookable.state == true){
          this.isBookable.color = "green";
        }else{
          this.isBookable.color = "red";
        }

        if(this.hasSeatMap.state == true){
          this.hasSeatMap.state = false;
          this.hasSeatMap.color = "red"
        }
    },
    hasSponsorClick(){
      this.hasSponsor.state = !this.hasSponsor.state;
      if(this.hasSponsor.state == true){
        this.hasSponsor.color = "green";
      }else{
        this.hasSponsor.color = "red";
      }
      console.log(this.startdate);
    },
    hasSeatMapClick(){
      this.hasSeatMap.state = !this.hasSeatMap.state;
      if(this.hasSeatMap.state == true){
        this.hasSeatMap.color = "green";
      }else{
        this.hasSeatMap.color = "red";
      }
      if(this.isBookable.state == true){
        this.isBookable.state = false;
        this.isBookable.color = "red"
      }
    },
    addSponsor(id){
      this.selectedSponsors.push(this.sponsors.filter(function(x){return x.id ==id})[0]);
      this.sponsors = this.sponsors.filter(function(x){return x.id !=id});
    },
    removeSponsor(id){
      this.sponsors.push(this.selectedSponsors.filter(function(x){return x.id ==id})[0]);
      this.selectedSponsors = this.selectedSponsors.filter(function(x){return x.id !=id});
    }
  },
  created() {
    var token = localStorage.getItem("token");
    var config = {
      headers: { Authorization: "bearer " + token }
    };

    var self = this;
    axios
      .get("/api/sponsor", config)
      .then(function(response) {
        self.sponsors = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error);
      });

    axios
      .get("/api/seatmap", config)
      .then(function(response) {
        self.seatmaps = response.data;
      })
      .catch(function(error) {
        console.log(error);
      });
  },
  computed:{
    sponsorStyle(){
      return "backgroundColor:"+this.hasSponsor.color;
    },
    bookableStyle(){
      return "backgroundColor:"+this.isBookable.color;
    },
    seatmapStyle(){
      return "backgroundColor:"+this.hasSeatMap.color;
    }
  }
};
</script>

<style>
.sidebyside50 {
  width: 50%;
  vertical-align: top;
  display: inline-block;
}
.sidebyside30 {
    width: 30%;
    vertical-align: top;
    display: inline-block;
    background-color: #6c6c6c;
    margin: 1.5%;
    color: white;
    text-align: center;
}
.eventOptions {
    background-color: #bbbbbb;
}
.sidebyside30 button {
    width: 50%;
}
.field {
  background-color: rgb(185, 185, 185);
  padding: 2px;
}
.chooser {
  width: 100%;
}
#publish {
  width: 100%;
}
.eventOptions input{
    margin-left: 1%;
    margin-right: 1%;
}
.addSponsorB{
    width: 100%;
}
</style>
