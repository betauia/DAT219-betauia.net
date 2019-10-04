<!--figure class="image is-128x128">
<img class="is-rounded" src="https://bulma.io/images/placeholders/128x128.png">
</figure-->

<template>
 <div class="BoardMembers">
  <div class="columns">
   <div class="column is-3 center">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/kristoffer.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Kristoffer Solli</li>
                <li class="MemeberPosition">Head</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3 center">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/even.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Even Eilertsen</li>
                <li class="MemeberPosition">Deputy head</li>
            </ul>
        </figcaption>
    </figure>
    </div>
  </div>
   <div class="columns">
   <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/christer.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Christer Mathisen</li>
                <li class="MemeberPosition">Banking and finance committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/placeholder.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Anders Godstad</li>
                <li class="MemeberPosition">Business relations committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/martin.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Martin Bråten</li>
                <li class="MemeberPosition">Social event committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/placeholder.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Mattias A. Coelho</li>
                <li class="MemeberPosition">Betalan Head</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   </div>
  </div>
</template>

<style lang="scss" scoped>

.MemeberName{
    color: black;
    font-size: 24px;
    font-weight: 200;
    text-justify: auto;
    text-align: center;
}
.MemeberPosition {
    color: #73bcff;
    text-align: center;
    text-justify: auto;
    font-size: 22px;
}
</style>

<script>
export default {
  name: 'BoardMembers',
};
</script>
