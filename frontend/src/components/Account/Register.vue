<template>
  <section class="padding center">

    <b-field label="Firstname">
        <b-input :value=firstname placeholder="kevin"></b-input>
    </b-field>

    <b-field label="Lastname">
        <b-input :value=lastname placeholder="bacon"></b-input>
    </b-field>

    <b-field label="Email" type="is-danger" message="This email is invalid">
      <b-input type="email"
      :value="username"
      placeholder="kevin@bacon.com"
      maxlength="30"></b-input>
    </b-field>

    <b-field label="Username" type="is-danger" message="This Username is invalid">
      <b-input type="username"
      :value="username"
      placeholder="mrbacon"
      maxlength="30"></b-input>
    </b-field>

    <b-field
      label="Password"
      type="is-danger"
      :message="['Password is too short', 'Password must have at least 8 characters']">
      <b-input :value="password"
      type="password"
      placeholder="bacon15mylyf3"
      maxlength="120"></b-input>
    </b-field>

    <b-field
      label="Password Again"
      type="is-danger"
      :message="['Password is too short', 'Password must have at least 8 characters']">
      <b-input :value="again_password"
      type="password"
      placeholder="bacon15mylyf3"
      maxlength="120"></b-input>
    </b-field>

    <div class="is-3 padding">Insert Recaptcha here for auth of humanoids</div>

    <p class="control">
      <button class="button is-primary">Register</button>
    </p>
  </section>
</template>

<script>
export default {
};
</script>
