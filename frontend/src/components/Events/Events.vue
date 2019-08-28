<template>
  <div class="Events">
    <div class="column padding">
<<<<<<< HEAD
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
              <li v-if="event.sponsorId">Event sponsor: {{event.sponsorId}}</li>
            </ul>
            <br>
            <p>{{event.seatMapId}}</p>
            <button v-if="event.seatMapId" v-on:click="buyticket">Buy ticket</button>
=======
      <div class="columns is-mobile is-multiline is-centered">
        <div class="column is-four-fifths is-offset-is-narrow">
          <div class="card">
            <header class="card-header">
              <p class="card-header-title">{{event.title}}</p>
            </header>
            <div class="card-content">
              <div class="content">
                <ul><h1>{{event.subTitle}}</h1>
                  <li>{{event.description}}</li>
                  <li>{{event.content}}</li>
                  <li>Tid publisert: {{event.eventTime}}</li>
                </ul>
                <br>
                <div>
                  <router-link to="/purchase"><button
                    class="button is-primary"
                  >Meld deg på!</button></router-link>
                </div>
              </div>
            </div>
>>>>>>> origin/master
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Events",
  props: {
    event: Object
  },
  created() {
    console.log(this.event);
  },
  methods: {
    buyticket() {
      this.$router.push(
        "/events/seatmap/" + this.event.id + "/" + this.event.seatMapId
      );
    }
  }
};
</script>

<style>
.italic {
  padding: 5px;
  font-style: italic;
  font-size: 12px;
}
</style>
