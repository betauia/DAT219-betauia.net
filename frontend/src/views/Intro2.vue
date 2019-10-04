<template>
    <v-container>
        <v-card>
            <BoardMemebers></BoardMemebers>
        </v-card>
    </v-container>

</template>

<script>
    import BoardMemebers from '@/components/BoardMemebers.vue';
    export default {
        name: "Intro2.vue",
        components: {
            BoardMemebers,
        },
    }
</script>

<style scoped>

</style>
