<template>
    <div class="logout">
        <div class="is-1"> You have been logged out from your account.</div>
    </div>
</template>

<script>
export default {
    name: 'Logout',
    created() {
        localStorage.removeItem("token");
        this.isLoggedIn = false;
        this.$router.push("/");
        this.$forceUpdate();
    }
}
</script>

<style>

</style>
