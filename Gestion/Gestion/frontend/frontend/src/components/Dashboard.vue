<template>
    <div class="dashboard">
        <h1>Dashboard</h1>
        <p>Bienvenido, {{ user.nombre }}</p>

        <div class="menu">
            <button @click="$router.push('/cursos')">Gestionar Cursos</button>
            <button @click="$router.push('/evaluaciones')">Gestionar Evaluaciones</button>
            <button @click="$router.push('/inscripciones')">Gestionar Inscripciones</button>
            <button @click="$router.push('/recursos')">Gestionar Recursos</button>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      user: {}
    }
  },

  mounted() {
    this.user = JSON.parse(localStorage.getItem("user"));
  }
}
</script>

<style>
    .menu {
        display: flex;
        flex-direction: column;
        gap: 10px;
    }

    button {
        padding: 12px;
        background: dodgerblue;
        border: none;
        color: white;
        cursor: pointer;
    }
</style>
