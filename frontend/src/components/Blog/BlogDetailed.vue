<template>
  <div class="blogdetailed">
    <header class="card-header">
      <p class="card-header-title">{{post.title}}</p>
    </header>
    <div class="card-content">
      <h1 class="summary">{{post.summary}}</h1>
        <image-widget :image-id="post.image"></image-widget>
        <div class="content">
          {{post.content}}
        <br>
        <div class="italic">Last edited at: {{post.lastEditDate}}</div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from"@/axios.js";

export default {
  data() {
    return {
      post: {}
    };
  },
  created() {
    console.log(this.post)
    var id = this.$route.params.id;
    var self = this;
    axios
      .get("/api/blog/" + id)
      .then(function(response) {
        self.post = response.data;
        console.log(response.data)
      })
      .catch(function(error) {
        console.log(error.response.data);
      });
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

