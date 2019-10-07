<template>
    <div class="Jobs">
        <div class="job column is-8 is-offset-2" v-for="job of jobs" v-bind:key="job">
            <div class="card is-centered">
                <header class="card-header-title title">{{job.title}}</header>
                <div class="card-content content">{{job.content}}</div>
                <div class="card-footer content">{{job.url}}</div>
            </div>
        </div>
    </div>

</template>

<script>
  import axios from"@/axios.js";

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
</style>
