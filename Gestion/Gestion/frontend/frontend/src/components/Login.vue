<template>
    <div class="login-wrapper">
        <div class="login-container">
            <div class="login-header">
                <div class="logo-circle">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path>
                        <circle cx="12" cy="7" r="4"></circle>
                    </svg>
                </div>
                <h2>Iniciar Sesion</h2>
                <p class="subtitle">Ingresa tus credenciales para continuar</p>
            </div>

            <form @submit.prevent="loginUser" class="login-form">
                <div class="input-group">
                    <label for="email">Correo Electronico</label>
                    <div class="input-wrapper">
                        <svg class="input-icon" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"></path>
                            <polyline points="22,6 12,13 2,6"></polyline>
                        </svg>
                        <input id="email"
                               type="email"
                               v-model="email"
                               placeholder="correo@ejemplo.com"
                               required
                               :disabled="loading" />
                    </div>
                </div>

                <div class="input-group">
                    <label for="password">Contrasena</label>
                    <div class="input-wrapper">
                        <svg class="input-icon" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <rect x="3" y="11" width="18" height="11" rx="2" ry="2"></rect>
                            <path d="M7 11V7a5 5 0 0 1 10 0v4"></path>
                        </svg>
                        <input id="password"
                               type="password"
                               v-model="password"
                               placeholder="Ingrese tu contrasena aqui"
                               required
                               :disabled="loading" />
                    </div>
                </div>

                <div class="form-options">
                    <label class="checkbox-label">
                        <input type="checkbox" v-model="rememberMe">
                        <span>Recordarme</span>
                    </label>
                    <a href="#" class="forgot-password">Restablece tu contrasena</a>
                </div>

                <button type="submit" class="btn-login" :disabled="loading">
                    <span v-if="!loading">Ingresar</span>
                    <span v-else class="loading-spinner">
                        <svg class="spinner" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <line x1="12" y1="2" x2="12" y2="6"></line>
                            <line x1="12" y1="18" x2="12" y2="22"></line>
                            <line x1="4.93" y1="4.93" x2="7.76" y2="7.76"></line>
                            <line x1="16.24" y1="16.24" x2="19.07" y2="19.07"></line>
                            <line x1="2" y1="12" x2="6" y2="12"></line>
                            <line x1="18" y1="12" x2="22" y2="12"></line>
                            <line x1="4.93" y1="19.07" x2="7.76" y2="16.24"></line>
                            <line x1="16.24" y1="7.76" x2="19.07" y2="4.93"></line>
                        </svg>
                        Iniciando sesión...
                    </span>
                </button>

                <transition name="fade">
                    <div v-if="error" class="alert alert-error">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <circle cx="12" cy="12" r="10"></circle>
                            <line x1="15" y1="9" x2="9" y2="15"></line>
                            <line x1="9" y1="9" x2="15" y2="15"></line>
                        </svg>
                        <span>{{ error }}</span>
                    </div>
                </transition>

                <transition name="fade">
                    <div v-if="success" class="alert alert-success">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"></path>
                            <polyline points="22 4 12 14.01 9 11.01"></polyline>
                        </svg>
                        <span>{{ success }}</span>
                    </div>
                </transition>
            </form>

            <div class="login-footer">
                <p>Crear cuenta <a href="#" class="register-link">Registrate Aqui</a></p>
            </div>
        </div>
    </div>
</template>

<script>
    import userService from "../services/userService.js";

    export default {
        data() {
            return {
                email: "",
                password: "",
                rememberMe: false,
                error: null,
                success: null,
                loading: false
            };
        },
        methods: {
            async loginUser() {
                try {
                    this.error = null;

                    const response = await userService.login(this.email, this.password);

                    
                    localStorage.setItem("user", JSON.stringify(response.data));

                    this.$router.push("/dashboard");

                } catch (err) {
                    this.error = err.response?.data?.error || "Error al iniciar sesión";
                }
            }

        }
    };
</script>

<style scoped>
    * {
        box-sizing: border-box;
        margin: 0;
        padding: 0;
    }

    .login-wrapper {
        min-height: 100vh;
        display: flex;
        align-items: stretch;
        justify-content: center;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        padding: 0;
        font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
    }

    .login-container {
        width: 100%;
        background: white;
        border-radius: 0;
        box-shadow: none;
        overflow: hidden;
        animation: slideUp 0.5s ease-out;
        min-height: 100vh;
        display: flex;
        flex-direction: column;
    }

    @keyframes slideUp {
        from {
            opacity: 0;
            transform: translateY(30px);
        }

        to {
            opacity: 1;
            transform: translateY(0);
        }
    }

    .login-header {
        padding: 40px 40px 30px;
        text-align: center;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        color: white;
    }

    .logo-circle {
        width: 80px;
        height: 80px;
        background: rgba(255, 255, 255, 0.2);
        backdrop-filter: blur(10px);
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        margin: 0 auto 20px;
        border: 3px solid rgba(255, 255, 255, 0.3);
    }

        .logo-circle svg {
            color: white;
        }

    .login-header h2 {
        font-size: 28px;
        font-weight: 700;
        margin: 0 0 8px;
    }

    .subtitle {
        font-size: 14px;
        opacity: 0.9;
        font-weight: 400;
    }

    .login-form {
        padding: 40px;
        flex: 1;
        display: flex;
        flex-direction: column;
        justify-content: center;
        max-width: 500px;
        margin: 0 auto;
        width: 100%;
    }

    .input-group {
        margin-bottom: 24px;
    }

        .input-group label {
            display: block;
            font-size: 14px;
            font-weight: 600;
            color: #333;
            margin-bottom: 8px;
        }

    .input-wrapper {
        position: relative;
        display: flex;
        align-items: center;
    }

    .input-icon {
        position: absolute;
        left: 14px;
        color: #9ca3af;
        pointer-events: none;
    }

    .input-wrapper input {
        width: 100%;
        padding: 12px 14px 12px 44px;
        border: 2px solid #e5e7eb;
        border-radius: 8px;
        font-size: 15px;
        transition: all 0.3s ease;
        background: #f9fafb;
    }

        .input-wrapper input:focus {
            outline: none;
            border-color: #667eea;
            background: white;
            box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
        }

        .input-wrapper input:disabled {
            background: #f3f4f6;
            cursor: not-allowed;
        }

    .form-options {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
        font-size: 14px;
    }

    .checkbox-label {
        display: flex;
        align-items: center;
        cursor: pointer;
        color: #4b5563;
    }

        .checkbox-label input[type="checkbox"] {
            margin-right: 8px;
            cursor: pointer;
        }

    .forgot-password {
        color: #667eea;
        text-decoration: none;
        font-weight: 500;
        transition: color 0.3s ease;
    }

        .forgot-password:hover {
            color: #764ba2;
            text-decoration: underline;
        }

    .btn-login {
        width: 100%;
        padding: 14px;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        border: none;
        border-radius: 8px;
        color: white;
        font-size: 16px;
        font-weight: 600;
        cursor: pointer;
        transition: all 0.3s ease;
        box-shadow: 0 4px 12px rgba(102, 126, 234, 0.4);
        margin-bottom: 16px;
    }

        .btn-login:hover:not(:disabled) {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(102, 126, 234, 0.5);
        }

        .btn-login:active:not(:disabled) {
            transform: translateY(0);
        }

        .btn-login:disabled {
            opacity: 0.7;
            cursor: not-allowed;
        }

    .loading-spinner {
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 8px;
    }

    .spinner {
        animation: spin 1s linear infinite;
    }

    @keyframes spin {
        from {
            transform: rotate(0deg);
        }

        to {
            transform: rotate(360deg);
        }
    }

    .alert {
        padding: 12px 16px;
        border-radius: 8px;
        display: flex;
        align-items: center;
        gap: 10px;
        font-size: 14px;
        margin-top: 16px;
    }

    .alert-error {
        background: #fee2e2;
        color: #991b1b;
        border: 1px solid #fecaca;
    }

    .alert-success {
        background: #d1fae5;
        color: #065f46;
        border: 1px solid #a7f3d0;
    }

    .fade-enter-active, .fade-leave-active {
        transition: all 0.3s ease;
    }

    .fade-enter-from, .fade-leave-to {
        opacity: 0;
        transform: translateY(-10px);
    }

    .login-footer {
        padding: 24px 40px 32px;
        text-align: center;
        background: #f9fafb;
        border-top: 1px solid #e5e7eb;
    }

        .login-footer p {
            color: #6b7280;
            font-size: 14px;
        }

    .register-link {
        color: #667eea;
        text-decoration: none;
        font-weight: 600;
        transition: color 0.3s ease;
    }

        .register-link:hover {
            color: #764ba2;
            text-decoration: underline;
        }

    @media (max-width: 480px) {
        .login-header,
        .login-form,
        .login-footer {
            padding-left: 24px;
            padding-right: 24px;
        }

        .form-options {
            flex-direction: column;
            align-items: flex-start;
            gap: 12px;
        }
    }
</style>