<!--figure class="image is-128x128">
<img class="is-rounded" src="https://bulma.io/images/placeholders/128x128.png">
</figure-->

<template>
 <div class="BoardMembers">
  <div class="columns">
   <div class="column">
    <figure class="image is-256x256">
        <img class="is-rounded" src="https://betauia.net/images/styret/2015/Anders.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Anders Refsdal Olsen</li>
                <li class="MemeberPosition">Leder</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column">
    <figure class="image is-256x256">
        <img class="is-rounded" src="https://betauia.net/images/styret/2015/Knut.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Knut Eivind Sandsmark</li>
                <li class="MemeberPosition">Nestleder</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   <div class="column">
    <figure class="image is-256x256">
        <img class="is-rounded" src="https://betauia.net/images/styret/2015/John.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">John Arne Føreland</li>
                <li class="MemeberPosition">PR Ansvarlig</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column">
    <figure class="image is-256x256">
        <img class="is-rounded" src="https://betauia.net/images/styret/2015/Martin.jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Martin Abrahamsen</li>
                <li class="MemeberPosition">BetaLAN Ansvarlig</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   </div>
  </div>
</template>

<style lang="scss" scoped>

.MemeberName{
    color: #ffffff;
    font-size: 29px;
    font-weight: 200;
    text-justify: auto;
    text-align: center;
}
.MemeberPosition {
    color: #dddddd;
    text-align: center;
    text-justify: auto;
    font-size: 26px;
}
</style>

<script>
export default {
  name: 'BoardMembers',
};
</script>
