<template>
  <div class="form-horizontal padding center" enctype="multipart/form-data">
    <div class="is-1 title">Add sponsor</div>
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
          required
        ></textarea>
      </div>
    </div>

    <!-- Button -->
    <div class="field">
      <label class="label" for="sponsor-add"></label>
      <div class="control">
        <button
          v-on:click="addSponsor"
          id="sponsor-add"
          name="sponsor-add"
          class="button is-primary"
        >Add sponsor</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {};
  },
  methods: {
    addSponsor() {
      var token = localStorage.getItem("token");
      var title = document.querySelector("input[name=sponsorTitle]").value;
      var url = document.querySelector("input[name=sponsorUrl]").value;
      var description = document.querySelector(
        "textarea[name=sponsorDescription]"
      ).value;

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var bodyParamters = {
        title: title,
        url: url,
        description: description
      };
      var self = this;
      axios
        .post("/api/sponsor", bodyParamters, config)
        .then(function(response) {
          console.log(response.data);
          self.$router.push("/sponsors/" + response.data.id);
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>
