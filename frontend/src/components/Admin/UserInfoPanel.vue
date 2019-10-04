<template>
  <div id="userPanel">
    <user v-bind:user="user"></user>
    <SimpleTable :rows="claims" :nowrap="true"></SimpleTable>
      <div class="tickets">
          <tr>
              <th>Order number</th>
              <th>Event</th>
              <th>Time purchased</th>
              <th>Status</th>
              <td>Details</td>
          </tr>
          <tr v-for="order of tickets" v-bind:key="order">
              <td>{{order.id}}</td>
              <td>{{order.eventTitle}}</td>
              <td>{{order.status}}</td>
              <td>{{order.status}}</td>
              <td>
                 <a>See orderdetails</a>
              </td>
          </tr>
      </div>
  </div>
</template>

<script>
import axios from "axios";
import UserInfo from "@/components/Admin/Userinfo.vue";
import { SimpleTable } from "simple-table-vue";

export default {
  components: {
    user: UserInfo,
    SimpleTable
  },
  data() {
    return {
      user: Object,
      claims: [],
      tickets:[]
    };
  },
  created() {
    var id = this.$route.params.id;

    if (id == null) {
      this.$router.push("/admin/users");
    }
    var token = localStorage.getItem("token");
    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var self = this;
    axios
      .get("/api/allclaims/user/" + id, config)
      .then(function(response) {
        self.claims = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error.response.data);
      });

    axios
      .get("/api/user/" + id, config)
      .then(function(response) {
        self.user = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        var code = error.response.data;
        console.log(code);
      });

    axios
      .get("/api/ticket/user/get/"+id,config)
      .then(function (response) {
        console.log(response.data);
        self.tickets = response.data;
      })
      .catch(function (error) {
        console.log(error.data);
      });
  }
};
</script>


<style>
#userPanel {
  background-color: grey;
}
.simple-table-vue table {
  width: 100%;
}
.simple-table-vue tbody {
  width: 100%;
}
.simple-table-vue {
  background: #707070;
}
.simple-table-vue .table {
  margin: 0;
}
.simple-table-vue tfoot,
.simple-table-vue tfoot * {
  max-height: 0;
  height: 0;
}
.simple-table-vue .header {
  border: 0px solid gray;
}
.simple-table-vue .header tr {
  background: gray;
}
.simple-table-vue [data-component="Tabla"] tr:hover {
  background: #eee;
}
.simple-table-vue tr.rowSelected {
  background: #ddd;
}
.simple-table-vue td {
  border: 1px solid #ddd;
  box-sizing: border-box;
  font-size: 16px;
}
roles .simple-table-vue #scrollableDiv {
  height: 20%;
}
.simple-table-vue .header td {
}
.simple-table-vue .header td div {
  background: white;
  border: 1px solid lightgray;
  padding: 2px;
  box-sizing: border-box;
}
.simple-table-vue td.nowrap {
  white-space: nowrap;
}
.simple-table-vue [data-order="asc"] {
  background: blue;
}
.simple-table-vue [data-order="desc"] {
  background: green;
}
.simple-table-vue table {
  border-collapse: collapse;
}
.simple-table-vue td[contenteditable="true"] {
  cursor: text;
}
.simple-table-vue td[contenteditable="true"]:hover {
  border-color: gray;
}
.simple-table-vue td[contenteditable="true"]:focus {
  background: white;
  color: #444;
}
.simple-table-vue button {
  padding: 0;
  margin: 2px;
  height: 30px;
  float: left;
  background: transparent;
  border: 0;
  cursor: default;
  opacity: 0.7;
}
.simple-table-vue button svg {
  opacity: inherit;
}
.simple-table-vue button:hover {
  opacity: 0.9;
}
.simple-table-vue input[type="checkbox"] {
  margin: 0;
  padding: 0;
}

.tickets table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

.tickets td, th {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}

.tickets tr:nth-child(even) {
    background-color: #dddddd;
}

.tickets a{
    color: blue;
}

.tickets a:link{
    color:blue;
}
.tickets a:visited{
    color: blue;
}
.tickets a:hover{
    color: black;
}
</style>
