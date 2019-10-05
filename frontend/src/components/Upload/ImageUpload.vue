<template>
    <div id="image-upload" class="mt-0">
        <v-container grid-list-xl>
            <image-input v-model="image">
                <div slot="activator">
                    <div size="150px" v-ripple v-if="!image" class="grey lighten-3 mb-3">
                        <v-btn>Click to add image</v-btn>
                    </div>
                    <v-avatar size="150px" v-ripple v-else class="mb-3">
                        <img :src="image.imageURL" alt="avatar">
                    </v-avatar>
                </div>
            </image-input>
            <v-slide-x-transition>
                <div v-if="image && saved === false">
                    <v-btn class="primary" @click="uploadImage" :loading="saving">Save Image</v-btn>
                </div>
            </v-slide-x-transition>
        </v-container>
    </div>
</template>

<script>
import ImageInput from './ImageInput.vue';
import axios from"@/axios.js";

export default {
  name: 'ImageUpload',
  data() {
    return {
      image: null,
      saving: false,
      saved: false,
    };
  },
  components: {
    ImageInput,
  },
  watch: {
    image: {
      handler() {
        this.saved = false;
      },
      deep: true,
    },
  },
  methods: {
    uploadImage() {
      console.log(this.image);
      var formData = new FormData();
      var imageFile = this.image.file;
      formData.append("image",imageFile[0]);
      axios
        .post("/api/image", formData,{
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })
        .then(function (response) {
          console.log(response.data);
        })
        .catch(function (error) {
          console.log(error);
        });

      this.saving = true;
      setTimeout(() => this.savedAvatar(), 1000);
    },
    savedAvatar() {
      this.saving = false;
      this.saved = true;
    },
  },
};
</script>

<style>
    #image-upload {
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
