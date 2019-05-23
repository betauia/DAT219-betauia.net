<template>
  <div id="userPanel">
    <SimpleTable :rows="users" :nowrap="true" :searchable="true" @rowClick="getUser"></SimpleTable>
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
      users: []
    };
  },
  methods: {
    getUser(user) {
      console.log(user.id);
      localStorage.setItem("id", user.id);
      this.$router.push("/admin/userinfo");
    }
  },
  created() {
    var token = localStorage.getItem("token");

    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var self = this;
    console.log(token);
    axios
      .get("/api/user", config)
      .then(function(response) {
        self.users = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error);
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
</style>

