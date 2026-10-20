<script setup>
import { ref } from 'vue'
import { addItem } from '../ticketsStore.js'
import { useRouter } from 'vue-router'

const router = useRouter()

// Объект формы
const form = ref({
  title: '',
  siteId: 0,
  description: ''
})

// Переменные для ошибок
const errorTitle = ref('')
const errorSiteId = ref('')
const errorDesc = ref('')

function onCreate() {
  // Сбрасываем старые ошибки
  errorTitle.value = ''
  errorSiteId.value = ''
  errorDesc.value = ''

  let isValid = true

  // 1. Проверка темы (от 5 до 80 символов)
  const cleanTitle = form.value.title.trim()
  if (cleanTitle.length < 5 || cleanTitle.length > 80) {
    errorTitle.value = 'Тема: от 5 до 80 символов'
    isValid = false
  }

  // 2. Проверка участка (целое число > 0)
  if (!Number.isInteger(form.value.siteId) || form.value.siteId <= 0) {
    errorSiteId.value = 'Укажите номер участка (больше 0)'
    isValid = false
  }

  // 3. Проверка описания (от 10 до 500 символов)
  const cleanDesc = form.value.description.trim()
  if (cleanDesc.length < 10 || cleanDesc.length > 500) {
    errorDesc.value = 'Описание: от 10 до 500 символов'
    isValid = false
  }

  // Если есть ошибки — останавливаемся (return)
  if (!isValid) return

  // Если всё ок — добавляем заявку в массив!
  addItem(cleanTitle, form.value.siteId, cleanDesc)

  // Очищаем форму
  form.value = { title: '', siteId: 0, description: '' }

  // Переходим на страницу списка
  router.push('/')
}
</script>

<template>
  <main>
    <h2>Новая заявка</h2>
    
    <!-- @submit.prevent отменяет перезагрузку страницы при Enter -->
    <form class="form" @submit.prevent="onCreate">
      
      <label for="title">Тема, 5–80 символов</label>
      <input id="title" v-model="form.title" name="title" type="text" autofocus />
      <span v-if="errorTitle" class="error">{{ errorTitle }}</span>

      <label for="siteId">Номер участка сети</label>
      <!-- v-model.number превращает текст в число! -->
      <input id="siteId" v-model.number="form.siteId" name="siteId" type="number" />
      <span v-if="errorSiteId" class="error">{{ errorSiteId }}</span>

      <label for="description">Описание, 10–500 символов</label>
      <textarea id="description" v-model="form.description" name="description"></textarea>
      <span v-if="errorDesc" class="error">{{ errorDesc }}</span>

      <button type="submit">Создать</button>
    </form>
  </main>
</template>

<style scoped>
.form { display: flex; flex-direction: column; gap: 6px; max-width: 480px; }
.form label { margin-top: 8px; font-weight: 600; }
.form input, .form textarea { padding: 8px; border: 1px solid #aaa; border-radius: 6px; }
.form textarea { min-height: 100px; resize: vertical; }
.form button { margin-top: 12px; padding: 10px; background: #2a6df4; color: white; border: none; border-radius: 6px; cursor: pointer; align-self: flex-start; }
.form button:hover { background: #1f57c7; }

/* Стили для красных сообщений об ошибках */
.error {
  color: #d32f2f;
  font-size: 14px;
  margin-top: 2px;
}
</style>
