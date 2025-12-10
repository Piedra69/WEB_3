<template>
    <div class="crud-container">
        <h1>Gestión de Cursos</h1>

        <button class="btn-add" @click="nuevoCurso">+ Nuevo Curso</button>

        <table class="crud-table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Título</th>
                    <th>Descripción</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="curso in cursos" :key="curso.id">
                    <td>{{ curso.id }}</td>
                    <td>{{ curso.titulo }}</td>
                    <td>{{ curso.descripcion }}</td>
                    <td>
                        <button @click="editarCurso(curso)">Editar</button>
                        <button class="btn-delete" @click="borrarCurso(curso.id)">Eliminar</button>
                    </td>
                </tr>
            </tbody>
        </table>

        <curso-form v-if="mostrarModal"
                    :curso="cursoSeleccionado"
                    @cerrar="cerrarModal"
                    @guardado="cargarCursos" />
    </div>
</template>

<script>
import cursoService from "../../services/cursoService";
import CursoForm from "./CursoForm.vue";

export default {
  components: { CursoForm },

  data() {
    return {
      cursos: [],
      mostrarModal: false,
      cursoSeleccionado: null
    };
  },

  mounted() {
    this.cargarCursos();
  },

  methods: {
    async cargarCursos() {
      const res = await cursoService.obtener();
      this.cursos = res.data;
    },

    nuevoCurso() {
      this.cursoSeleccionado = null;
      this.mostrarModal = true;
    },

    editarCurso(curso) {
      this.cursoSeleccionado = { ...curso };
      this.mostrarModal = true;
    },

    async borrarCurso(id) {
      if (!confirm("¿Seguro que deseas eliminar este curso?")) return;
      await cursoService.eliminar(id);
      this.cargarCursos();
    },

    cerrarModal() {
      this.mostrarModal = false;
    }
  }
};
</script>

<style>
    .crud-container {
        padding: 20px;
    }

    .crud-table {
        width: 100%;
        border-collapse: collapse;
    }

        .crud-table th, .crud-table td {
            border: 1px solid #ccc;
            padding: 10px;
        }

    .btn-add {
        margin-bottom: 15px;
        padding: 10px;
    }

    .btn-delete {
        background: red;
        color: white;
    }
</style>
