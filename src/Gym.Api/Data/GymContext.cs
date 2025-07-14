
// =============================
// File: Data/GymContext.cs
// =============================
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Data;

public class GymContext(DbContextOptions<GymContext> opts) : DbContext(opts)
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<Fingerprint> Fingerprints => Set<Fingerprint>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Controller> Controllers => Set<Controller>();
    public DbSet<AccessToken> AccessTokens => Set<AccessToken>();
    public DbSet<ControllerTokenStatus> ControllerTokenStatuses => Set<ControllerTokenStatus>();
    public DbSet<AccessLog> AccessLogs => Set<AccessLog>();
    public DbSet<EmailAlert> EmailAlerts => Set<EmailAlert>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Member>(e =>
        {
            e.ToTable("members");
            e.HasKey(m => m.MemberId);
            e.Property(m => m.MemberId).HasColumnName("member_id");
            e.Property(m => m.FirstName).HasColumnName("first_name");
            e.Property(m => m.LastName).HasColumnName("last_name");
            e.Property(m => m.Email).HasColumnName("email");
            e.Property(m => m.Phone).HasColumnName("phone");
            e.Property(m => m.DateOfBirth).HasColumnName("date_of_birth");
            e.Property(m => m.IdScanPath).HasColumnName("id_scan_path");
            e.Property(m => m.KycComplete).HasColumnName("kyc_complete");
            e.Property(m => m.CreatedAt).HasColumnName("created_at");
            e.Property(m => m.UpdatedAt).HasColumnName("updated_at");
        });
        b.Entity<Plan>(e =>
        {
            e.ToTable("plans");
            e.HasKey(p => p.PlanId);
            e.Property(p => p.PlanId).HasColumnName("plan_id");
            e.Property(p => p.Name).HasColumnName("name");
            e.Property(p => p.Description).HasColumnName("description");
            e.Property(p => p.PriceCents).HasColumnName("price_cents");
            e.Property(p => p.DurationMonths).HasColumnName("duration_months");
            e.Property(p => p.GraceDays).HasColumnName("grace_days");
            e.Property(p => p.IsActive).HasColumnName("is_active");
            e.Property(p => p.CreatedAt).HasColumnName("created_at");
            e.Property(p => p.UpdatedAt).HasColumnName("updated_at");
        });
        b.Entity<AppUser>(e =>
        {
            e.ToTable("app_users");
            e.HasKey(u => u.UserId);
            e.Property(u => u.UserId).HasColumnName("user_id");
            e.Property(u => u.Username).HasColumnName("username");
            e.Property(u => u.PasswordHash).HasColumnName("password_hash");
            e.Property(u => u.Role).HasColumnName("role").HasConversion<string>();
            e.Property(u => u.Email).HasColumnName("email");
            e.Property(u => u.IsEnabled).HasColumnName("is_enabled");
            e.Property(u => u.CreatedAt).HasColumnName("created_at");
        });
        b.Entity<Fingerprint>(e =>
        {
            e.ToTable("fingerprints");
            e.HasKey(f => f.FingerprintId);
            e.Property(f => f.FingerprintId).HasColumnName("fingerprint_id");
            e.Property(f => f.MemberId).HasColumnName("member_id");
            e.Property(f => f.FingerLabel).HasColumnName("finger_label").HasConversion<string>();
            e.Property(f => f.TemplateBlob).HasColumnName("template_blob");
            e.Property(f => f.EnrolledAt).HasColumnName("enrolled_at");
            e.Property(f => f.QualityScore).HasColumnName("quality_score");
        });
        b.Entity<Subscription>(e =>
        {
            e.ToTable("subscriptions");
            e.HasKey(s => s.SubscriptionId);
            e.Property(s => s.SubscriptionId).HasColumnName("subscription_id");
            e.Property(s => s.MemberId).HasColumnName("member_id");
            e.Property(s => s.PlanId).HasColumnName("plan_id");
            e.Property(s => s.StartDate).HasColumnName("start_date");
            e.Property(s => s.EndDate).HasColumnName("end_date");
            e.Property(s => s.Status).HasColumnName("status").HasConversion<string>();
            e.Property(s => s.CreatedAt).HasColumnName("created_at");
            e.Property(s => s.UpdatedAt).HasColumnName("updated_at");
        });
        b.Entity<Payment>(e =>
        {
            e.ToTable("payments");
            e.HasKey(p => p.PaymentId);
            e.Property(p => p.PaymentId).HasColumnName("payment_id");
            e.Property(p => p.SubscriptionId).HasColumnName("subscription_id");
            e.Property(p => p.AmountCents).HasColumnName("amount_cents");
            e.Property(p => p.PaidOn).HasColumnName("paid_on");
            e.Property(p => p.Method).HasColumnName("method").HasConversion<string>();
            e.Property(p => p.TxnReference).HasColumnName("txn_reference");
            e.Property(p => p.RecordedBy).HasColumnName("recorded_by");
            e.Property(p => p.CreatedAt).HasColumnName("created_at");
        });
        b.Entity<Controller>(e =>
        {
            e.ToTable("controllers");
            e.HasKey(c => c.ControllerId);
            e.Property(c => c.ControllerId).HasColumnName("controller_id");
            e.Property(c => c.Name).HasColumnName("name");
            e.Property(c => c.IpAddress).HasColumnName("ip_address");
            e.Property(c => c.FirmwareVersion).HasColumnName("firmware_version");
            e.Property(c => c.LastSeen).HasColumnName("last_seen");
        });
        b.Entity<AccessToken>(e =>
        {
            e.ToTable("access_tokens");
            e.HasKey(t => t.TokenId);
            e.Property(t => t.TokenId).HasColumnName("token_id");
            e.Property(t => t.MemberId).HasColumnName("member_id");
            e.Property(t => t.TokenValue).HasColumnName("token_value");
            e.Property(t => t.IssuedAt).HasColumnName("issued_at");
            e.Property(t => t.ExpiresAt).HasColumnName("expires_at");
            e.Property(t => t.Revoked).HasColumnName("revoked");
        });
        b.Entity<ControllerTokenStatus>(e =>
        {
            e.ToTable("controller_token_status");
            e.HasKey(ct => new { ct.ControllerId, ct.TokenId });
            e.Property(ct => ct.ControllerId).HasColumnName("controller_id");
            e.Property(ct => ct.TokenId).HasColumnName("token_id");
            e.Property(ct => ct.PushedAt).HasColumnName("pushed_at");
            e.Property(ct => ct.PushStatus).HasColumnName("push_status").HasConversion<string>();
        });
        b.Entity<AccessLog>(e =>
        {
            e.ToTable("access_logs");
            e.HasKey(l => l.LogId);
            e.Property(l => l.LogId).HasColumnName("log_id");
            e.Property(l => l.ControllerId).HasColumnName("controller_id");
            e.Property(l => l.MemberId).HasColumnName("member_id");
            e.Property(l => l.EventType).HasColumnName("event_type").HasConversion<string>();
            e.Property(l => l.EventTime).HasColumnName("event_time");
            e.Property(l => l.Reason).HasColumnName("reason");
        });
        b.Entity<EmailAlert>(e =>
        {
            e.ToTable("email_alerts");
            e.HasKey(a => a.AlertId);
            e.Property(a => a.AlertId).HasColumnName("alert_id");
            e.Property(a => a.AlertType).HasColumnName("alert_type").HasConversion<string>();
            e.Property(a => a.RelatedMember).HasColumnName("related_member");
            e.Property(a => a.Details).HasColumnName("details");
            e.Property(a => a.SentAt).HasColumnName("sent_at");
        });
    }
}
