/* ==========================================================
   DEMO DATA – GYM ACCESS SYSTEM
   MySQL 8.x  |  utf8mb4  |  12 Jul 2025
   ----------------------------------------------------------
   • Shows Arabic text handling
   • Populates every table with a few rows
   • Uses fixed primary-key values so FK relationships line up
   ========================================================== */

START TRANSACTION;
SET NAMES utf8mb4;

/* ----------  Plans  ---------- */
INSERT INTO plans (plan_id, name, description, price_cents, duration_months, grace_days, is_active)
VALUES
 (1, 'شهري',      'اشتراك لمدة شهر',        3000,  1, 3, TRUE),
 (2, 'ربع سنوي',  'اشتراك لمدة 3 أشهر',     8000,  3, 5, TRUE),
 (3, 'سنوي',      'اشتراك لمدة 12 شهر',    30000, 12, 7, TRUE);

/* ----------  Members  ---------- */
INSERT INTO members (member_id, first_name, last_name, email, phone, date_of_birth,
                     id_scan_path, kyc_complete)
VALUES
 (1, 'أحمد',   'علي',     'ahmad.ali@example.com',  '+971501111111', '1990-02-14',
     'scans/ahmad_ali_id.jpg',   TRUE),
 (2, 'John',   'Smith',   'john.smith@example.com', '+359888222333', '1985-11-02',
     'scans/john_smith_id.jpg',  TRUE),
 (3, 'Мария',  'Петрова', 'maria.petrova@example.com', '+359884555666', '1993-07-30',
     'scans/maria_petrova_id.jpg', TRUE);

/* ----------  Fingerprints  ---------- */
INSERT INTO fingerprints (fingerprint_id, member_id, finger_label, template_blob, quality_score)
VALUES
 (1, 1, 'RIGHT_INDEX', UNHEX('0123456789ABCDEF'), 78),
 (2, 2, 'RIGHT_INDEX', UNHEX('89ABCDEF01234567'), 72),
 (3, 3, 'RIGHT_INDEX', UNHEX('FEDCBA9876543210'), 80);

/* ----------  Subscriptions  ---------- */
INSERT INTO subscriptions (subscription_id, member_id, plan_id, start_date, end_date, status)
VALUES
 (1, 1, 1, '2025-07-01', '2025-07-31', 'ACTIVE'),
 (2, 2, 1, '2025-05-01', '2025-05-31', 'EXPIRED'),
 (3, 3, 2, '2025-07-01', '2025-09-30', 'ACTIVE');

/* ----------  Payments  ---------- */
INSERT INTO payments (payment_id, subscription_id, amount_cents, paid_on, method,
                      txn_reference, recorded_by)
VALUES
 (1, 1, 3000, '2025-07-01', 'CARD',  'TXN-1001', 'data_entry'),
 (2, 2, 3000, '2025-05-01', 'CASH',  'TXN-1002', 'data_entry'),
 (3, 3, 8000, '2025-07-01', 'CARD',  'TXN-1003', 'data_entry');

/* ----------  Controllers  ---------- */
INSERT INTO controllers (controller_id, name, ip_address, firmware_version, last_seen)
VALUES
 (1, 'Front Door', '192.168.10.150', '1.2.3', NOW()),
 (2, 'Back Door',  '192.168.10.151', '1.2.3', NOW());

/* ----------  Access Tokens  ---------- */
INSERT INTO access_tokens (token_id, member_id, token_value, issued_at,
                           expires_at, revoked)
VALUES
 (1, 1, 'd75f4a23785c48ad95703d1f05db2d14c1f874eadbbff17b0fac63620d05b2e1',
     NOW(),              '2025-08-02 23:59:59', FALSE),
 (2, 2, 'e4a2cf55e76a4aa4b6dd8dcb011f8f223cee6c5499b4fda2d948b1b256902fd7',
     '2025-04-30 10:00', '2025-05-31 23:59:59', FALSE),
 (3, 3, 'a268a4a8e0d1fa2f55fef8b0c73ce7bae33a789fdfb2bf02b77eb8a0e378c575',
     NOW(),              '2025-10-02 23:59:59', FALSE);

/* ----------  Controller ↔ Token status  ---------- */
INSERT INTO controller_token_status
  (controller_id, token_id, pushed_at, push_status)
VALUES
 (1, 1, NOW(),                    'SUCCESS'),
 (2, 1, NOW(),                    'SUCCESS'),
 (1, 2, '2025-05-01 08:00:00',    'SUCCESS'),
 (2, 2, '2025-05-01 08:00:05',    'SUCCESS'),
 (1, 3, NOW(),                    'SUCCESS'),
 (2, 3, NOW(),                    'SUCCESS');

/* ----------  Access Logs  ---------- */
INSERT INTO access_logs (log_id, controller_id, member_id,
                         event_type, event_time, reason)
VALUES
 (1, 1, 1, 'GRANT', NOW(),                     NULL),
 (2, 1, 2, 'DENY',  '2025-06-01 07:10:00',     'EXPIRED'),
 (3, 2, 3, 'GRANT', NOW(),                     NULL);

/* ----------  Application Staff Users  ---------- */
INSERT INTO app_users (user_id, username, password_hash, role,
                       email, is_enabled)
VALUES
 (1, 'admin',      '$2a$12$uBdmK.Yli5iGyrFqoZrgYuC/2Rz/s5HH9lwwANup6vJABG1oSJdle',
     'ADMIN',      'admin@gym.local',      TRUE),
 (2, 'data_entry', '$2a$12$gT7xO7B8B7Jy8fD42Kue4eWnD0JS3QO9a.8Jy6eE1B3pdxLk2iU7u',
     'DATA_ENTRY', 'desk@gym.local',       TRUE),
 (3, 'support',    '$2a$12$9y0fKJzWTtcfYtVQvLuGZ.nODYNfD8a/8O.98oMqFo9s7Qs.zip.q',
     'SUPPORT',    'support@gym.local',    TRUE);

/* ----------  Email Alerts (sample)  ---------- */
INSERT INTO email_alerts (alert_id, alert_type, related_member, details)
VALUES
 (1, 'OVERDUE',         2, 'Member John Smith overdue by 3 days'),
 (2, 'TOKEN_PUSH_FAIL', 1, 'Front Door push pending after 3 retries');

COMMIT;
