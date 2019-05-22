<template>
  <div class="Posts">
    <div class="columns padding">
      <div class="card">
        <header class="card-header">
          <p class="card-header-title">{{post.title}}</p>
        </header>
        <div class="card-content">
          <h1 class="summary">{{post.summary}}</h1>
          <div class="content">
            {{post.content}}
            <br>
            <div class="italic">Last edited at: {{post.lastEditDate}}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Posts",
  props: {
    post: Object
  }
};
</script>

<style>
.italic {
  padding: 5px;
  font-style: italic;
  font-size: 12px;
}
.summary {
  font-weight: bold;
}
</style>
