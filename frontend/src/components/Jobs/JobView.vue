<template>
    <div class="Jobs">
        <div class="job" v-for="job of jobs" v-bind:key="job">
            <p>{{job.title}}</p>
            <p>{{job.content}}</p>
            <p>{{job.url}}</p>
        </div>
    </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: 'JobView',
    data(){
      return{
        jobs:[],
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/job")
        .then(function (response) {
          console.log(response.data);
          self.jobs = response.data;
        })
        .catch(function (error) {
          console.log(error);
        })
    }
  };
</script>

<style scoped>
.job{
    margin: 5px;
    background-color: darkgray;
    width: 30%;
}
</style>
