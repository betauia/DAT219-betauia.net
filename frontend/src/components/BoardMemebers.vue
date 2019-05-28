<!--figure class="image is-128x128">
<img class="is-rounded" src="https://bulma.io/images/placeholders/128x128.png">
</figure-->

<template>
 <div class="BoardMembers">
  <div class="columns">
   <div class="column is-3 center">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/dummyimg.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Kristoffer Solli</li>
                <li class="MemeberPosition">Leder</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3 center">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/even.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Even Eilertsen</li>
                <li class="MemeberPosition">Nestleder</li>
            </ul>
        </figcaption>
    </figure>
    </div>
  </div>
   <div class="columns">
   <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/dummyimg.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Christer Mathisen</li>
                <li class="MemeberPosition">Økonomi</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/dummyimg.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Aslak Skailand</li>
                <li class="MemeberPosition">BedKom</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/dummyimg.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Martin Bråten</li>
                <li class="MemeberPosition">EventKom</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/dummyimg.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Mattias  Coelho</li>
                <li class="MemeberPosition">Betalan Ansvarlig</li>
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
