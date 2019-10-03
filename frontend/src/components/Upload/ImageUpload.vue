<template>
    <div class="file">
        <form id="imageFrom" @submit.prevent="onSubmit" enctype="multipart/form-data">
            <div class="fields">
                <label>Upload Image</label>
                <input type="file" ref="file" @change="onSelect"/>
            </div>
            <div class="fields">
                <button @click="onSubmit">Submit</button>
            </div>
            <div class="message">
                <h3>{{message}}</h3>
            </div>
        </form>
    </div>
</template>
<script>
import axios from 'axios';

export default {
  name: 'ImageUpload',
  data() {
    return {
      file: null,
      message: '',
    };
  },
  methods: {
    onSelect(event) {
      console.log(event);
      const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png'];
      const file = this.$refs.file.files[0];
      this.file = file;
      if (!allowedTypes.includes(file.type)) {
        this.message = 'Only images are allowed!';
      }
      if (file.size > 5000000) {
        this.message = 'Too large! Max image size is 500GB';
      }
    },
    async onSubmit() {
      const formData = new FormData();
      formData.append('file', this.file, this.file.name);
      try {
        await axios.post('../assets/img/uploads', formData, {});
        this.message = 'Uploaded successfully!';
      } catch (e) {
        console.log(e);
        this.message = 'Oops, something screwed up!';
      }
    },
  },
};
</script>
