<template>
  <main>
    <h2>Вход</h2>
    <form class="form" @submit.prevent>
      <label for="login">Логин</label>
      <input id="login" name="login" type="text" autofocus />

      <label for="password">Пароль</label>
      <input id="password" name="password" type="password" />

      <button type="submit">Войти</button>
    </form>
  </main>
</template>

<style scoped>
.form { display: flex; flex-direction: column; gap: 6px; max-width: 480px; }
.form label { margin-top: 8px; font-weight: 600; }
.form input { padding: 8px; border: 1px solid #aaa; border-radius: 6px; }
.form button { margin-top: 12px; padding: 10px; background: #2a6df4; color: white; border: none; border-radius: 6px; cursor: pointer; }
</style>
