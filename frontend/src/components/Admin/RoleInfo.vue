<template>
  <div class="role">
    <div class="roleblock">
      <p>Role id: {{role.id}}</p>
      <p>Role name: {{role.name}}</p>
    </div>
    <SimpleTable :rows="claims" :nowrap="true"></SimpleTable>
    <SimpleTable :rows="users" :nowrap="true" @rowClick="goToUser"></SimpleTable>
    <b-modal :active.sync="addUserModalActive" :width="1000" scroll="keep">
      <SimpleTable :rows="addUsers" :norwap="true" :searchable="true" @rowClick="addUser"></SimpleTable>
    </b-modal>
    <button class="button-is-primary is-medium" @click="activateUserPanel">Add user</button>
  </div>
</template>

<script>
import axios from "axios";
import { SimpleTable } from "simple-table-vue";

export default {
  components: {
    SimpleTable
  },
  data() {
    return {
      role: Object,
      claims: [],
      users: [],
      addUsers: [],
      addUserModalActive: false
    };
  },
  methods: {
    activateUserPanel() {
      var id = this.$route.params.id;

      if (id == null) {
        this.$router.push("/admin/roles");
      }
      var token = localStorage.getItem("token");
      var config = {
        headers: { Authorization: "bearer " + token }
      };
      var self = this;

      axios
        .get("/api/user/nrole/" + id, config)
        .then(function(response) {
          self.addUsers = response.data;
          console.log(response.data);
        })
        .catch(function(errors) {
          console.log(error.data);
        });
      this.addUserModalActive = true;
    },
    addUser(user) {
      var userid = user.id;

      var token = localStorage.getItem("token");

      var config = {
        headers: { Authorization: "bearer " + token }
      };

      var self = this;
      axios
        .post("/api/user/role/" + userid + "/" + this.$route.params.id, config)
        .then(function(response) {
          self.addUserModalActive = false;
          self.addUsers = null;
          self.$forceUpdate();
        })
        .catch(function(errors) {
          console.log(errors.data);
        });
    },
    goToUser(user) {
      var userid = user.id;
      this.$router.push("/admin/userinfo/" + user.id);
    }
  },
  created() {
    var id = this.$route.params.id;

    if (id == null) {
      this.$router.push("/admin/roles");
    }
    var token = localStorage.getItem("token");
    var config = {
      headers: { Authorization: "bearer " + token }
    };
    var self = this;
    axios
      .get("/api/claim/role/" + id, config)
      .then(function(response) {
        self.claims = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        console.log(error.response.data);
      });

    axios
      .get("/api/role/" + id, config)
      .then(function(response) {
        self.role = response.data;
        console.log(response.data);
      })
      .catch(function(error) {
        var code = error.response.data;
        console.log(code);
      });

    axios
      .get("/api/user/role/" + id, config)
      .then(function(response) {
        self.users = response.data;
      })
      .catch(function(error) {
        console.log(error);
      });
  }
};
</script>

<style>
.roleblock {
  background-color: rgb(190, 190, 190);
  padding: 10px;
}
</style>
