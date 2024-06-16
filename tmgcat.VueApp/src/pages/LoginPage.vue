<template>
    <div class="sign-page">
        <h2 class="sign-header">Вход</h2>
        <input placeholder="Email" v-model="email" class="sign-input">
        <Password placeholder="Пароль" v-model="password" :feedback="false"  class="sign-password"/>
        <button class="sign-button" @click="login()">Войти</button>
    </div>
    </template>
    
    <script>
    import Password from 'primevue/password';
    export default {
        components: {
        Password
  },
        data() {
            return {
                password: '',
                email:''
            }
        },
        methods: {
            login() {
    
            }
        }
    }
    </script>
    
    <style>
    </style>