<template>
  <div class="User">
    <div class="Input">
      <label>Username:</label>
      <br>
      <input type="text" v-model="user.userName">
    </div>
    <div class="Input">
      <label>Email:</label>
      <br>
      <input type="text" v-model="user.email">
    </div>
    <div class="Input">
      <label>Firstname:</label>
      <br>
      <input type="text" v-model="user.firstName">
    </div>
    <div class="Input">
      <label>Lastname:</label>
      <br>
      <input type="text" v-model="user.lastName">
    </div>
    <div class="Input">
      <label>Id:</label>
      <br>
      <input type="text" v-model="user.id" disabled>
    </div>
    <div class="Input">
      <label>Active account:</label>
      <br>
      <input type="text" v-model="user.active">
    </div>
    <div class="Input">
      <label>VerifiedEmail:</label>
      <br>
      <input type="text" v-model="user.verifiedEmail">
    </div>
    <div class="Input">
      <label>Force logout:</label>
      <br>
      <input type="text" v-model="user.forceLogout">
    </div>
  </div>
</template>

<script>
export default {
  name: "User",
  props: {
    user: Object
  }
};
</script>

<style>
.User {
  background-color: rgb(167, 167, 192);
  margin: 12px;
  border-block-color: black;
  border-width: thin;
  border-style: solid;
  color: black;
}
input {
  font-size: 16px;
  width: 100%;
}
.Input {
  margin-left: 10px;
  margin-bottom: 4px;
  border-block-color: black;
  border-width: thin;
  border-style: solid;
  color: black;
  width: 50%;
}
</style>