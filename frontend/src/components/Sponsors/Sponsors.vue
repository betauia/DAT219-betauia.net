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
import axios from"@/axios.js";
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
    var self = this;
    axios
      .get("/api/sponsor")
      .then(response => {
        self.sponsors = response.data;
        console.log(self.sponsors);
      })
      .catch(e => {
        this.errors.push(e);
      });
  }
};
</script>
