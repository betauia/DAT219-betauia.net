<template>
<div class="blog">
<h1 class="title is-1 rigth">Nyheter</h1>
<h3 class="subtitle is-3 rigth">{title}</h3>
<h3 class="subtitle is-3 rigth">{msg}</h3>
<h3 class="subtitle is-3 rigth">{img}</h3>

</div>
</template>

<style lang="scss" scoped>
.rigth {
    text-align: left;
    padding-inline-start: 15%;
}
</style>


<script>
export default {
  name: 'Blog',
};
</script>
