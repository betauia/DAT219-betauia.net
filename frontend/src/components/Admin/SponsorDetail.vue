<template>
    <div class="form-horizontal padding center" enctype="multipart/form-data">
        <div class="is-1 title">Edit sponsor {{sponsor.id}}</div>
        <!-- Text input-->
        <div class="field">
            <label class="label" for="sponsorTitle">Title</label>
            <div class="control">
                <input
                    id="sponsorTitle"
                    name="sponsorTitle"
                    type="text"
                    placeholder="Atea"
                    class="input is-primary"
                    v-model="sponsor.title"
                    required
                >
            </div>
        </div>

        <!-- Text input-->
        <div class="field">
            <label class="label" for="sponsorUrl">Url</label>
            <div class="control">
                <input
                    id="sponsorUrl"
                    name="sponsorUrl"
                    type="text"
                    placeholder="atea.no"
                    class="input is-primary"
                    v-model="sponsor.url"
                    required
                >
            </div>
        </div>

        <!-- Textarea -->
        <div class="field">
            <label class="label" for="sponsorDescription">Description</label>
            <div class="control">
        <textarea
            class="textarea is-primary"
            id="sponsorDescription"
            name="sponsorDescription"
            v-model="sponsor.description"
            required
        ></textarea>
            </div>
        </div>

        <span>
        <button
            v-on:click="editSponsor"
            id="savebutton"
            name="savebutton"
            class="button is-info"
        >Save changes</button>
        </span>
        <span>
        <button
            v-on:click="deleteSponsor"
            id="savebutton"
            name="savebutton"
            class="button is-danger"
        >Delete sponsor</button>
        </span>
    </div>
</template>

<script>
  import axios from "axios";

  export default {
    data() {
      return {
        sponsor:{},
      };
    },
    created(){
      const id = this.$route.params.id;
      const self = this;
      axios
        .get("/api/sponsor/"+id)
        .then(function (response) {
          console.log(response.data);
          self.sponsor = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods: {
      editSponsor() {
        var token = localStorage.getItem("token");

        var config = {
          headers: { Authorization: "bearer " + token }
        };

        var bodyParamters = {
          id:this.sponsor.id,
          title: this.sponsor.title,
          url: this.sponsor.url,
          description: this.sponsor.description
        };
        var self = this;
        axios
          .put("/api/sponsor/"+this.sponsor.id, bodyParamters, config)
          .then(function(response) {
            console.log(response.data);
            self.$router.push("/sponsors/" + response.data.id);
          })
          .catch(function(error) {
            console.log(error);
          });
      },
      deleteSponsor(){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        const self = this;
        axios
          .delete("/api/sponsor/"+this.sponsor.id,config)
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
