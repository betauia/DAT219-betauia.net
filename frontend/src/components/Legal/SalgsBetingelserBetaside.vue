<template>
    <span class="modal-active">
        <div class="modal-background" v-on:click="modal-close"></div>
        <div class="modal-content">
            <figure class="image is-3by4">            
                <img src="@/assets/img/salgsbetingelser/SalgsbetingleserBetaSide-1.png"/>
            </figure>
            <figure class="image is-3by4">
                <img src="@/assets/img/salgsbetingelser/SalgsbetingleserBetaSide-2.png"/>
            </figure>
            <figure class="image is-3by4">
                <img src="@/assets/img/salgsbetingelser/SalgsbetingleserBetaSide-3.png"/>
            </figure>
            <figure class="image is-3by4">
                <img src="@/assets/img/salgsbetingelser/SalgsbetingleserBetaSide-4.png"/>
            </figure>
        </div>
        <button class="modal-close is-large" v-on:click="modal-close" aria-label="close"></button>
        </span>
</template>

<script>
export default {

}
</script>

<style>

</style>
