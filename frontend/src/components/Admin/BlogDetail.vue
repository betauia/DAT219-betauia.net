<template>
    <div class="blogdetailed">
        <div class="field">
            <label class="label" for="blogTitle">Title</label>
            <div class="control">
                <input
                    id="blogTitle"
                    name="blogTitle"
                    type="text"
                    class="input is-primary"
                    v-model="post.title"
                    required
                >
            </div>
        </div>

        <!-- Text input-->
        <div class="field">
            <label class="label" for="blogDescription">Description</label>
            <div class="control">
                <input
                    id="blogDescription"
                    name="blogDescription"
                    type="text"
                    class="input is-primary"
                    v-model="post.summary"
                    required
                >
            </div>
        </div>

        <!-- Textarea -->
        <div class="field">
            <label class="label" for="blogContent">Content</label>
            <div class="control">
                <textarea class="textarea is-primary" id="blogContent" name="blogContent" required>{{post.content}}</textarea>
            </div>
        </div>

        <div class="field">
            <label class="label">Creation date {{post.creationDate}}</label>
            <label class="label">Last edit time {{post.lastEditDate}}</label>
        </div>

        <span>
        <button
            v-on:click="updateBlog"
            id="savebutton"
            name="savebutton"
            class="button is-info"
        >Save changes</button>
        </span>
        <span>
        <button
            v-on:click="deleteBlog"
            id="savebutton"
            name="savebutton"
            class="button is-danger"
        >Delete blog post</button>
        </span>
    </div>
</template>

<script>
import axios from "axios";

  export default {
    name: 'BlogDetail',
    data(){
      return{
        post:{},
      }
    },
    created() {
      const id = this.$route.params.id;
      const self = this;
      axios
        .get("/api/blog/"+id)
        .then(function (response) {
          console.log(response.data);
          self.post = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
        })
    },
    methods:{
      updateBlog(){

      },
      deleteBlog(){

      }
    }
  };
</script>

<style scoped>
    .italic {
        padding: 5px;
        font-style: italic;
        font-size: 12px;
    }
    .summary {
        font-weight: bold;
    }
</style>
