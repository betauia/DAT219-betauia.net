<!-- 
Title = title;
SubTitle = subTitle;
Description = description;
Content = content;
EventTime = eventTime;
Author = author;
Image = image;
Atendees = atendees;
IsPublic = isPublic;
-->

<template>
  <div class="form-horizontal padding center" enctype="multipart/form-data">
    <div class="is-1 title">Add Event</div>

<!-- Text input-->
<div class="field">
  <label class="label" for="title">Title</label>
  <div class="control">
    <input id="title" name="title" type="text" placeholder="Betalan #420" class="input " required="">
    
  </div>
</div>

<!-- Text input-->
<div class="field">
  <label class="label" for="description">Description</label>
  <div class="control">
    <input id="description" name="description" type="text" placeholder="Semesterets feeteste LAN" class="input " required="">
    
  </div>
</div>

<!-- Textarea -->
<div class="field">
  <label class="label" for="content">Content</label>
  <div class="control">                     
    <textarea class="textarea" id="content" name="content"></textarea>
  </div>
</div>

<!-- Select Basic -->
<div class="field">
  <label class="label" for="eventIs">Is Event Public</label>
  <div class="control">
  	<div class="select">
	    <select id="eventIs" name="eventIs" class="">
	      <option>Yes</option>
	      <option>No</option>
	    </select>
	</div>
  </div>
</div>

<!-- Button -->
<div class="field">
  <label class="label" for="publish"></label>
  <div class="control">
    <button id="publish" name="publish" class="button is-primary">Publish</button>
  </div>
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
        .post("/api/event/add", bodyParamters, config)
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
