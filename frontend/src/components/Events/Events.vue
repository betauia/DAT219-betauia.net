<template>
  <div class="Events">
    <div class="column padding">
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
