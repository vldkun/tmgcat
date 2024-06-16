<template>
<div class="sign-page">
    <h2 class="sign-header">Регистрация</h2>
    <input placeholder="Имя пользователя" v-model="userName" class="sign-input">
    <input placeholder="Email" v-model="email" class="sign-input">
    <Password placeholder="Пароль" v-model="password" :feedback="false"  class="sign-password"/>
    <button class="sign-button" @click="register()">Зарегистрироваться</button>
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
            userName: '',
            email:''
        }
    },
    
    methods: {
        register() {

        }
    }
}
</script>

<style>
</style>