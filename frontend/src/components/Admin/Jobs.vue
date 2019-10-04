<template>
    <div id="jobs">
        <table>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Description</th>
                <th>Url</th>
                <th>Edit/Del</th>
            </tr>
            <tr v-for="job of jobs" v-bind:key="job">
                <td>{{job.id}}</td>
                <td>{{job.title}}</td>
                <td>{{job.content}}</td>
                <td>{{job.url}}</td>
                <td>
                    <a @click="detailClick(job.id)">Details/Edit</a>|
                    <a @click="deleteClick(job.id)">Delete me</a>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: 'Jobs',
    data(){
      return{
        jobs:[]
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/job")
        .then(function(response){
          console.log(response.data);
          self.jobs = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods:{
      detailClick(id){
        this.$router.push("/admin/jobdetail/"+id);
      },
      deleteClick(id){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        const self = this;
        axios
          .delete("/api/job/"+id,config)
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
