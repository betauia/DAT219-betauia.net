<template>
  <div class="Intro">
    <section class="intro hero is-fullheight header-img">
      <div class="hero-body">
        <div class="header container is-widescreeen has-text-centered">
          <h1 class="title is-1 fontBlack">Beta Engineering &amp; Technology Association</h1>
          <p class="subtitle is-4 fontBlack">Linjeforrening for Dataingeniør studenter ved Universitetet i Agder</p>
        </div>
      </div>
    </section>
    <section class="about hero is-fullheight is-link">
      <div class="hero-body">
        <div class="container is-widescreen">
          <h1 class="title is-2 " >About Beta</h1>
          <p class="subtitle is-3">
              BETA ENGINEERING & TECHNOLOGY ASSOCIATION is a student organization that covers students studying computer engineering and IT at the University of Agder, campus Grimstad. BETA is managed by volunteering students, with the goal of bringing students and companies closer together. In addition to this we want to create a thriving social community for the students by holding social and educational events ourselves.
          </p>
        </div>
      </div>
    </section>
      <!--
    <section class="help hero is-fullheight is-light">
      <div class="hero-body">
        <div class="container is-widescreeen">
          <h1 class="title is-2 fontBlack">Har du lyst til å være med å hjelpe til?</h1>
          <p class="subtitle fontBlack">lyst til bli med i styret for BETA ?</p>
        </div>
      </div>
      -->
    </section>
    <section class="board-members hero is-fullheight is-bold">
      <div class="hero-body">
        <div class="container is-widescreeen">
          <h1 class="title has-text-centered is-1 fontStyret">Administration Committee</h1>
          <p class="subtitle">
            <BoardMemebers></BoardMemebers>
          </p>
        </div>
      </div>
    </section>
  </div>
</template>

<style lang="scss">
//https://picsum.photos/128/128/?random
//.has-bg-img { background: url('/assets/images/img.jpg')center center; background-size:cover; }
.tower-img {
  background: url("../assets/img/tower.jpg") center center;
  background-size: cover;
}

.header-img {
  background: url("../assets/img/header.jpg") center center;
  background-size: cover;
}

.retro-img {
  background: url("../assets/img/retro.jpg") center center;
  background-size: cover;
}

.fontBlack {
  color: #ffffff;
  text-shadow: #0f0f0f 2px 2px 3px;
}

.fontStyret {
  color: black;
  text-shadow:#0085ff 1px 1px 1px;
}

#nav {
  padding: 26px;
  height: 30px;
  a {
    font-weight: bold;
    color: #8bc7ff;
    font-size: 18px;
    &.router-link-exact-active {
      color: #003d74;
    }
  }
}
</style>


<script>
import BoardMemebers from '@/components/BoardMemebers.vue';

export default {
  name: 'Intro',
  components: {
    BoardMemebers,
  },
};
</script>
