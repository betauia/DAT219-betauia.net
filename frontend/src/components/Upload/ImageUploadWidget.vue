<template>
    <div id="image-upload" class="mt-0">
        <v-container grid-list-xl>
            <img :src="image.imageURL" alt="avatar" v-if="image">
            <image-input v-model="image">
                <div slot="activator">
                    <div size="150px" v-if="!image" class="grey lighten-3 mb-3">
                        <v-btn>Click to add image</v-btn>
                    </div>
                </div>
            </image-input>
            <image-input v-model="image">
                <div slot="activator">
                    <div size="150px" v-if="image" class="grey lighten-3 mb-3">
                        <v-btn>Change image</v-btn>
                    </div>
                </div>
            </image-input>
        </v-container>
    </div>
</template>

<script>
import ImageInput from './ImageInput.vue';
import axios from "axios";

export default {
  name: 'ImageUploadWidget',
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
      const self = this;
      var ret;
      axios
        .post("/api/image", formData,{
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })
        .then(function (response) {
          console.log(response);
          self.ret = response.data;
        })
        .catch(function (error) {
          console.log(error.response);
          self.ret = error.response;
        });
      this.saving = true;
      setTimeout(() => this.savedAvatar(), 1000);
      return ret;
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
        margin-top: 60px;
    }
</style>
