<template>
  <div id="rolePanel">
    <SimpleTable :rows="roles" :nowrap="true" :searchable="true" @rowClick="getRole"></SimpleTable>
  </div>
</template>

<script>
import axios from"@/axios.js";
import { SimpleTable } from "simple-table-vue";

export default {
  components: {
    SimpleTable
  },
  data() {
    return {
      roles: []
    };
  },
  methods: {
    getRole(role) {
      console.log(role.id);
      this.$router.push("/admin/roleinfo/" + role.id);
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
      .get("/api/role", config)
      .then(function(response) {
        self.roles = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>
