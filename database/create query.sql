-- ==========================================
--  GYM ACCESS SYSTEM – SCHEMA  v1.1
--  Arabic-ready (utf8mb4_unicode_ci)
--  MySQL 8.0+ (InnoDB)
-- ==========================================

/*------------------------------------------------
  1.  DATABASE
-------------------------------------------------*/
CREATE DATABASE IF NOT EXISTS gym_access_system
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
USE gym_access_system;

/* Helper: every table inherits the DB charset/collation,
   but we repeat DEFAULT clauses for clarity.            */

/*------------------------------------------------
  2.  REFERENCE / LOOK-UP TABLES
-------------------------------------------------*/
CREATE TABLE IF NOT EXISTS plans (
    plan_id          INT AUTO_INCREMENT PRIMARY KEY,
    name             VARCHAR(80)  NOT NULL,
    description      VARCHAR(255),
    price_cents      INT UNSIGNED NOT NULL,
    duration_months  TINYINT UNSIGNED NOT NULL,
    grace_days       TINYINT UNSIGNED DEFAULT 0,
    is_active        BOOLEAN DEFAULT TRUE,
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY uk_plan_name (name)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

/*------------------------------------------------
  3.  CORE BUSINESS ENTITIES
-------------------------------------------------*/
CREATE TABLE IF NOT EXISTS members (
    member_id        BIGINT AUTO_INCREMENT PRIMARY KEY,
    first_name       VARCHAR(60)  NOT NULL,
    last_name        VARCHAR(60)  NOT NULL,
    email            VARCHAR(120) NOT NULL,
    phone            VARCHAR(35),
    date_of_birth    DATE,
    id_scan_path     VARCHAR(255),
    kyc_complete     BOOLEAN DEFAULT FALSE,
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY uk_member_email (email)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS fingerprints (
    fingerprint_id   BIGINT AUTO_INCREMENT PRIMARY KEY,
    member_id        BIGINT NOT NULL,
    finger_label     ENUM('LEFT_THUMB','LEFT_INDEX','LEFT_MIDDLE','LEFT_RING','LEFT_LITTLE',
                          'RIGHT_THUMB','RIGHT_INDEX','RIGHT_MIDDLE','RIGHT_RING','RIGHT_LITTLE')
                     DEFAULT 'RIGHT_INDEX',
    template_blob    LONGBLOB NOT NULL,
    enrolled_at      TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    quality_score    TINYINT UNSIGNED,
    CONSTRAINT fk_fp_member
        FOREIGN KEY (member_id) REFERENCES members(member_id)
        ON DELETE CASCADE
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS subscriptions (
    subscription_id  BIGINT AUTO_INCREMENT PRIMARY KEY,
    member_id        BIGINT NOT NULL,
    plan_id          INT    NOT NULL,
    start_date       DATE   NOT NULL,
    end_date         DATE   NOT NULL,
    status           ENUM('ACTIVE','EXPIRED','CANCELLED') DEFAULT 'ACTIVE',
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_sub_member
        FOREIGN KEY (member_id) REFERENCES members(member_id)
        ON DELETE CASCADE,
    CONSTRAINT fk_sub_plan
        FOREIGN KEY (plan_id)   REFERENCES plans(plan_id)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS payments (
    payment_id       BIGINT AUTO_INCREMENT PRIMARY KEY,
    subscription_id  BIGINT NOT NULL,
    amount_cents     INT UNSIGNED NOT NULL,
    paid_on          DATE NOT NULL,
    method           ENUM('CASH','CARD','BANK_TRANSFER','OTHER') DEFAULT 'CASH',
    txn_reference    VARCHAR(120),
    recorded_by      VARCHAR(60),
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_pay_sub
        FOREIGN KEY (subscription_id) REFERENCES subscriptions(subscription_id)
        ON DELETE CASCADE,
    INDEX idx_pay_paid_on (paid_on)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

/*------------------------------------------------
  4.  ACCESS CONTROL ENTITIES
-------------------------------------------------*/
CREATE TABLE IF NOT EXISTS controllers (
    controller_id    INT AUTO_INCREMENT PRIMARY KEY,
    name             VARCHAR(80) NOT NULL,
    ip_address       VARCHAR(45) NOT NULL,
    firmware_version VARCHAR(40),
    last_seen        TIMESTAMP NULL,
    UNIQUE KEY uk_controller_ip (ip_address)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS access_tokens (
    token_id         BIGINT AUTO_INCREMENT PRIMARY KEY,
    member_id        BIGINT NOT NULL,
    token_value      CHAR(64) NOT NULL,
    issued_at        TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    expires_at       TIMESTAMP NULL,
    revoked          BOOLEAN DEFAULT FALSE,
    CONSTRAINT fk_tok_member
        FOREIGN KEY (member_id) REFERENCES members(member_id)
        ON DELETE CASCADE,
    UNIQUE KEY uk_token_value (token_value),
    INDEX idx_tok_expires (expires_at)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS controller_token_status (
    controller_id    INT    NOT NULL,
    token_id         BIGINT NOT NULL,
    pushed_at        TIMESTAMP NULL,
    push_status      ENUM('PENDING','SUCCESS','FAIL') DEFAULT 'PENDING',
    PRIMARY KEY (controller_id, token_id),
    CONSTRAINT fk_cts_controller
        FOREIGN KEY (controller_id) REFERENCES controllers(controller_id)
        ON DELETE CASCADE,
    CONSTRAINT fk_cts_token
        FOREIGN KEY (token_id)     REFERENCES access_tokens(token_id)
        ON DELETE CASCADE
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS access_logs (
    log_id           BIGINT AUTO_INCREMENT PRIMARY KEY,
    controller_id    INT    NOT NULL,
    member_id        BIGINT,
    event_type       ENUM('GRANT','DENY') NOT NULL,
    event_time       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    reason           VARCHAR(120),
    CONSTRAINT fk_al_controller
        FOREIGN KEY (controller_id) REFERENCES controllers(controller_id),
    CONSTRAINT fk_al_member
        FOREIGN KEY (member_id)    REFERENCES members(member_id)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

/*------------------------------------------------
  5.  APPLICATION USERS / ROLES
-------------------------------------------------*/
CREATE TABLE IF NOT EXISTS app_users (
    user_id          INT AUTO_INCREMENT PRIMARY KEY,
    username         VARCHAR(60) NOT NULL,
    password_hash    CHAR(60)    NOT NULL,
    role             ENUM('DATA_ENTRY','ADMIN','SUPPORT') NOT NULL,
    email            VARCHAR(120),
    is_enabled       BOOLEAN DEFAULT TRUE,
    created_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY uk_app_username (username)
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;

/*------------------------------------------------
  6.  SYSTEM & ALERT LOGGING
-------------------------------------------------*/
CREATE TABLE IF NOT EXISTS email_alerts (
    alert_id         BIGINT AUTO_INCREMENT PRIMARY KEY,
    alert_type       ENUM('OVERDUE','TOKEN_PUSH_FAIL','KYC_ISSUE') NOT NULL,
    related_member   BIGINT,
    details          VARCHAR(255),
    sent_at          TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_emalert_member
        FOREIGN KEY (related_member) REFERENCES members(member_id)
        ON DELETE SET NULL
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci;
