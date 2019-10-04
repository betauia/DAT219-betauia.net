<template>
    <div class="form-horizontal padding center" enctype="multipart/form-data">
        <div class="is-1 title">Add job</div>
        <!-- Text input-->
        <div class="field">
            <label class="label" for="jobTitle">Title</label>
            <div class="control">
                <input
                    id="jobTitle"
                    name="jobTitle"
                    type="text"
                    placeholder="Jobb tittel"
                    class="input is-primary"
                    v-model="job.title"
                    required
                >
            </div>
        </div>

        <!-- Text input-->
        <div class="field">
            <label class="label" for="jobUrl">Url</label>
            <div class="control">
                <input
                    id="jobUrl"
                    name="jobUrl"
                    type="text"
                    placeholder="job.no"
                    class="input is-primary"
                    v-model="job.url"
                    required
                >
            </div>
        </div>

        <!-- Textarea -->
        <div class="field">
            <label class="label" for="jobDescription">Content</label>
            <div class="control">
        <textarea
            class="textarea is-primary"
            id="jobDescription"
            name="jobDescription"
            placeholder="Jobb informasjon"
            v-model="job.content"
            required
        ></textarea>
            </div>
        </div>

        <!-- Button -->
        <div class="field">
            <label class="label" for="job-add"></label>
            <div class="control">
                <button
                    v-on:click="addJob"
                    id="job-add"
                    name="job-add"
                    class="button is-primary"
                >Add sponsor</button>
            </div>
        </div>
    </div>
</template>

<script>
  import axios from "axios";
  export default {
    name: 'AddJob',
    data(){
      return{
        job:{
          title: "",
          content:"",
          url:"",
        }
      }
    },
    methods:{
        addJob(){
          var token = localStorage.getItem("token");

          var config = {
            headers: { Authorization: "bearer " + token }
          };

          var bodyParamters = {
            title: this.job.title,
            url: this.job.url,
            content: this.job.content
          };
          console.log(bodyParamters);
          var self = this;
          axios
            .post("/api/job", bodyParamters, config)
            .then(function(response) {
              console.log(response.data);
              self.$router.push("/jobs");
            })
            .catch(function(error) {
              console.log(error);
            });
        }
    }
  };
</script>

<style scoped>

</style>
