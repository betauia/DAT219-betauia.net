<template>
  <div class="Posts">
    <div class="columns is-mobile is-multiline is-centered">
      <div class="column is-three-quarters is-offset-is-narrow">
        <div>
          <div class="card padding">
            <header>
              <p class="card-header-title">{{post.title}}</p>
            </header>
            <div>
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
