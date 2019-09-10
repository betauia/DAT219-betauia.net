<template>
  <div class="sponsorDetailed">
    <div class="Columns padding">
      <div clas="card">
        <header class="card-header">
          <p class="card-hearder-title">{{sponsor.title}}</p>
        </header>
        <div class="card-content">
          <div class="description">{{sponsor.description}}</div>
          <div class="italic">{{sponsor.url}}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      sponsor: Object
    };
  },
  created() {
    var id = this.$route.params.id;
    var self = this;
    console.log(id);
    axios
      .get("/api/sponsor/" + id)
      .then(function(response) {
        self.sponsor = response.data;
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

