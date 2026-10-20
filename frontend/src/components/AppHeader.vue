<template>
  <header class="topbar">
    <h1>Учёт повреждений кабеля</h1>
    <nav class="nav">
      <router-link to="/">Список</router-link>
      <router-link to="/new">Создать</router-link>
      <router-link to="/login">Вход</router-link>
    </nav>
  </header>
</template>

<style scoped>
.topbar { display: flex; justify-content: space-between; align-items: center; border-bottom: 1px solid #ddd; padding-bottom: 12px; margin-bottom: 24px; }
h1 { font-size: 20px; margin: 0; }
.nav { display: flex; gap: 15px; }
.nav a { text-decoration: none; color: #2a6df4; font-weight: bold; }
.nav a:hover { text-decoration: underline; }
</style>
