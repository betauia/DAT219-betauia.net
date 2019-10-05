<template>
    <div id="sponsors">
        <table>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Description</th>
                <th>Url</th>
                <th>Edit/Del</th>
            </tr>
            <tr v-for="sponsor of sponsors" v-bind:key="sponsor">
                <td>{{sponsor.id}}</td>
                <td>{{sponsor.title}}</td>
                <td>{{sponsor.description}}</td>
                <td>{{sponsor.url}}</td>
                <td>
                    <a @click="detailClick(sponsor.id)">Details/Edit</a>|
                    <a @click="deleteClick(sponsor.id)">Delete me</a>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'Sponsors',
    data(){
      return{
        sponsors:[],
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/sponsor")
        .then(function(response){
          console.log(response.data);
          self.sponsors = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods:{
      detailClick(id){
        this.$router.push("/admin/sponsordetail/"+id);
      },
      deleteClick(id){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        const self = this;
        axios
          .delete("/api/sponsor/"+id,config)
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
