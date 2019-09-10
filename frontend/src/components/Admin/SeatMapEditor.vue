<template>
  <div class="seatmapeditor">
    <div id="editor">
      <div class="editblock">
        <b-field label="Seatmap name">
          <input :value="Seatmap" id="seatmapname" name="seatmapname" placeholder="Map name">
        </b-field>
      </div>
      <div class="editblock" id="groupedit">
        <b-field label="Add group"></b-field>

        <div class="inputblock">
          <div class="row">
            <input :value="row" name placeholder="rows" id="row">
            <input :value="row" name placeholder="columns" id="column">
            <button v-on:click="createGroup">Create seat group</button>
          </div>
        </div>
      </div>
      <div class="editblock">
        <b-field label="Number of seats">
          <p>{{count}}</p>
        </b-field>
      </div>
      <div class="editblock">
        <button v-on:click="saveSeatMap" id="saveSeatmap">Save seatmap</button>
      </div>
    </div>
    <div id="grid">
      <div v-for="group in groups" v-bind:key="group">
        <SeatGroup v-bind:group="group"></SeatGroup>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Vue from "vue";
import SeatGroup from "@/components/SeatMap/SeatGroup.vue";
export default {
  components: {
    SeatGroup: SeatGroup
  },
  data: function() {
    return {
      count: 0,
      seats: [],
      groups: []
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
    createGroup: function(event) {
      var rows = document.getElementById("row").value;
      var columns = document.getElementById("column").value;

      if (rows == "" || columns == "") {
        alert("bad request");
        return;
      }

      var group = {};
      group.rows = rows;
      group.columns = columns;

      var seats = [];
      for (var x = 0; x < rows; x++) {
        for (var y = 0; y < columns; y++) {
          this.count++;
          var seat = {};
          seat.x = x * 20;
          seat.y = y * 20;
          seat.id = this.count;
          seats.push(seat);
        }
      }
      group.seats = seats;
      this.groups.push(group);
      //alert(JSON.stringify(group, null, 2));
    },
    saveSeatMap: function(event) {
      var name = document.querySelector("input[name=seatmapname]").value;
      var seatmap = {};
      seatmap.id = name;
      seatmap.numseats = this.count;
      var out = JSON.stringify(this.seats, null, 2);
      var groups = this.$el.querySelectorAll(".seatgroup");

      //alert(seats[0].offsetLeft);
      var jseats = [];
      groups.forEach(function(group) {
        var x = group.offsetLeft;
        var y = group.offsetTop;
        var seats = group.getElementsByClassName("seat");
        for (var i = 0; i < seats.length; i++) {
          var jseat = {};
          jseat.number = seats[i].textContent;
          jseat.x = seats[i].offsetLeft + x;
          jseat.y = seats[i].offsetTop + y;
          //jseat.ownerid = name;
          jseats.push(jseat);
        }
      });
      var output = {};
      output.seatmapmodel = seatmap;
      output.seats = jseats;

      //alert(JSON.parse(jseats, null, 2));

      //seats = group.getElementsByClassName("seat");
      //alert(seats[1].offsetLeft);

      var token = localStorage.getItem("token");

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var body = {
        seatmapmodel: seatmap,
        seats: jseats
      };
      console.log(body);

      var self = this;
      axios
        .post("/api/seatmap", body, config)
        .then(function(response) {
          console.log(response.data);
          self.$router.push("/admin/seatmapdetail/"+response.data.seatMapModel.id)
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>

<style>
.seatmapeditor {
  border-style: solid;
  border-width: medium;
  background-color: grey;
}
.editblock {
  margin: 10px;
  padding: 5px;
  background: rgb(151, 151, 151);
}
#seatmapname {
  width: 100%;
}
#groupedit input {
  width: 50%;
}
#groupedit button {
  width: 100%;
}
.seatmapeditor div {
  padding: 0;
  margin: 0;
}
#editor {
  vertical-align: top;
  display: inline-block;
  background-color: darkgrey;
  width: 20%;
  height: 100%;
}
#editor button {
  width: 100%;
}
#grid {
  vertical-align: top;
  display: inline-block;
  position: relative;
  background-image: url("https://i.imgur.com/6BryTTm.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  width: 900px;
  height: 900px;
}
</style>
