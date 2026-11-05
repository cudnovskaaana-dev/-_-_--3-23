INSERT INTO network_sites (id, name) VALUES (1, 'Астана-Центр'), (2, 'Алматы-Магистраль') ON CONFLICT (id) DO NOTHING;
INSERT INTO app_users (id, login, role) VALUES (1, 'dispatcher.demo', 'Dispatcher'), (2, 'electrician.demo', 'Electrician') ON CONFLICT (id) DO NOTHING;
INSERT INTO cable_tickets (id, number, title, description, status, site_id, created_by_user_id, assignee_user_id) VALUES
(1, 'КБ-101', 'Обрыв кабеля на участке Астана', 'Потеряна связь после земляных работ.', 'New', 1, 1, NULL),
(2, 'КБ-102', 'Повреждение магистрали Алматы', 'Нестабильный сигнал после грозы.', 'InProgress', 2, 1, 2),
(3, 'КБ-103', 'Износ линии на участке Астана', 'Плановая замена повреждённого отрезка.', 'Closed', 1, 1, 2),
(4, 'КБ-104', 'Отменён выезд на участок Алматы', 'Дублирующая заявка после уточнения адреса.', 'Cancelled', 2, 1, NULL)
ON CONFLICT (id) DO NOTHING;
SELECT setval(pg_get_serial_sequence('network_sites', 'id'), GREATEST((SELECT max(id) FROM network_sites), 1));
SELECT setval(pg_get_serial_sequence('app_users', 'id'), GREATEST((SELECT max(id) FROM app_users), 1));
SELECT setval(pg_get_serial_sequence('cable_tickets', 'id'), GREATEST((SELECT max(id) FROM cable_tickets), 1));
