<script setup>
import AppHeader from './components/AppHeader.vue'
</script>

<template>
  <div class="app">
    <AppHeader />
    <router-view />
  </div>
</template>

<style scoped>
.app { font-family: system-ui, Arial, sans-serif; max-width: 900px; margin: 0 auto; padding: 24px; color: #222; }
</style>
