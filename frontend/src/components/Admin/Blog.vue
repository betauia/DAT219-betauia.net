<template>
    <div id="blogs">
        <table>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Last edit date</th>
                <th>Creation date</th>
                <th>Edit/Del</th>
            </tr>
            <tr v-for="blog of blogs" v-bind:key="blog">
                <td>{{blog.id}}</td>
                <td>{{blog.title}}</td>
                <td>{{blog.lastEditDate}}</td>
                <td>{{blog.creationDate}}</td>
                <td>
                    <a @click="detailClick(blog.id)">Details/Edit</a>|
                    <a @click="deleteClick(blog.id)">Delete me</a>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
  import axios from"@/axios.js";

  export default {
    name: 'Blog',
    data(){
      return{
        blogs:[],
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/blog")
        .then(function(response){
          console.log(response.data);
          self.blogs = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods:{
      detailClick(id){
        this.$router.push("/admin/blog/"+id);
      },
      deleteClick(id){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        const self = this;
        axios
          .delete("/api/blog/"+id,config)
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
