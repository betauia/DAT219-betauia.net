<template>
    <div id="blogs">
        <table>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Last edit date</th>
                <th>Creation date</th>
                <th>Edit/Det/Del</th>
            </tr>
            <tr v-for="blog of blogs" v-bind:key="blog">
                <td>{{blog.id}}</td>
                <td>{{blog.title}}</td>
                <td>{{blog.lastEditDate}}</td>
                <td>{{blog.creationDate}}</td>
                <td>
                    <a @click="detailClick(blog.id)">Edit me</a>|
                    <a @click="deleteClick(blog.id)">Details me</a>|
                    <a @click="deleteClick(blog.id)">Delete me</a>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
  import axios from "axios";

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
        console.log(id);
      },
      deleteClick(id){

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
