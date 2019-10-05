<template>
  <div class="Events">
    <div class="column padding">
      <div class="card">
        <header class="card-header">
          <p class="card-header-title">{{event.title}}</p>
        </header>
        <div class="card-content">
          <div class="content">
            <ul>
              <li>subTitle: {{event.subTitle}}</li>
              <li>Description: {{event.description}}</li>
              <li>Content: {{event.content}}</li>
              <li>EventTime: {{event.eventTime}}</li>
            </ul>
            <br>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from"@/axios.js";

export default {
  data() {
    return {
      event: Object,
      errors: []
    };
  },

  created() {
    var id = this.$route.params.id;
    axios
      .get("/api/event/" + id)
      .then(response => {
        this.event = response.data;
        console.log(response.data);
      })
      .catch(e => {
        this.errors.push(e);
      });
  }
};
</script>

<style>
</style>
