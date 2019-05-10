<template>
  <div>
    <div id="editor">
      <b-field label="Seatmap name">
        <b-input :value="Seatmap" name placeholder="seatmap name"></b-input>
      </b-field>
      <b-field label="Mapwidth">
        <input :value="MapWidth" name placeholder="600" id="mapwidth">
      </b-field>
      <b-field label="Mapheight">
        <input :value="MapHeight" name placeholder="600" id="mapHeight">
      </b-field>
      <button v-on:click="changeMapSize()" id="updateSeatMap">Update seat map size</button>

      <b-field label="Seat Id">
        <input :value="SeatId" name placeholder="Seat id">
      </b-field>
      <button v-on:click="createSeat()" id="createSeat">Create new seat {{count}}</button>
    </div>
    <div id="grid">
      <div v-for="seat in seats" v-bind:key="seat">
        <Seat v-bind:seat="seat"></Seat>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Seat from "@/components/SeatMap/Seat.vue";
import Vue from "vue";

const seats = [
  {
    id: 1,
    width: "5%",
    height: "5%",
    x: "0%",
    y: "0%"
  }
];

export default {
  components: {
    Seat: Seat
  },
  data: function() {
    return {
      widthG: 10,
      heightG: 10,
      width: 600,
      height: 600,
      top: 0,
      left: 0,
      count: 0,
      seats: []
    };
  },
  methods: {
    changeMapSize: function(event) {
      this.width = document.getElementById("mapwidth").value;
      this.height = document.getElementById("mapHeight").value;
      document.getElementById("grid").style.width = this.width + "px";
      document.getElementById("grid").style.height = this.height + "px";
    },
    createSeat: function(event) {
      this.name = "hello world";
      var newseat = {
        id: this.count + 1,
        width: "5%",
        height: "5%",
        x: "0%",
        y: "0%"
      };
      this.seats[this.count] = newseat;
      this.$forceUpdate();
      this.count++;
    },
    resize(newRect) {
      this.width = newRect.width;
      this.height = newRect.height;
      this.top = newRect.top;
      this.left = newRect.left;
    }
  }
};
</script>


<style>
div {
  padding: 0;
  margin: 0;
}
#editor {
  background-color: darkgrey;
  width: 20%;
  height: 600px;
  float: left;
}
#grid {
  background-color: cadetblue;
  width: 600px;
  height: 600px;
  display: inline-block;
}
button {
  width: 100%;
  height: 5%;
}
</style>
