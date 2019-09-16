<template>
    <div id="events">
        <table>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Description</th>
                <th>Edit/Del</th>
            </tr>
            <tr v-for="event of events" v-bind:key="event">
                <td>{{event.id}}</td>
                <td>{{event.title}}</td>
                <td>{{event.description}}</td>
                <td>
                    <a @click="detailClick(event.id)">Details/Edit</a>|
                    <a @click="deleteClick(event.id)">Delete me</a>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: 'Event',
    data(){
      return{
        events:[],
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/event")
        .then(function (response) {
          var data = [];
          response.data.forEach(function (val) {
            var t = val.eventModel;
            t.sponsors = val.sponsors;
            data.push(t);
          });
          console.log(data);
          self.events = data;
        })
        .catch(function (error) {
          console.log(error.response)
        })
    },
    methods:{
      detailClick(id){
        this.$router.push('/admin/event/'+id);
      },
      deleteClick(id){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        const self = this;
        axios
          .delete("/api/event/"+id,config)
          .then(function (response) {
            console.log(response.data);
            location.reload();
          })
          .catch(function(error){
            console.log(error.response);
          });
      },
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
