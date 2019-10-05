<template>
    <div id="buyTicket" class="columns is-centered">
        <div class="card column is-8">
            <div class="card-header">
                <h1 class="card-header-title title is-1">
                    Bekreft Kjøpshandling
                </h1>
            </div>
            <div class="media-content is-grouped-centered">
                <div class="label">Setenummer:
                    <div class="content">
                        <div v-for="seat of seats" v-bind:key="seat">{{seat.number}}
                        </div>
                    </div>
                </div>
                <div class="content">
                    <p>Total price: {{ticket.amount}}</p>
                </div>

            </div>
            <div class="card-footer column">
                <div class="is-italic">Billettene til BetaLAN er forhåndskjøpte
                    og du som kunden vil ikke motta tjenesten før BetaLAN er
                    gjennomført i sin helhet. Ved å klikke 'Aksepter betingelser'
                    vil du automatisk godta disse vilkårene og <SalgsBetingelserBetaside></SalgsBetingelserBetaside>
                    vår.
                </div>
            </div>
            <div class="card-footer-item is-center column is-half">
                <p class="label">Choose phone number for payment</p>
                <input
                    class="input is-primary"
                    id="phoneNumber"
                    type="text"
                    v-model="ticket.phoneNumber"
                    required
                >
            </div>
            <div class="card-footer-item">
                <img src="@/assets/img/Betal med Vipps - 250px@3x.png" class="button is-paddingless is-medium" @click="initiateVippsPayment"/>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import SalgsBetingelserBetaside from '../Legal/SalgsBetingelserBetaside.vue';

export default {
  name: 'TicketInit',
  components: {
      SalgsBetingelserBetaside,
  },
  data() {
    return {
      ticket: {},
      seats: {},
    };
  },
    created() {
        const id = this.$route.params.id;
        const self = this;
        var token = localStorage.getItem("token");
        var config = {
            headers: { Authorization: "bearer " + token }
        };

        axios
            .get("/api/ticket/get/"+id,config)
            .then(function (response) {
                console.log(response);
                self.ticket = response.data;
                self.seats = response.data.eventSeats;
            })
            .catch(function (error) {
                console.log(error.response);
            });
    },
    methods:{
        initiateVippsPayment(){
            const id = this.$route.params.id;
            const self = this;
            var token = localStorage.getItem("token");
            var config = {
                headers: { Authorization: "bearer " + token }
            };

            var bodyParam = {
                id:id,
                mobileNumber: this.ticket.phoneNumber,
            };

            console.log(bodyParam);
            axios
                .post("/api/ticket/initiatepayment",bodyParam,config)
                .then(function (response) {
                    console.log(response.data);
                    window.location = response.data;
                })
                .catch(function (error) {
                    console.log(error.response);
                });
        }
    }
};
</script>


<style scoped>

</style>
