<!--figure class="image is-128x128">
<img class="is-rounded" src="https://bulma.io/images/placeholders/128x128.png">
</figure-->

<template>
 <div class="BoardMembers">
  <div class="columns">
   <div class="column is-3 center">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/Anders_Godstad_(Leder).jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Anders Godstad</li>
                <li class="MemeberPosition">Head</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3 center">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/Theodor_Middleton_(Nestleder).jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Theodor Middleton</li>
                <li class="MemeberPosition">Deputy Head</li>
            </ul>
        </figcaption>
    </figure>
    </div>
  </div>
   <div class="columns">
   <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/placeholder.png">
        <figcaption>
            <ul>
                <li class="MemeberName">Vinh Huu Vu</li>
                <li class="MemeberPosition">Banking & Finance Committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/Ingrid_Louise_Husevåg-Kristensen_(Bedriftskomité).jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Ingrid Louise Husevåg-Kristensen</li>
                <li class="MemeberPosition">Business Relations Committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/Jon_Inntveit_(Eventkomité).jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Jon Inntveit</li>
                <li class="MemeberPosition">Social Event Committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
    <div class="column is-3">
    <figure class="image is-256x256">
        <img class="is-rounded" src="../assets/img/board/Adrian_Risholm_(BetaLAN).jpg">
        <figcaption>
            <ul>
                <li class="MemeberName">Adrian Risholm</li>
                <li class="MemeberPosition">BetaLAN Committee</li>
            </ul>
        </figcaption>
    </figure>
    </div>
   </div>
       <div class="columns">
           <div class="column is-3 center">
               <figure class="image is-256x256">
                   <img class="is-rounded" src="../assets/img/board/placeholder.png">
                   <figcaption>
                       <ul>
                           <li class="MemeberName">Even Eilertsen</li>
                           <li class="MemeberPosition">Beta Security Committee</li>
                       </ul>
                   </figcaption>
               </figure>
           </div>
           <div class="column is-3 center">
               <figure class="image is-256x256">
                   <img class="is-rounded" src="../assets/img/board/placeholder.png">
                   <figcaption>
                       <ul>
                           <li class="MemeberName">TBD</li>
                           <li class="MemeberPosition">Programming & Development Committee</li>
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
