<template>
    <span>
        <span v-on:click="isTermsOfSaleModalActive=true">
            <a>kjøpsavtalen</a>
        </span>

        <b-modal class="is-centered" :active.sync="isTermsOfSaleModalActive"
                 full-screen has-modal-card :can-cancel="true">
            <div class="modal-card">
                <header class="modal-card-head">
                    <p class="modal-card-title">Kjøpsavtale</p>
                </header>
                <section class="modal-card-body">
                    <div>
                        <div class="card-content">
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
                    </div>
                </section>
                <footer class="modal-card-foot">
                    <div class="card-footer-item"></div>
                    <div class="card-footer-item">
                        <b-button class="button" type="button" @click="isTermsOfSaleModalActive=false">Lukk</b-button>
                    </div>
                </footer>
            </div>
        </b-modal>
    </span>

</template>

<script>
export default {
    data() {
        return {
            isTermsOfSaleModalActive: false,
            isAccepted: false,
        };
    },
}
</script>

<style scoped>
a {
    color: blue;
}
</style>
