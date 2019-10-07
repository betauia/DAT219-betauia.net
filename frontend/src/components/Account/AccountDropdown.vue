<template>
    <b-dropdown
        v-model="navigation"
        position="is-bottom-left"
        aria-role="menu">
        <a
            class="navbar-item"
            slot="trigger"
            role="button">
            <span class="has-text-white">Account</span>
            <b-icon icon="menu-down"></b-icon>
        </a>
        <div class="label">
            <b-dropdown-item custom aria-role="menuitem">
                Logged in successfully <b></b>
            </b-dropdown-item>
            <hr class="dropdown-divider">
            <b-dropdown-item class="has-text-black" value="home" aria-role="menuitem" href="/account/info">
                    <b-icon icon="home"></b-icon>
                    Account Information
            </b-dropdown-item>
            <b-dropdown-item value="products" aria-role="menuitem" href="/account/accountorders">
                    <b-icon icon="cart"></b-icon>
                    Orders/Tickets
            </b-dropdown-item>
            <b-dropdown-item value="blog" aria-role="menuitem" href="/account/edit/${user.id}">
                    <b-icon icon="book-open"></b-icon>
                    Edit Information
            </b-dropdown-item>
            <hr class="dropdown-divider">
            <b-dropdown-item value="logout" v-on:click="logout" aria-role="menuitem">
                <b-icon icon="logout"></b-icon>
                Logout
            </b-dropdown-item>
        </div>
    </b-dropdown>
</template>

<script>
    export default {
        name: "AccountDropdown.vue",
        data() {
            return {
                navigation: 'home'
            }
        },
        methods: {
            logout() {
                localStorage.removeItem("token");
                console.log("Logged out")
                this.$forceUpdate();
                location.reload();
            }
        }
    }
</script>

<style scoped>
a {
    color: #1b1e21;
}
</style>
