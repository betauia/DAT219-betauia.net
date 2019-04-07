<template>
  <footer class="footer">
    <div class="container">
      <div class="columns">
        <div class="column is-3 left">
          <ul>
            <li>Discord: <a href="https://discord.gg/betauia">https://discord.gg/betauia</a></li>
            <li>Email: <a href="mailto:post@betauia.net">post@betauia.net</a></li>
            <br>
            <!-- TODO: fix icons and add css to post-address class -->
            <div class="post-address">
              <li> <strong>Post og Faktura</strong></li>
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
            <a href="https://www.facebook.com/beta.linjeforening" title="Facebook" target="_blank">
              <icon name="brands/facebook-square" scale="3"></icon>
            </a>
            <a href="https://github.com/beta" title="Github" target="_blank">
              <icon name="brands/github-square" scale="3"></icon>
            </a>
            <a href="https://twitch.tv/betauia" title="Twitch" target="_blank">
              <icon name="brands/twitch" scale="3"></icon>
            </a>
          </div>
        </div>
        <div class="column is-3 right">
          <ul>
            <li>
              <router-link to="/privacy-policy">Privacy policy</router-link>
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
import 'vue-awesome/icons/brands/soundcloud';
import 'vue-awesome/icons/brands/youtube-square';
import 'vue-awesome/icons/brands/facebook-square';
import 'vue-awesome/icons/brands/github-square';
import 'vue-awesome/icons/brands/instagram';

export default {

};
</script>

<style lang="scss" scoped>
  a {
    color: #8888ff;
    text-shadow: #8d8d8d 0.1px 0.1px 0;
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
