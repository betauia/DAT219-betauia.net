<template>
    <div>
<div class="column is-two-thirds is-centered">
    <div class="tile is-ancestor">
    <div class="tile is-parent">
        <router-link to="/admin/addsponsor">
        <article class="tile is-child box">
            <p class="title">Add Sponsor</p>
            <p class="subtitle">Add a sponsor of an event or the website</p>
        </article>
        </router-link>
    </div>
    <div class="tile is-parent">
        <router-link to="/admin/sponsors">
        <article class="tile is-child box">
            <p class="title">Edit Sponsor</p>
            <p class="subtitle">Edit the details of a sponsor</p>
        </article>
        </router-link>
    </div>
    <div class="tile is-parent">
        <router-link to="/admin/users">
            <article class="tile is-child box">
                <p class="title">User Manager</p>
                <p class="subtitle">View/edit/delete users on the website</p>
            </article>
        </router-link>
    </div>
        <div class="tile is-parent">
            <router-link to="/admin/roles">
                <article class="tile is-child box">
                    <p class="title">Role Manager</p>
                    <p class="subtitle">View and edit the roles on the website</p>
                </article>
            </router-link>
        </div>
</div>
    <div class="tile is-ancestor">
        <div class="tile is-vertical">
            <div class="tile">
                <div class="tile is-parent">
                    <router-link to="/admin/seatmapeditor">
                        <article class="tile is-child box">
                            <p class="title">Add Seatmap</p>
                            <p class="subtitle">Make a new seating map for an upcoming event</p>
                        </article>
                    </router-link>
                </div>
                <div class="tile is-parent">
                    <router-link to="/admin/seatmaps">
                        <article class="tile is-child box">
                            <p class="title">Edit Seatmap</p>
                            <p class="subtitle">Edit and existing seating map</p>
                        </article>
                    </router-link>
                </div>
                <div class="tile is-parent">
                    <router-link to="/admin/blog/add">
                        <article class="tile is-child box">
                            <p class="title">Add Post</p>
                            <p class="subtitle">Add a blog post to the news tab</p>
                        </article>
                    </router-link>
                </div>
                <div class="tile is-parent">
                    <router-link to="/admin/blog/edit">
                        <article class="tile is-child box">
                            <p class="title">Edit Post</p>
                            <p class="subtitle">Edit an existing blog post</p>
                        </article>
                    </router-link>
                </div>
            </div>
            <div class="tile">
                <div class="tile is-parent">
                    <router-link to="/admin/event/add">
                        <article class="tile is-child box">
                            <p class="title">Add Event</p>
                            <p class="subtitle">Add a new event to post on events, here you can select seating maps and other options to your event</p>
                        </article>
                    </router-link>
                </div>
                <div class="tile is-parent">
                    <router-link to="/admin/event">
                        <article class="tile is-child box">
                            <p class="title">Edit Events</p>
                            <p class="subtitle">Edit an existing event, here you get a list of previously posted events</p>
                        </article>
                    </router-link>
                </div>
                <div class="tile is-parent">
                    <router-link to="/admin/addjob">
                        <article class="tile is-child box">
                            <p class="title">Add Job</p>
                            <p class="subtitle">Add a new job to post on jobs</p>
                        </article>
                    </router-link>
                </div>
                <div class="tile is-parent">
                    <router-link to="/admin/jobs">
                        <article class="tile is-child box">
                            <p class="title">Edit Jobs</p>
                            <p class="subtitle">Edit an existing job, here you get a list of previously posted jobs</p>
                        </article>
                    </router-link>
                </div>
            </div>
        </div>
    </div>

    <div class="tile is-ancestor notification is-light">
        <div class="tile is-parent">
            <article class="tile is-child">
                <p class="title">Instructions</p>
                <p class="subtitle">Click on the tiles above or the menu items on the left to select what to do in the admin panel</p>
            </article>
        </div>
    </div>
    </div>
    </div>
</template>

<script>
    export default {
        name: "AdminDashboard.vue"
    }
</script>

<style scoped>

</style>
