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
        <textarea class="textarea" id="content" name="content"></textarea>
      </div>
    </div>

    <!-- Select Basic -->
    <div class="field">
      <label class="label" for="eventIs">Is Event Public</label>
      <div class="control">
        <div class="select">
          <select id="eventIs" name="eventIs" v-model="ispublic">
            <option>true</option>
            <option>false</option>
          </select>
        </div>
      </div>
    </div>

    <div class="field sidebyside">
      <label class="label" for="sponsor">Select sponsors</label>
      <div class="control chooser">
        <select v-model="selectedSponsor" name="sponsor" class="chooser">
          <option disabled value>Select a sponsor</option>
          <option v-for="item in sponsors" :value="item" v-bind:key="item">{{item.id}}</option>
        </select>
      </div>
    </div>

    <div class="field sidebyside">
      <label class="label" for="seatmap">Select seatmap</label>
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
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      seatmaps: [],
      sponsors: [],
      selectedSeatmap: null,
      selectedSponsor: null,
      ispublic: null
    };
  },
  methods: {
    addEvent() {
      var token = localStorage.getItem("token");
      var title = document.querySelector("input[name=title]").value;
      var description = document.querySelector("input[name=description]").value;
      var content = document.querySelector("textarea[name=content]").value;

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var bodyParamters = {
        title: title,
        description: description,
        content: content,
        isPublic: this.ispublic
      };

      if (this.selectedSeatmap != null) {
        bodyParamters.seatmapid = this.selectedSeatmap.id;
      }
      if (this.selectedSponsor != null) {
        bodyParamters.sponsorid = this.selectedSponsor.id;
      }

      console.log(bodyParamters);
      var self = this;
      axios
        .post("/api/event", bodyParamters, config)
        .then(function(response) {
          console.log(response.data);
        })
        .catch(function(error) {
          console.log(error);
        });
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
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

<style>
.sidebyside {
  width: 50%;
  vertical-align: top;
  display: inline-block;
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
</style>
