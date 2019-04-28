<template>
    <div class="post">
        <h3 id="title">{{post.title}}</h3>
        <p id="content">{{post.content}}</p>
        <p id="date">Last edited at: {{post.lastEditDate}}</p>
    </div>
</template>

<script>
export default {
    name:'BlogPost',
    props:{
        post: Object,
    },
};
</script>

<style lang="scss">
.post{
    background-color: aqua;
    width: 50%;
    #content{
        font-size: 120%;
    }
    #title{
        font-size: 250%;
        background-color: grey;
        text-align: center;
    }
    #date{
        font-size: 80%;
        text-align: right;
    }
}
</style>
