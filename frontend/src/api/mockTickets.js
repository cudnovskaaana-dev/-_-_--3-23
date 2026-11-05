import { ref } from 'vue'

const records = ref([
  { id: 1, number: 'КБ-101', title: 'Обрыв кабеля на участке Астана-Центр', siteId: 3, description: 'Потеряна связь после земляных работ.', status: 'New' },
  { id: 2, number: 'КБ-102', title: 'Повреждение магистрали после грозы', siteId: 1, description: 'На участке нестабильный сигнал.', status: 'InProgress' },
])
let nextId = 3

const pause = (ms = 500) => new Promise((resolve) => setTimeout(resolve, ms))

export async function mockFetch(url, options = {}) {
  await pause()
  if (import.meta.env.VITE_MOCK_FORCE_500 === 'true') {
    return Response.json({ error: 'Внутренняя ошибка сервера' }, { status: 500 })
  }
  if (import.meta.env.VITE_MOCK_FORCE_400 === 'true') {
    return Response.json({ error: 'Проверьте поля заявки' }, { status: 400 })
  }
  const path = new URL(url, 'http://mock.local').pathname
  if (path.endsWith('/__mock/500')) return Response.json({ error: 'Внутренняя ошибка сервера' }, { status: 500 })
  if (path.endsWith('/cable-tickets') && options.method === 'POST') {
    const body = JSON.parse(options.body || '{}')
    if (!body.title || body.title.trim().length < 5 || body.title.trim().length > 80) {
      return Response.json({ error: 'Проверьте поля заявки' }, { status: 400 })
    }
    if (!Number.isInteger(body.siteId) || body.siteId < 1 || !body.description || body.description.trim().length < 10 || body.description.trim().length > 500) {
      return Response.json({ error: 'Проверьте поля заявки' }, { status: 400 })
    }
    const ticket = { id: nextId, number: `КБ-${100 + nextId}`, title: body.title.trim(), siteId: body.siteId, description: body.description.trim(), status: 'New' }
    nextId += 1
    records.value.push(ticket)
    return Response.json(ticket, { status: 201 })
  }
  if (path.endsWith('/cable-tickets')) return Response.json(records.value)
  return Response.json({ error: 'Не найдено' }, { status: 404 })
}
