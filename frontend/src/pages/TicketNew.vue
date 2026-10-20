<script setup>
import { ref } from 'vue'
const form = ref({ title: '', siteId: null, description: '' })
</script>

<template>
  <main>
    <h2>Новая заявка</h2>
    <form class="form" @submit.prevent>
      <label for="title">Тема, 5–80 символов</label>
      <input id="title" v-model="form.title" name="title" type="text" autofocus />

      <label for="siteId">Номер участка сети</label>
      <input id="siteId" v-model.number="form.siteId" name="siteId" type="number" />

      <label for="description">Описание, 10–500 символов</label>
      <textarea id="description" v-model="form.description" name="description"></textarea>

      <button type="submit">Создать</button>
    </form>
  </main>
</template>

<style scoped>
.form { display: flex; flex-direction: column; gap: 6px; max-width: 480px; }
.form label { margin-top: 8px; font-weight: 600; }
.form input, .form textarea { padding: 8px; border: 1px solid #aaa; border-radius: 6px; }
.form button { margin-top: 12px; padding: 10px; background: #2a6df4; color: white; border: none; border-radius: 6px; cursor: pointer; }
</style>
