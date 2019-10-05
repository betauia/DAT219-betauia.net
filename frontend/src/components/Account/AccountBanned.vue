<template>
    <div class="columns is-mobile is-multiline is-centered">
    <div class="column is-three-quarters is-offset-is-narrow">
        <div>
            <div class="card padding">
                <header>
                    <p class="card-header-title">The account is banned</p>
                </header>
                <div>
                    <div class="content">
                        If you think this was a mistake please contact customer support.
                        <br>
                        The account will be deleted shortly after the ban.
                        <br>
                        <a href = "mailto: abc@example.com">support@betauia.net</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>

<script>
  export default {
    name: 'AccountBanned'
  };
</script>

<style scoped>
.content{
    margin-left: 1%;
}
a{
    color: blue;
}
</style>
