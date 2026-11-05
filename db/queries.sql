-- Фильтр и сортировка: новые заявки выбранного участка сверху.
SELECT id, number, title, status, created_at
FROM cable_tickets WHERE site_id = 1 ORDER BY created_at DESC;

-- JOIN заявок с участком и назначенным электромонтёром.
SELECT t.number, s.name AS site_name, u.login AS electrician, t.status
FROM cable_tickets t
JOIN network_sites s ON s.id = t.site_id
LEFT JOIN app_users u ON u.id = t.assignee_user_id
ORDER BY t.id;

-- GROUP BY: количество заявок в каждом статусе.
SELECT status, count(*) AS ticket_count FROM cable_tickets GROUP BY status ORDER BY status;

-- CHECK отвергает неизвестное состояние (ожидается ошибка ограничения).
INSERT INTO cable_tickets (number, title, description, status, site_id, created_by_user_id)
VALUES ('КБ-999', 'Проверка ограничения', 'Тест допустимых статусов.', 'Unknown', 1, 1);
