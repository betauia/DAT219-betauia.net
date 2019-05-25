<template>
  <div id="sponsors">
    <ul v-if="sponsors && sponsors.length">
      <div v-for="sponsor of sponsors" v-bind:key="sponsor">
        <sponsor v-bind:sponsor="sponsor"></sponsor>
      </div>
    </ul>
    <ul v-if="errors && errors.length">
      <li v-for="error of errors" v-bind:key="error">{{error.message}}</li>
    </ul>
  </div>
</template>

<script>
import axios from "axios";
import SponsorBlock from "@/components/Sponsors/Sponsorblock.vue";

export default {
  components: {
    sponsor: SponsorBlock
  },
  data() {
    return {
      sponsors: [],
      errors: []
    };
  },

  created() {
    axios
      .get("/api/sponsor")
      .then(response => {
        this.sponsors = response.data;
        console.log(this.sponsors);
      })
      .catch(e => {
        this.errors.push(e);
      });
  }
};
</script>
