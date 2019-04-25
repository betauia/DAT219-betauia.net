<template>

<div class="Intro">
  <section class="intro hero is-link is-fullheight-with-navbar has-bg-img fontBlack">
    <div class="hero-body">
    <div class="header container is-widescreeen has-text-centered">
      <h1 class="title is-1"> Beta Engineering &amp; Technology Association </h1>
      <p class="subtitle is-3">
        "insert inspirational quote"
      </p>
    </div>
  </div>
  </section>
<section class="about hero is-fullheight is-primary is-bold">
    <div class="hero-body">
    <div class="container is-widescreen">
      <h1 class="title is-2"> About Beta </h1>
      <p class="subtitle">
        Beta er linjeforeningen for Data Ingeniør Studenter ved Universitetet i Agder.
        Beta er en non-profit organisasjon. Det betyr at Beta driver ikke for å tjene penger,
        men heller bruker fortjenesten sin på dems medlemmer.
        Alle data ingeniør studenter ved UiA er automatisk medlem
        av Beta dersom en ikke aktivt melder seg ut.
      </p>
    </div>
    </div>
</section>
<section class="help hero is-fullheight is-danger is-bold">
    <div class="hero-body">
    <div class="container is-widescreeen">
      <h1 class="title is-2"> Har du lyst til å være med å hjelpe til? </h1>
      <p class="subtitle">
       lyst til bli med i styret for BETA ?
      </p>
    </div>
    </div>
</section>

<section class="board-members hero is-fullheight is-success is-bold">
    <div class="hero-body">
    <div class="container is-widescreeen">
      <h1 class="title has-text-centered is-1"> Styret</h1>
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
.has-bg-img {
  background: url("../assets/img/header.jpg")center center;
  background-size:cover;
  }


 .fontBlack {
    color: #ffffff;
    text-shadow: #0f0f0f 2px 2px 0;
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
