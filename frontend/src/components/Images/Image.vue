<template>
    <div id="image">
        <img :src="image">
        <image-widget v-bind:image-id="id"></image-widget>
    </div>
</template>

<script>
  import axios from"@/axios.js";
  export default {
    name: 'Image',
    data(){
      return{
        image: '',
        id:''
      }
    },
    created() {
      const id = this.$route.params.id;
      this.id = id;
      const self = this;
      axios
        .get("/api/image/64/"+id)
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
