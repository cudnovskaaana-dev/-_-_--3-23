# Контракт API

Базовый адрес в разработке: http://localhost:5000

## Таблица запросов

| Метод и путь | Тело | Успех | Ошибка |
|---|---|---|---|
| GET /api/cable-tickets | — | 200, массив (пустой — 200 и []) | — |
| GET /api/cable-tickets/{id} | — | 200, объект | 404 |
| POST /api/cable-tickets | title, siteId, description | 201, id, number, status: New | 400 |
| PATCH /api/cable-tickets/{id}/assignee | assigneeUserId | 200 | 404 |
| PATCH /api/cable-tickets/{id}/status | status | 200 | 404, 409 |

- DELETE не используется. Отмена — это статус Cancelled.
- Пути /api/cable-tickets/create нет: действие несёт метод POST.
- Назначение и смена статуса — два разных PATCH.

## Примеры JSON

### Создание заявки

POST /api/cable-tickets

```json
{
  "title": "Обрыв кабеля на участке Астана-Центр",
  "siteId": 3,
  "description": "После грозы нет сигнала, повреждение на магистрали"
}
```

Ответ 201:

```json
{
  "id": 17,
  "number": "КБ-104",
  "title": "Обрыв кабеля на участке Астана-Центр",
  "description": "После грозы нет сигнала, повреждение на магистрали",
  "status": "New",
  "siteId": 3,
  "createdByUserId": 5,
  "assigneeUserId": null
}
```

Клиент НЕ присылает id, number, status и исполнителя —
их определяет сервер.

### Назначение электромонтёра

PATCH /api/cable-tickets/17/assignee

```json
{ "assigneeUserId": 8 }
```

Ответ 200:

```json
{ "id": 17, "assigneeUserId": 8, "status": "New" }
```

### Перевод в работу / закрытие / отмена

PATCH /api/cable-tickets/17/status

```json
{ "status": "InProgress" }
```

Ответ 200:

```json
{ "id": 17, "status": "InProgress" }
```

Допустимые значения status: New, InProgress, Closed, Cancelled.

### Ошибки

400 — прислали ерунду:

```json
{ "error": "title must be 5–80 characters" }
```

404 — заявки с таким id нет:

```json
{ "error": "cable ticket 999 not found" }
```

409 — нельзя так по правилам:

```json
{ "error": "cannot close ticket in status New" }
```
