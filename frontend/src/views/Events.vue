<template>
  <div class="events">
    <template>
      <div id="events">
        <ul v-if="events && events.length">
          <div v-for="event of events" v-bind:key="event">
            <event v-bind:event="event"></event>
          </div>
        </ul>
        <ul v-if="errors && errors.length">
          <li v-for="error of errors" v-bind:key="error">{{error.message}}</li>
        </ul>
      </div>
    </template>
  </div>
</template>


<script>
import axios from"@/axios.js";
import Events from "@/components/Events/Events.vue";

export default {
  components: {
    event: Events
  },
  data() {
    return {
      events: [],
      errors: [],
      free:false,
    };
  },

  created() {
    axios
      .get("/api/event")
      .then(response => {
        this.events = response.data;
        this.events = this.events.reverse();
        console.log(response.data);
      })
      .catch(e => {
        this.errors.push(e);
      });
  }
};
</script>
 
