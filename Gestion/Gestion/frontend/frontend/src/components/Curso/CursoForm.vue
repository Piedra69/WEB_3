<template>
    <div class="modal-overlay">
        <div class="modal">
            <h2>{{ curso ? "Editar Curso" : "Nuevo Curso" }}</h2>

            <form @submit.prevent="guardar">
                <label>Título</label>
                <input v-model="form.titulo" required />

                <label>Descripción</label>
                <textarea v-model="form.descripcion"></textarea>

                <div class="actions">
                    <button type="submit">Guardar</button>
                    <button @click="$emit('cerrar')" type="button">Cancelar</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
    import cursoService from "../../services/cursoService";

    export default {
        props: ["curso"],

        data() {
            return {
                form: {
                    id: null,
                    titulo: "",
                    descripcion: "",
                    userId: null
                }
            };
        },

        mounted() {
            const user = JSON.parse(localStorage.getItem("user"));

            if (!user) {
                alert("Sesión no encontrada");
                this.$router.push("/");
                return;
            }

            this.form.userId = user.id ?? user.user?.id;

            if (this.curso) {
                this.form = { ...this.curso };
                this.form.userId = user.id ?? user.user?.id;
            }
        },

        methods: {
            async guardar() {
                const data = {
                    id: this.form.id,
                    titulo: this.form.titulo,
                    descripcion: this.form.descripcion,
                    userId: this.form.userId
                };

                try {
                    if (data.id) {
                        await cursoService.actualizar(data);
                    } else {
                        await cursoService.registrar(data);
                    }

                    this.$emit("cerrar");
                    this.$emit("guardado");
                }
                catch (err) {
                    console.log("⚠️ ERROR SERVIDOR:", err.response?.data);
                    console.log("⚠️ MENSAJE:", err.response?.data?.error);
                    console.log("⚠️ DATA ENVIADA:", data);
                }
            }

        }
    };
</script>
