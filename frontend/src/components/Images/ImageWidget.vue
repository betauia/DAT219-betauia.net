<template>
    <img class="imageWidget" :src="image">
</template>

<script>
  import axios from"@/axios.js";
  export default {
    name: 'imageWidget',
    props:{
      imageId:String,
    },
    watch:{
      imageId: function(newVal){
        if(newVal == '' || newVal == null)return;
        const self = this;
        axios
          .get("/api/image/64/"+newVal)
          .then(function (response) {
            self.image = 'data:image/png;base64,'+self.image.concat(response.data)
          })
          .catch(function (error) {
            console.log(error.response);
          });
      }
    },
    data(){
      return{
        image: ''
      }
    },
    created() {
      if(this.$props.imageId == null)return;

      //alert(this.$props.imageId)
      if(this.$props.imageId == ''|| this.$props.imageId == null)return;

      const self = this;
      axios
        .get("/api/image/64/"+this.$props.imageId)
        .then(function (response) {
          self.image = 'data:image/png;base64,'+self.image.concat(response.data)
        })
        .catch(function (error) {
          console.log(error.response);
        });
    },
  };
</script>

<style scoped>

</style>
