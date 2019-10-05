<template>
  <div id="posts">
    <ul v-if="posts && posts.length">
      <div v-for="post of posts" v-bind:key="post">
        <post v-bind:post="post"></post>
      </div>
    </ul>
    <ul v-if="errors && errors.length">
      <li v-for="error of errors" v-bind:key="error">{{error.message}}</li>
    </ul>
  </div>
</template>


<script>
import axios from"@/axios.js";
import BlogPost from "@/components/Blog/BlogPost.vue";

export default {
  components: {
    post: BlogPost
  },
  data() {
    return {
      posts: [],
      errors: []
    };
  },

  created() {
    axios
      .get("/api/blog")
      .then(response => {
        this.posts = response.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
  }
};
</script>
