<template>
    <router-view />
</template>


<script>
    import Login from "./components/Login.vue";

    export default {
        components: { Login }
    };
</script>


<style>
    .container {
        max-width: 600px;
        margin: 40px auto;
        font-family: Arial, sans-serif;
    }

    .card {
        padding: 20px;
        margin-top: 20px;
        border-radius: 8px;
        background-color: #ffd800;
    }
</style>
