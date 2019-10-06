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
    <div id="addEvent">
        <div class="form-horizontal padding center addevent" enctype="multipart/form-data">
            <div class="is-1 title">Add Event</div>

            <div class="column">
                <ImageUploadWidget ref="imageupload"></ImageUploadWidget>
            </div>
            <!-- Text input-->
            <div class="field">
                <label class="label" for="title">Title</label>
                <input
                    id="title"
                    name="title"
                    type="text"
                    placeholder="Your Title Here"
                    class="input is-primary"
                    required
                />
            </div>

            <!-- Text input-->
            <div class="field">
                <label class="label" for="description">Description</label>
                <div class="control">
                    <input
                        id="description"
                        name="description"
                        type="text"
                        placeholder="A short description of the event."
                        class="input is-primary"
                        required
                    />
                </div>
            </div>

            <!-- Textarea -->
            <div class="field">
                <label class="label" for="content">Content</label>
                <div class="control">
                    <textarea class="textarea is-primary" id="textcontent" name="content"></textarea>
                </div>
            </div>

            <!-- Select Basic -->
            <div>
                <div class="field">
                    <label class="label" for="eventIs">Public Event</label>
                    <div class="control">
                        <div class="select">
                            <select class="select is-primary" id="eventIs" name="eventIs" v-model="ispublic">
                                <option>True</option>
                                <option>False</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="columns">
                    <div class="column is-half" id="startdate">
                        <b-field label="Start date" for="eventIs">
                            <datetime
                                placeholder="Click to set a date..."
                                type="datetime"
                                v-model="startdate"
                                input-class="my-class"
                                value-zone="Europe/Oslo"
                                format="dd-MM-yyyy HH:mm"
                                zone="Europe/Oslo"
                                class="is-primary"
                                :phrases="{ok: 'Continue', cancel: 'Exit'}"
                                :hour-step="1"
                                :minute-step="5"
                                :week-start="1"
                                use24-hour
                            ></datetime>
                        </b-field>
                    </div>
                    <div class="column padding" id="enddate">
                        <!--
                        <b-field label="End date" for="eventIs" v-model="enddate">
                            <b-field>
                                <b-datepicker
                                    placeholder="Type or select a date..."
                                    icon="calendar-today"
                                    editable
                                ></b-datepicker>
                            </b-field>
                            <b-field>
                                <b-timepicker
                                    icon="clock"
                                    editable
                                ></b-timepicker>
                            </b-field>
                        </b-field>
                        -->

                        <span class="control">
                         <b-field
                             label="End date" for="eventIs"
                             icon="calendar-today">
                        <datetime
                            placeholder="Click to set a date..."
                            type="datetime"
                            v-model="enddate"
                            input-class="my-class"
                            value-zone="Europe/Oslo"
                            format="dd-MM-yyyy HH:mm"
                            zone="Europe/Oslo"
                            class="is-primary"
                            :phrases="{ok: 'Continue', cancel: 'Exit'}"
                            :hour-step="1"
                            :minute-step="5"
                            :min-datetime="startdate"
                            :week-start="1"
                            use24-hour
                            auto
                        ></datetime></b-field>
                    </span>

                    </div>
                </div>
            </div>
            <div>
                <div class="eventOptions">
                    <label class="label">Choose event options</label>
                    <div class="columns">
                        <div class="column is-one-third">Has Sponsor
                            <b-button :style="sponsorStyle" @click="hasSponsorClick">Click me</b-button>
                        </div>
                        <div class="column">Is bookable
                            <b-button :style="bookableStyle" @click="isBookableClick">Click me</b-button>
                        </div>
                        <div class="column">Has seatmap
                            <b-button :style="seatmapStyle" @click="hasSeatMapClick">Click me</b-button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="columns">
                <div class="field column is-one-third" v-if="hasSponsor.state==true">
                    <label class="label" for="sponsor">Added sponsors:</label>
                    <div v-for="sponsor of selectedSponsors">
                        <b-button class="addSponsorB has-background-success" v-on:click="removeSponsor(sponsor.id)">
                            {{sponsor.id}}
                        </b-button>
                    </div>
                    <label class="label">Sponsors to choose from</label>
                    <div v-for="sponsor of sponsors">
                        <b-button class="addSponsorB has-background-danger" v-on:click="addSponsor(sponsor.id)">
                            {{sponsor.id}}
                        </b-button>
                    </div>
                </div>

                <div class="column" v-if="isBookable.state==true">
                    <label class="label" for="atendees">Number of possible atendees</label>
                    <section>
                        <b-field>
                            <b-numberinput id="numberAtendees" v-model="numberAtt"></b-numberinput>
                        </b-field>
                    </section>
                </div>

                <!--
                v-for="item in seatmaps"
                      :value="item"
                      v-bind:key="item"
                -->
                <div class="column" v-if="hasSeatMap.state==true">
                    <label class="label" for="seatmap">Select seatmap</label>
                    <div class="select">
                        <select v-model="selectedSeatmap">
                            <option
                                v-for="item in seatmaps"
                                :value="item"
                                v-bind:key="item"
                            >{{item.id}}, with {{item.numSeats}} seats
                            </option>
                            <option v-if="seatmaps.length==0">No stored seat maps</option>
                        </select>
                    </div>
                </div>
            </div>
            <!-- Button -->
            <b-button
                class="is-primary"
                id="publish" name="publish"
                v-on:click="addEvent"
            >Publish
            </b-button>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import {Datetime} from 'vue-datetime';
    import ImageUploadWidget from '../Upload/ImageUploadWidget.vue';
    export default {
        components: {
            datetime: Datetime,
            ImageUploadWidget,
        },
        data() {
            return {
                numberAtt: 30,
                seatmaps: [],
                sponsors: [],
                selectedSeatmap: null,
                ispublic: null,
                isBookable: {
                    state: false,
                    color: 'red',
                },
                hasSeatMap: {
                    state: false,
                    color: 'red',
                },
                hasSponsor: {
                    state: false,
                    color: 'red',
                },
                selectedSponsors: [],
                startdate: '0000-00-00T00:00:00.000Z',
                enddate: '0000-00-00T00:00:00.000Z',
            };
        },
        methods: {
            async addEvent() {
                var image = await this.$refs.imageupload.uploadImage();

                const token = localStorage.getItem('token');
                const title = document.querySelector('input[name=title]').value;
                const description = document.querySelector('input[name=description]').value;
                const content = document.querySelector('#textcontent').value;
                const startdatedoc = document.querySelector('#startdate');
                const enddatedoc = document.querySelector('#enddate');
                const enddate = enddatedoc.querySelector('.vdatetime-input').value;
                const startdate = startdatedoc.querySelector('.vdatetime-input').value;
                const config = {
                    headers: {Authorization: `bearer ${token}`},
                };

                const eventModel = {
                    title,
                    description,
                    content,
                    isPublic: this.ispublic,
                    enddate,
                    startdate,
                    image,
                };

                const bodyParameters = {
                    eventModel,
                    sponsors: [],
                };

                if (this.hasSponsor.state == true) {
                    if (this.selectedSponsors.length > 0) {
                        bodyParameters.sponsors = this.selectedSponsors;
                    }
                }
                if (this.hasSeatMap.state == true) {
                    if (this.selectedSeatmap != null) {
                        bodyParameters.eventModel.seatmapid = this.selectedSeatmap.id;
                    }
                }
                if (this.isBookable.state == true) {
                    const atendees = document.getElementById('numberAtendees').value;
                    bodyParameters.eventModel.MaxAtendees = atendees;
                }

                console.log(bodyParameters);
                const self = this;
                axios
                    .post('/api/event', bodyParameters, config)
                    .then((response) => {
                        self.$router.push('/events/');
                        console.log(response.data);
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            isBookableClick() {
                this.isBookable.state = !this.isBookable.state;
                if (this.isBookable.state == true) {
                    this.isBookable.color = 'green';
                } else {
                    this.isBookable.color = 'red';
                }

                if (this.hasSeatMap.state == true) {
                    this.hasSeatMap.state = false;
                    this.hasSeatMap.color = 'red';
                }
            },
            hasSponsorClick() {
                this.hasSponsor.state = !this.hasSponsor.state;
                if (this.hasSponsor.state == true) {
                    this.hasSponsor.color = 'green';
                } else {
                    this.hasSponsor.color = 'red';
                }
                console.log(this.startdate);
            },
            hasSeatMapClick() {
                this.hasSeatMap.state = !this.hasSeatMap.state;
                if (this.hasSeatMap.state == true) {
                    this.hasSeatMap.color = 'green';
                } else {
                    this.hasSeatMap.color = 'red';
                }
                if (this.isBookable.state == true) {
                    this.isBookable.state = false;
                    this.isBookable.color = 'red';
                }
            },
            addSponsor(id) {
                this.selectedSponsors.push(this.sponsors.filter(x => x.id == id)[0]);
                this.sponsors = this.sponsors.filter(x => x.id != id);
            },
            removeSponsor(id) {
                this.sponsors.push(this.selectedSponsors.filter(x => x.id == id)[0]);
                this.selectedSponsors = this.selectedSponsors.filter(x => x.id != id);
            },
        },
        created() {
            const token = localStorage.getItem('token');
            const config = {
                headers: {Authorization: `bearer ${token}`},
            };

            const self = this;
            axios
                .get('/api/sponsor', config)
                .then((response) => {
                    self.sponsors = response.data;
                    console.log(response.data);
                })
                .catch((error) => {
                    console.log(error);
                });

            axios
                .get('/api/seatmap', config)
                .then((response) => {
                    self.seatmaps = response.data;
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        computed: {
            sponsorStyle() {
                return `backgroundColor:${this.hasSponsor.color}`;
            },
            bookableStyle() {
                return `backgroundColor:${this.isBookable.color}`;
            },
            seatmapStyle() {
                return `backgroundColor:${this.hasSeatMap.color}`;
            },
        },
    };
</script>

<style>
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
        padding: 2px;
    }

    .chooser {
        width: 100%;
    }

    #publish {
        width: 100%;
    }

    .eventOptions input {
        margin-left: 1%;
        margin-right: 1%;
    }

    .addSponsorB {
        width: 100%;
    }
</style>
