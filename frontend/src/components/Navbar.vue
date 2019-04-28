<template>
  <nav class="navbar mobile is-primary">
    <div class="navbar-brand">
      <a class="navbar-item">
        <router-link to="/">
        <img src="@/assets/img/logo.png">
        </router-link>
      </a>
      <a class="navbar-item">
        <a href="https://tools.uia.no/bitbucket/projects/DAT219G19V/repos/betauia/browse">
        <b-icon pack="fab" icon="gitlab"></b-icon>
        </a>
      </a>
      <a class="navbar-item">
        <router-link to="/blog">Blog</router-link>
      </a>
      <a class="navbar-item">
        <router-link to="/events">Events</router-link>
      </a>
    </div>

    <div class="navbar-menu">
      <div class="navbar-end padding">
        <b-dropdown position="is-bottom-left" aria-role="menu">
          <a class="navbar-item" slot="trigger" role="button">
            <span>Login</span>
            <b-icon icon="menu-down"></b-icon>
          </a>

          <b-dropdown-item aria-role="menu-item" custom paddingless>
            <form action>
              <div class="modal-card" style="width:300px;">
                <section class="modal-card-body">
                  <b-field label="Email">
                    <b-input type="email" placeholder="Your email" required></b-input>
                  </b-field>

                  <b-field label="Password">
                    <b-input type="password" password-reveal
                    placeholder="Your password"
                    required></b-input>
                  </b-field>

                  <b-checkbox>Remember me</b-checkbox>
                </section>
                <footer class="modal-card-foot">
                  <button class="button is-primary">Login</button>
                </footer>
              </div>
            </form>
          </b-dropdown-item>
        </b-dropdown>
      </div>
    </div>
  </nav>
</template>


<script>
export default {};
</script>

<style lang="scss">

</style>
