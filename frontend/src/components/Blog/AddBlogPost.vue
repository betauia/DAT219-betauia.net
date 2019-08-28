<template>
  <div class="form-horizontal padding center" enctype="multipart/form-data">
    <div class="is-1 title">Add Post</div>
    <!-- Text input-->
    <div class="field">
      <label class="label" for="blogTitle">Title</label>
      <div class="control">
        <input
          id="blogTitle"
          name="blogTitle"
          type="text"
          placeholder="Bekk Prog &amp; Pils"
          class="input is-primary"
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
          placeholder="Kom og ta deg en kald en mens du progger
            på hobby prosjektet ditt"
          class="input is-primary"
          required
        >
      </div>
    </div>

    <!-- Textarea -->
    <div class="field">
      <label class="label" for="blogContent">Content</label>
      <div class="control">
        <textarea class="textarea is-primary" id="blogContent" name="blogContent" required></textarea>
      </div>
    </div>

    <!-- File Button -->
    <!-- Upload -->
    <!--label class="label" for="blog-upload">Upload a picture</label>
    <div class="file">
      <label class="file-label">
        <input
          class="file-input is-primary"
          type="file"
          name="blog-upload"
          onchange="if (this.files.length > 0)
            document.getElementById('filename-blog-upload').innerHTML = this.files[0].name;"
        >
        <span class="file-cta">
          <span class="file-icon">
            <i class="fa fa-upload"></i>
          </span>
          <span class="file-label is-primary" id="filename-blog-upload">Choose a file…</span>
        </span>
      </label>
    </div-->
    <!-- Button -->
    <div class="field">
      <label class="label" for="blog-publish"></label>
      <div class="control">
        <button
          v-on:click="addPost"
          id="blog-publish"
          name="blog-publish"
          class="button is-primary"
        >Publish</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {};
  },
  methods: {
    addPost() {
      var token = localStorage.getItem("token");
      var title = document.querySelector("input[name=blogTitle]").value;
      var summary = document.querySelector("input[name=blogDescription]").value;
      var content = document.querySelector("textarea[name=blogContent]").value;

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var bodyParamters = {
        title: title,
        summary: summary,
        content: content
      };
      var self = this;
      axios
        .post("/api/blog", bodyParamters, config)
        .then(function(response) {
          console.log(response.data);
          self.$router.push("/blog/detailed/" + response.data);
        })
        .catch(function(error) {
          console.log(error);
        });
    }
  }
};
</script>
