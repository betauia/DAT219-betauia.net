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
