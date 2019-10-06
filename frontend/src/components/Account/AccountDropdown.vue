<template>
    <b-dropdown
        v-model="navigation"
        position="is-bottom-left"
        aria-role="menu">
        <a
            class="navbar-item"
            slot="trigger"
            role="button">
            <span>Account</span>
            <b-icon icon="menu-down"></b-icon>
        </a>
        <div class="is-primary">
            <b-dropdown-item custom aria-role="menuitem">
                Logged in successfully <b></b>
            </b-dropdown-item>
            <hr class="dropdown-divider">
            <b-dropdown-item value="home" aria-role="menuitem">
                <router-link to="/account/info">
                    <b-icon icon="home"></b-icon>
                    Account Information
                </router-link>
            </b-dropdown-item>
            <b-dropdown-item value="products" aria-role="menuitem">
                <router-link to="/account/accountorders">
                    <b-icon icon="cart"></b-icon>
                    Orders/Tickets
                </router-link>
            </b-dropdown-item>
            <b-dropdown-item value="blog" aria-role="menuitem">
                <router-link to="/account/edit/${user.id}">
                    <b-icon icon="book-open"></b-icon>
                    Edit Information
                </router-link>
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

</style>
