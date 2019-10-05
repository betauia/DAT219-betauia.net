<template>
    <div id="orders">
        <tr>
            <th>Order number</th>
            <th>Event</th>
            <th>Time purchased</th>
            <th>Status</th>
            <td>Details</td>
        </tr>
        <tr v-for="order of orders" v-bind:key="order" v-if="order.id != null">
            <td>{{order.id}}</td>
            <td>{{order.eventTitle}}</td>
            <td>{{order.status}}</td>
            <td>{{order.status}}</td>
            <td>
                <a @click="detailClick(order.id)">See orderdetails</a>
            </td>
        </tr>
    </div>
</template>

<script>
import axios from"@/axios.js";
export default {
    name: 'AccountOrders',
    data() {
        return {
          orders: [],
        };
      },
      created() {
        var token = localStorage.getItem("token");

        var config = {
          headers: { Authorization: "bearer " + token }
        };
        var self = this;
        console.log(localStorage.getItem("token"));
        axios
          .get("/api/ticket/get",config)
          .then(function(response) {
            console.log(response["data"]);
            self.orders = response["data"];
          })
          .catch(function(error) {
            console.log(error.response.data);
          });
      },
      methods:{
        detailClick(id){
          this.$router.push("/ticketdetails/"+id);
        }
      }
  };
</script>

<style scoped>
    table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

    td, th {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 8px;
    }

    tr:nth-child(even) {
        background-color: #dddddd;
    }

    a{
        color: blue;
    }

    a:link{
        color:blue;
    }
    a:visited{
        color: blue;
    }
    a:hover{
        color: black;
    }
</style>
