<template>
    <div class="form-horizontal padding center" enctype="multipart/form-data">
        <div class="is-1 title">Edit job {{job.id}}</div>
        <!-- Text input-->
        <div class="field">
            <label class="label" for="jobTitle">Title</label>
            <div class="control">
                <input
                    id="jobTitle"
                    name="jobTitle"
                    type="text"
                    placeholder="Atea"
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
                    placeholder="atea.no"
                    class="input is-primary"
                    v-model="job.url"
                    required
                >
            </div>
        </div>

        <!-- Textarea -->
        <div class="field">
            <label class="label" for="jobDescription">Description</label>
            <div class="control">
        <textarea
            class="textarea is-primary"
            id="jobDescription"
            name="jobDescription"
            v-model="job.content"
            required
        ></textarea>
            </div>
        </div>

        <span>
        <button
            v-on:click="editJob"
            id="savebutton"
            name="savebutton"
            class="button is-info"
        >Save changes</button>
        </span>
        <span>
        <button
            v-on:click="deleteJob"
            id="savebutton"
            name="savebutton"
            class="button is-danger"
        >Delete job</button>
        </span>
    </div>
</template>

<script>
  import axios from"@/axios.js";
  export default {
    name: 'JobDetail',
    data(){
      return{
        job:{},
      }
    },
    created() {
      const id = this.$route.params.id;
      const self = this;
      axios
        .get("/api/job/"+id)
        .then(function(response){
          console.log(response.data);
          self.job = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods: {
      editJob() {
        var token = localStorage.getItem("token");

        var config = {
          headers: { Authorization: "bearer " + token }
        };

        var bodyParamters = {
          id: this.job.id,
          title: this.job.title,
          url: this.job.url,
          content: this.job.content
        };
        var self = this;
        axios
          .put("/api/job/" + this.job.id, bodyParamters, config)
          .then(function (response) {
            console.log(response.data);
            self.$router.push("/jobs");
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      deleteJob() {
        var token = localStorage.getItem("token");
        var config = {
          headers: { Authorization: "bearer " + token }
        };
        const self = this;
        axios
          .delete("/api/job/" + this.job.id, config)
          .then(function (response) {
            console.log(response.data);
            location.reload();
          })
          .catch(function (error) {
            console.log(error.response);
          });
      },
    }
  };
</script>

<style scoped>

</style>
