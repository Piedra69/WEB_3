<template>
    <div>
        <h2>Gestión de Cursos</h2>

        <button @click="obtenerCursos">Actualizar lista</button>

        <ul>
            <li v-for="curso in cursos" :key="curso.id">
                {{ curso.nombre }}
            </li>
        </ul>
    </div>
</template>

<script>
import cursoService from "../../services/cursoService";

export default {
  data() {
    return {
      cursos: []
    }
  },

  mounted() {
    this.obtenerCursos();
  },

  methods: {
    async obtenerCursos() {
      const res = await cursoService.obtenerCursos();
      this.cursos = res.data;
    }
  }
}
</script>
