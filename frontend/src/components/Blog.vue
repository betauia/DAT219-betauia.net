<template>
  <div class="blog">
    <div id="padding">
    <ul v-if="posts && posts.length">
      <li v-for="post of posts" v-bind:key="post">
        <p>
          <strong>{{post.title}}</strong>
        </p>
        <p>{{post.body}}</p>
      </li>
    </ul>

    <ul v-if="errors && errors.length">
      <li v-for="error of errors" v-bind:key="error">{{error.message}}</li>
    </ul>
  </div>
  </div>
</template>

<style lang="scss" scoped>

#padding {
  padding: 20px;
}

.rigth {
  text-align: left;
  padding-inline-start: 15%;
}

</style>


<script>
import axios from 'axios';

export default {
  data() {
    return {
      posts: [],
      errors: [],
    };
  },

  created() {
    axios
      .get('/api/BlogApi')
      .then((response) => {
        this.posts = response.data;
      })
      .catch((e) => {
        this.errors.push(e);
      });
  },
};
</script>
