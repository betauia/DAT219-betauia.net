<template>
  <section class="padding center">
    <b-field label="Email" type="is-danger" message="This email is invalid">
      <b-input type="email"
      :value="username"
      placeholder="bendik@datapor.no"
      maxlength="30"></b-input>
    </b-field>

    <b-field
      label="Password"
      type="is-danger"
      :message="['Password is too short', 'Password must have at least 8 characters']"
    >
      <b-input :value="password" type="password" placeholder="hunter2k" maxlength="120"></b-input>
    </b-field>

    <div class="field" grouped>
      <button class="button is-primary"> Login</button>
      <b-checkbox> Remember Me! </b-checkbox>
      </div>
  </section>
</template>

<script>
export default {
};
</script>
