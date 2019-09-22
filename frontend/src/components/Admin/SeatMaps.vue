<template>
    <div id="seatmaps">
        <table>
            <tr>
                <th>Id</th>
                <th>Number of seats</th>
                <th>Edit/Del</th>
            </tr>
            <tr v-for="seatmap of seatMaps" v-bind:key="seatmap">
                <td>{{seatmap.id}}</td>
                <td>{{seatmap.numSeats}}</td>
                <td>
                    <a @click="detailClick(seatmap.id)">Details/Edit</a>|
                    <a @click="deleteClick(seatmap.id)">Delete me</a>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: 'SeatMaps',
    data(){
      return{
        seatMaps:[],
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/seatmap")
        .then(function (response) {
          self.seatMaps = response.data;
          console.log(response.data);
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods:{
      detailClick(id){
        this.$router.push('/admin/seatmapdetail/'+id);
      },
      deleteClick(id){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        const self = this;
        axios
          .delete("/api/seatmap/"+id,config)
          .then(function (response) {
            console.log(response.data);
            location.reload();
          })
          .catch(function(error){
            console.log(error.response);
          });
      }
    }
  };
</script>

<style scoped>
    table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

    td, th {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 8px;
    }

    tr:nth-child(even) {
        background-color: #dddddd;
    }

    a{
        color: blue;
    }

    a:link{
        color:blue;
    }
    a:visited{
        color: blue;
    }
    a:hover{
        color: black;
    }
</style>
