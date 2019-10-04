<template>
    <div id="buyTicket" class="container is-center">
        <div class="card">
            <div class="card-header">
                <h1 class="card-header-title title is-1">
                    Confirm purchase
                </h1>
            </div>
            <div class="media-content">
                <div class="label">Selected Seats:
                    <div class="content">seat:
                        <span v-for="seat of seats" v-bind:key="seat">{{seat.number}}<span v-if="seats.length" text="."></span></span>
                    </div>
                </div>
                <div class="content">
                    <p>Choose phone number for payment</p>
                    <input id="phoneNumber" type="text" v-model="ticket.phoneNumber" required>
                    <p>Total price: {{ticket.amount}}</p>
                </div>
                <img src="@/assets/img/Betal med Vipps - 250px@3x.png" class="button is-paddingless" @click="initiateVippsPayment"/>
            </div>
            <div class="card-footer">
                <div class="is-italic">Billettene til BetaLAN er forhåndskjøpte
                    og du som kunden vil ikke motta tjenesten før BetaLAN er
                    gjennomført i sin helhet. Ved å klikke Betal med vipps
                    vil du automatisk godta disse vilkårene og
                    vår. </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'TicketInit',
  components: {
  },
  data() {
    return {
      ticket: {},
      seats: {},
    };
  },
  created() {
    const { id } = this.$route.params;
    const self = this;
    const token = localStorage.getItem('token');
    const config = {
      headers: { Authorization: `bearer ${token}` },
    };

    axios
      .get(`/api/ticket/get/${id}`, config)
      .then((response) => {
        console.log(response);
        self.ticket = response.data;
        self.seats = response.data.eventSeats;
      })
      .catch((error) => {
        console.log(error.response);
      });
  },
  methods: {
    initiateVippsPayment() {
      const { id } = this.$route.params;
      const self = this;
      const token = localStorage.getItem('token');
      const config = {
        headers: { Authorization: `bearer ${token}` },
      };

      const bodyParam = {
        id,
        mobileNumber: this.ticket.phoneNumber,
      };

      console.log(bodyParam);
      axios
        .post('/api/ticket/initiatepayment', bodyParam, config)
        .then((response) => {
          console.log(response.data);
          window.location = response.data;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
  },
};
</script>

<style scoped>

</style>
