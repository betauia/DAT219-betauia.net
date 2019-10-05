<template>
    <div class="form-horizontal padding center addevent" enctype="multipart/form-data">
        <div class="is-1 title">Edit Event</div>

        <image-upload-widget ref="imageupload"></image-upload-widget>

        <!-- Text input-->
        <div class="field">
            <label class="label" for="title">Title</label>
            <div class="control">
                <input
                    id="title"
                    name="title"
                    type="text"
                    class="input is-primary"
                    v-model="event.eventModel.title"
                    required
                >
            </div>
        </div>

        <!-- Text input-->
        <div class="field">
            <label class="label" for="description">Description</label>
            <div class="control">
                <input
                    id="description"
                    name="description"
                    type="text"
                    class="input is-primary"
                    v-model="event.eventModel.description"
                    required
                >
            </div>
        </div>

        <!-- Textarea -->
        <div class="field">
            <label class="label" for="content">Content</label>
            <div class="control">
                <textarea class="textarea is-primary" id="content" name="content">{{event.eventModel.content}}</textarea>
            </div>
        </div>

        <!-- Select Basic -->
        <div class="field">
            <label class="label" for="eventIs">Is Event Public</label>
            <div class="control">
                <div class="select">
                    <select id="eventIs" name="eventIs" v-model="event.eventModel.isPublic">
                        <option>true</option>
                        <option>false</option>
                    </select>
                </div>
            </div>
        </div>


        <div class="columns">
            <div class="column is-half">
                <div class="field" id="startdate">
                    <label class="label" for="eventIs">Startdate</label>
                    <div class="control">
                        <datetime
                            type="datetime"
                            v-model="startdate"
                            input-class="my-class"
                            value-zone="Europe/Oslo"
                            format="dd-MM-yyyy HH:mm"
                            zone="Europe/Oslo"
                            :phrases="{ok: 'Continue', cancel: 'Exit'}"
                            :hour-step="1"
                            :minute-step="5"
                            :week-start="1"
                            use24-hour
                            auto
                        ></datetime>
                    </div>
                </div>
            </div>
            <div class="field" id="enddate">
                <label class="label" for="eventIs">Enddate</label>
                <div class="control">
                    <datetime
                        type="datetime"
                        v-model="enddate"
                        input-class="my-class"
                        value-zone="Europe/Oslo"
                        format="dd-MM-yyyy HH:mm"
                        zone="Europe/Oslo"
                        :phrases="{ok: 'Continue', cancel: 'Exit'}"
                        :hour-step="1"
                        :minute-step="5"
                        :min-datetime="startdate"
                        :week-start="1"
                        use24-hour
                        auto
                    ></datetime>
                </div>
            </div>
        </div>
        <div>
            <div>
                <div>
                    <div class="eventOptions">
                        <label class="label">Choose event options</label>
                        <div class="columns">
                            <div class="column is-one-third">Has Sponsor
                                <b-button :style="sponsorStyle" @click="hasSponsorClick">Click me</b-button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="field" v-if="hasSponsor.state==true">
            <label class="label" for="sponsor">Added sponsors</label>
            <div v-for="sponsor of selectedSponsors">
                <b-button class="addSponsorB has-background-success" v-on:click="removeSponsor(sponsor)">
                    {{sponsor}}
                </b-button>
            </div>
            <label class="label">Sponsors to choose from</label>
            <div v-for="sponsor of sponsors">
                <b-button class="addSponsorB has-background-danger" v-on:click="addSponsor(sponsor)">
                    {{sponsor}}
                </b-button>
            </div>
        </div>

        <span>
        <button
            v-on:click="updateEvent"
            id="savebutton"
            name="savebutton"
            class="button is-info"
        >Save changes</button>
        </span>
        <span>
        <button
            v-on:click="deleteEvent"
            id="deletebutton"
            name="deletebutton"
            class="button is-danger"
        >Delete event</button>
        </span>
    </div>
</template>

<script>
import axios from 'axios';
import ImageUploadWidget from "../Upload/ImageUploadWidget";
  export default {
    name: 'EventDetail',
      components: {ImageUploadWidget},
      data(){
      return{
        sponsors: [],
        seatmaps: [],
        event:{},
        selectedSeatmap: null,
        selectedSponsors:[],
        isBookable:{
          state:false,
          color:"red",
        },
        hasSeatMap:{
          state:false,
          color:"red",
        },
        hasSponsor:{
          state:false,
          color:"red",
        },
      }
    },
    created() {
      const id = this.$route.params.id;
      const self = this;
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };

      axios
        .get("/api/event/"+id)
        .then(function (response) {
          console.log(response.data);
          self.event = response.data;
          self.startdate = getParsedTime(response.data.eventModel.startdate);
          self.enddate = getParsedTime(response.data.eventModel.enddate);
          if(response.data.sponsors.length >0){
            response.data.sponsors.forEach(function (a) {
              self.selectedSponsors.push(a.id);
            });
            self.hasSponsor.state = true;
            self.hasSponsor.color = "green";
          }
        })
        .catch(function (error) {
          console.log(error.response);
        });

      axios
        .get("/api/sponsor", config)
        .then(function(response) {
          console.log(self.selectedSponsors);
          response.data.forEach(function (a) {
            if(!self.selectedSponsors.includes(a.id)) self.sponsors.push(a.id);
          });
          console.log(self.sponsors)
        })
        .catch(function(error) {
          console.log(error);
        });

      axios
        .get("/api/seatmap", config)
        .then(function(response) {
          self.seatmaps = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });


      function getParsedTime(input) {
        const date = input.split(" ")[0];
        const time = input.split(" ")[1];
        const ndate = date.split("-")[2] + "-" + date.split("-")[1] + "-" +date.split("-")[0];
        var ntime = "";
        switch (time.split(":")[1]) {
          case "00":
            ntime = "T22:" +  time.split(":")[1] + ":00.000Z";
            break;
            case "01":
            ntime = "T23" + time.split(":")[1] + ":00.000Z";
            break;
          default:
            ntime = "T" + (parseInt(time.split(":")[0])-2).toString() + ":" + time.split(":")[1] + ":00.000Z";
        }
        return ndate + ntime;
      }
    },
    methods:{
        async updateEvent(){
          var image = await this.$refs.imageupload.uploadImage();
          var token = localStorage.getItem("token");
          var title = document.querySelector("input[name=title]").value;
          var description = document.querySelector("input[name=description]").value;
          var content = document.querySelector("textarea[name=content]").value;
          const startdatedoc = document.querySelector('#startdate');
          const enddatedoc = document.querySelector('#enddate');
          const enddate = enddatedoc.querySelector('.vdatetime-input').value;
          const startdate = startdatedoc.querySelector('.vdatetime-input').value;
          var config = {
            headers: { Authorization: "bearer " + token }
          };

          var eventModel = {
            id: this.event.eventModel.id,
            title: title,
            description: description,
            content: content,
            isPublic: this.ispublic,
            enddate:enddate,
            startdate:startdate,
            image: image,
          };

          var bodyParameters = {
            eventModel:eventModel,
            sponsors:[],
          };


          if(this.hasSponsor.state == true){
            if (this.selectedSponsors.length >0) {
              bodyParameters.sponsors = this.selectedSponsors;
            }
          }

          console.log(bodyParameters);
          axios
            .put("/api/event/"+this.event.eventModel.id,bodyParameters, config)
            .then(function(response) {
              console.log(response.data);
            })
            .catch(function(error) {
              console.log(error.response);
            });
        },
      deleteEvent(){
        var token = localStorage.getItem("token");
        var config = {
          headers: {Authorization: "bearer " + token}
        };
        axios
          .delete("/api/event/"+this.event.eventModel.id,config)
          .then(function (response) {
            console.log(response.data);
            location.reload();
          })
          .catch(function(error){
            console.log(error.response);
          })
      },
      isBookableClick(){
        this.isBookable.state = !this.isBookable.state;
        if(this.isBookable.state == true){
          this.isBookable.color = "green";
        }else{
          this.isBookable.color = "red";
        }

        if(this.hasSeatMap.state == true){
          this.hasSeatMap.state = false;
          this.hasSeatMap.color = "red"
        }
      },
      hasSponsorClick(){
        this.hasSponsor.state = !this.hasSponsor.state;
        if(this.hasSponsor.state == true){
          this.hasSponsor.color = "green";
        }else{
          this.hasSponsor.color = "red";
        }
      },
      hasSeatMapClick(){
        this.hasSeatMap.state = !this.hasSeatMap.state;
        if(this.hasSeatMap.state == true){
          this.hasSeatMap.color = "green";
        }else{
          this.hasSeatMap.color = "red";
        }
        if(this.isBookable.state == true){
          this.isBookable.state = false;
          this.isBookable.color = "red"
        }
      },
      addSponsor(id){
        this.selectedSponsors.push(this.sponsors.filter(function(x){return x ==id})[0]);
        this.sponsors = this.sponsors.filter(function(x){return x !=id});
        console.log(this.selectedSponsors)
      },
      removeSponsor(id){
        this.sponsors.push(this.selectedSponsors.filter(function(x){return x ==id})[0]);
        this.selectedSponsors = this.selectedSponsors.filter(function(x){return x !=id});
      },
    },
    computed:{
      sponsorStyle(){
        return "backgroundColor:"+this.hasSponsor.color;
      },
      bookableStyle(){
        return "backgroundColor:"+this.isBookable.color;
      },
      seatmapStyle(){
        return "backgroundColor:"+this.hasSeatMap.color;
      }
    }
  };
</script>

<style scoped>
    .sidebyside50 {
        width: 50%;
        vertical-align: top;
        display: inline-block;
    }
    .sidebyside30 {
        width: 30%;
        vertical-align: top;
        display: inline-block;
        background-color: #6c6c6c;
        margin: 1.5%;
        color: white;
        text-align: center;
    }
    .eventOptions {

    }
    .sidebyside30 button {
        width: 50%;
    }
    .field {
    }
    .chooser {
        width: 100%;
    }
    #publish {
        width: 100%;
    }
    .eventOptions input{
        margin-left: 1%;
        margin-right: 1%;
    }
    .addSponsorB{
        width: 100%;
    }
</style>
