<template>
  <footer class="footer">
    <div class="container">
      <div class="columns">
        <div class="column is-3 left">
          <ul>
            <li>
              Discord:
              <a href="https://discord.gg/UQCPkZD">https://discord.gg/UQCPkZD</a>
            </li>
            <li>
              Email:
              <a href="mailto:post@betauia.net">post@betauia.net</a>
            </li>
            <br>
            <!-- TODO: fix icons and add css to post-address class -->
            <div class="post-address">
              <li>
                <strong>Post og Faktura</strong>
              </li>
              <li>Bluebox, Beta</li>
              <li>Jon Lilletuns vei 9</li>
              <li>4879 Grimstad</li>
            </div>
          </ul>
        </div>
        <div class="column is-6 content has-text-centered center">
          <div class="copyright">
            Copyright &copy; 2008 - {{ (new Date()).getFullYear() }}
            Beta Engineering &amp; Technology Association.
          </div>
          <div class="social">
            <b-icon pack="fab" icon="facebook"></b-icon>
            <b-icon pack="fab" icon="github"></b-icon>
            <b-icon pack="fab" icon="twitch"></b-icon>
          </div>
        </div>
        <div class="column is-3 right">
          <ul>
            <li>
              <router-link to="/privacy">Privacy policy</router-link>
            </li>
            <li>
              <router-link to="/disclaimer">Disclaimer</router-link>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </footer>
</template>
<script>

export default {
};
</script>

<style lang="scss" scoped>
.footer {
  background-color: #b9ddff;
}

a {
  color: #0085ff;
}
.copyright {
  margin-top: 10px;
  font-size: 12px;
}
.social {
  margin-top: 15px;
  a {
    color: #ffffff;
    &:not(:first-child) {
      margin-left: 10px;
    }
  }
}
.right {
  text-align: right;
}
</style>
