<template>
    <div id="image-upload" class="mt-0">
        <v-container grid-list-xl>
            <img :src="image.imageURL" alt="avatar" v-if="image">
            <image-input v-model="image">
                <div slot="activator">
                    <div size="150px" v-if="!image" class="grey lighten-3 mb-3">
                        <b-button class="is-light is-large is-outlined is-inverted">Click to add image</b-button>
                    </div>
                </div>
            </image-input>
            <image-input v-model="image">
                <div slot="activator">
                    <div size="150px" v-if="image" class="grey lighten-3 mb-3">
                        <b-button>Change image</b-button>
                    </div>
                </div>
            </image-input>
        </v-container>
    </div>
</template>

<script>
import ImageInput from './ImageInput.vue';
import axios from"@/axios.js";

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
    async uploadImage() {
      return await this.axiosResponse().then(response=>{
        return response;
      });
    },
    async axiosResponse(){
      if(this.image == null){
        return ""
      }

      var formData = new FormData();
      var imageFile = this.image.file;
      formData.append("image",imageFile[0]);
      return  await axios
      .post("/api/image", formData,{
        headers: {
          'Content-Type': 'multipart/form-data'
       }
       })
        .then(function (response) {
          return response.data;
        })
        .catch(function (error) {
          return error.response;
        });
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
