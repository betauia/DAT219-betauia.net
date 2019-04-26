<template>
    <center>
        <section>

            
        <form method="POST" action="/api/login">
        <div class="login">
            <div class="subtitle has-text-centered is-3">Super secure login</div>
            <b-input type="email"
                placeholder="user@example.com"
                maxlength="30">
            </b-input>

            <b-input type="password"
                value="hunter2"
                password-reveal>
            </b-input>
            <hr>
            <button type="submit" class="btn btn-primary">Login</button>

        </div>
        </form>
    </section>
    </center>
</template>

<style lang="scss" scoped>

.login {
    width: 30%;
    padding: 30px;
}

</style>


<script>
    export default {
        data() {
            return {
                name: 'John Silver'
            }
        }
    }
</script>
