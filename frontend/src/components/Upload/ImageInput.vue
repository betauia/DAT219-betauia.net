<template>
  <div>
    <!-- slot for parent component to activate the file changer -->
    <div @click="launchFilePicker()">
      <slot name="activator"></slot>
    </div>
    <!-- image input: style is set to hidden and assigned a ref so that it can be triggered -->
    <input type="file"
           ref="file"
           :name="uploadFieldName"
           @change="onFileChange(
          $event.target.name, $event.target.files)"
           style="display:none">
    <!-- error dialog displays any potential error messages -->
    <v-dialog v-model="errorDialog" max-width="300">
      <div class="card">
        <div class="subheading">{{errorText}}</div>
        <div class="is-spaced">
          <button class="is-link" @click="errorDialog = false" flat>Got it!</button>
        </div>
      </div>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: 'image-input',
  data: () => ({
    errorDialog: null,
    errorText: '',
    uploadFieldName: 'file',
    maxSize: 1024,
  }),
  props: {
    // Use "value" to enable using v-model
    value: Object,
  },
  methods: {
    launchFilePicker() {
      this.$refs.file.click();
    },
    onFileChange(fieldName, file) {
      const { maxSize } = this;
      const imageFile = file[0];
      if (file.length > 0) {
        const size = imageFile.size / maxSize / maxSize;
        if (!imageFile.type.match('image.*')) {
          // check whether the upload is an image
          this.errorDialog = true;
          this.errorText = 'Please choose an image file';
        } else if (size > 1) {
          // check whether the size is greater than the size limit
          this.errorDialog = true;
          this.errorText = 'Your file is too big! Please select an image under 1MB';
        } else {
          // Append file into FormData and turn file into image URL
          const formData = new FormData();
          const imageURL = URL.createObjectURL(imageFile);
          formData.append(fieldName, imageFile);

          // Emit the FormData and image URL to the parent component
          this.$emit('input', { formData, imageURL,file });
        }
      }
    },
  },
};
</script>
