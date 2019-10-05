<template>
    <img class="imageWidget" :src="image">
</template>

<script>
  import axios from "axios";
  export default {
    name: 'imageWidget',
    props:{
      imageId:String,
    },
    data(){
      return{
        image: ''
      }
    },
    created() {
      const self = this;
      axios
        .get("/api/image/64/"+this.$props.imageId)
        .then(function (response) {
          self.image = 'data:image/png;base64,'+self.image.concat(response.data)
        })
        .catch(function (error) {
          console.log(error.response);
        });
    }
  };
</script>

<style scoped>

</style>
