<template>
  <div class="sponsor">
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
export default {
  name: "Sponsorblock",
  props: {
    sponsor: Object
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
