<template>
  <form class="form-horizontal padding center" enctype="multipart/form-data">
    <div class="is-1 title">Add Post</div>

    <fieldset>
      <!-- Text input-->
      <div class="field">
        <label class="label" for="blog-title">Title</label>
        <div class="control">
          <input
            id="blog-title"
            name="blog-title"
            type="text"
            placeholder="Bekk Prog &amp; Pils"
            class="input is-primary"
            required
          >
        </div>
      </div>

      <!-- Text input-->
      <div class="field">
        <label class="label" for="blog-Description">Description</label>
        <div class="control">
          <input
            id="blog-Description"
            name="blog-Description"
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
        <label class="label" for="blog-content">Content</label>
        <div class="control">
          <textarea class="textarea is-primary"
            id="blog-content"
            name="blog-content"
            required></textarea>
        </div>
      </div>

      <!-- File Button -->

      <label class="label" for="blog-upload">Upload a picture</label>
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
      </div>
      <!-- Button -->
      <div class="field">
        <label class="label" for="blog-publish"></label>
        <div class="control">
          <button id="blog-publish" name="blog-publish" class="button is-primary">Publish</button>
        </div>
      </div>
    </fieldset>
  </form>
</template>

<script>
export default {};
</script>

<style>
</style>
