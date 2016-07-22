namespace TPDigital.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OracleDbContext : DbContext
    {
        public OracleDbContext()
            : base("name=OracleDbContext")
        {
            Database.Initialize(false);
        }

        public virtual DbSet<TP_ACTIVITY> TP_ACTIVITY { get; set; }
        public virtual DbSet<TP_ACTIVITY_IMAGE> TP_ACTIVITY_IMAGE { get; set; }
        public virtual DbSet<TP_ACTIVITY_PRODUCT> TP_ACTIVITY_PRODUCT { get; set; }
        public virtual DbSet<TP_CATEGORY> TP_CATEGORY { get; set; }
        public virtual DbSet<TP_COMMENT> TP_COMMENT { get; set; }
        public virtual DbSet<TP_EXCHANGE> TP_EXCHANGE { get; set; }
        public virtual DbSet<TP_EXPRESS> TP_EXPRESS { get; set; }
        public virtual DbSet<TP_FAVORITE> TP_FAVORITE { get; set; }
        public virtual DbSet<TP_IMAGE> TP_IMAGE { get; set; }
        public virtual DbSet<TP_LOCATION> TP_LOCATION { get; set; }
        public virtual DbSet<TP_ORDER> TP_ORDER { get; set; }
        public virtual DbSet<TP_ORDER_PRODUCT> TP_ORDER_PRODUCT { get; set; }
        public virtual DbSet<TP_PACKAGE_STATE> TP_PACKAGE_STATE { get; set; }
        public virtual DbSet<TP_PAYMENT_STATE> TP_PAYMENT_STATE { get; set; }
        public virtual DbSet<TP_PRODUCT> TP_PRODUCT { get; set; }
        public virtual DbSet<TP_PRODUCT_IMAGE> TP_PRODUCT_IMAGE { get; set; }
        public virtual DbSet<TP_RECEIVER> TP_RECEIVER { get; set; }
        public virtual DbSet<TP_RETURN> TP_RETURN { get; set; }
        public virtual DbSet<TP_ROLE> TP_ROLE { get; set; }
        public virtual DbSet<TP_SHOPPING_CART> TP_SHOPPING_CART { get; set; }
        public virtual DbSet<TP_USER> TP_USER { get; set; }
        public virtual DbSet<TP_USER_ROLE> TP_USER_ROLE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TP_ACTIVITY>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ACTIVITY>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_ACTIVITY>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TP_ACTIVITY>()
                .Property(e => e.DISCOUNT)
                .HasPrecision(3, 3);

            modelBuilder.Entity<TP_ACTIVITY>()
                .HasMany(e => e.TP_ACTIVITY_PRODUCT)
                .WithRequired(e => e.TP_ACTIVITY)
                .HasForeignKey(e => e.ACTIVITY_ID);

            modelBuilder.Entity<TP_ACTIVITY>()
                .HasMany(e => e.TP_ACTIVITY_IMAGE)
                .WithOptional(e => e.TP_ACTIVITY)
                .HasForeignKey(e => e.ACTIVITY_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_ACTIVITY_IMAGE>()
                .Property(e => e.ACTIVITY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ACTIVITY_IMAGE>()
                .Property(e => e.IMAGE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ACTIVITY_IMAGE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ACTIVITY_PRODUCT>()
                .Property(e => e.ACTIVITY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ACTIVITY_PRODUCT>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ACTIVITY_PRODUCT>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_CATEGORY>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_CATEGORY>()
                .Property(e => e.CATEGORY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_CATEGORY>()
                .HasMany(e => e.TP_PRODUCT)
                .WithOptional(e => e.TP_CATEGORY)
                .HasForeignKey(e => e.CATEGORY_ID);

            modelBuilder.Entity<TP_COMMENT>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_COMMENT>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_COMMENT>()
                .Property(e => e.ORDER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_COMMENT>()
                .Property(e => e.CONTENT)
                .IsUnicode(false);

            modelBuilder.Entity<TP_COMMENT>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_COMMENT>()
                .Property(e => e.STARS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.ORDER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.QUANTITY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.PACKAGE_STATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.EXPRESS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.RETURN_EXPRESS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXCHANGE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXPRESS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_EXPRESS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_EXPRESS>()
                .Property(e => e.CHECK_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<TP_EXPRESS>()
                .HasMany(e => e.TP_EXCHANGE)
                .WithOptional(e => e.TP_EXPRESS)
                .HasForeignKey(e => e.EXPRESS_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_EXPRESS>()
                .HasMany(e => e.TP_EXCHANGE1)
                .WithOptional(e => e.TP_EXPRESS1)
                .HasForeignKey(e => e.RETURN_EXPRESS_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_EXPRESS>()
                .HasMany(e => e.TP_ORDER)
                .WithOptional(e => e.TP_EXPRESS)
                .HasForeignKey(e => e.EXPRESS_ID);

            modelBuilder.Entity<TP_EXPRESS>()
                .HasMany(e => e.TP_RETURN)
                .WithOptional(e => e.TP_EXPRESS)
                .HasForeignKey(e => e.EXPRESS_ID);

            modelBuilder.Entity<TP_FAVORITE>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_FAVORITE>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_FAVORITE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_IMAGE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_IMAGE>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<TP_IMAGE>()
                .HasMany(e => e.TP_ACTIVITY_IMAGE)
                .WithOptional(e => e.TP_IMAGE)
                .HasForeignKey(e => e.IMAGE_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_IMAGE>()
                .HasMany(e => e.TP_USER)
                .WithOptional(e => e.TP_IMAGE)
                .HasForeignKey(e => e.ICON_ID);

            modelBuilder.Entity<TP_IMAGE>()
                .HasMany(e => e.TP_PRODUCT_IMAGE)
                .WithOptional(e => e.TP_IMAGE)
                .HasForeignKey(e => e.IMAGE_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_LOCATION>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_LOCATION>()
                .Property(e => e.PRIVINCE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_LOCATION>()
                .Property(e => e.CITY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_LOCATION>()
                .HasMany(e => e.TP_RECEIVER)
                .WithRequired(e => e.TP_LOCATION)
                .HasForeignKey(e => e.LOCATION_ID);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.SHIPPING_RECEIVER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.EXPRESS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.PACKAGE_STATE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.PAYMENT_STATE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER>()
                .Property(e => e.TOTAL_PRICE)
                .HasPrecision(14, 2);

            modelBuilder.Entity<TP_ORDER>()
                .HasMany(e => e.TP_COMMENT)
                .WithOptional(e => e.TP_ORDER)
                .HasForeignKey(e => e.ORDER_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_ORDER>()
                .HasMany(e => e.TP_EXCHANGE)
                .WithRequired(e => e.TP_ORDER)
                .HasForeignKey(e => e.ORDER_ID);

            modelBuilder.Entity<TP_ORDER>()
                .HasMany(e => e.TP_ORDER_PRODUCT)
                .WithRequired(e => e.TP_ORDER)
                .HasForeignKey(e => e.ORDER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_ORDER>()
                .HasMany(e => e.TP_RETURN)
                .WithRequired(e => e.TP_ORDER)
                .HasForeignKey(e => e.ORDER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_ORDER_PRODUCT>()
                .Property(e => e.ORDER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER_PRODUCT>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER_PRODUCT>()
                .Property(e => e.QUANTITY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ORDER_PRODUCT>()
                .Property(e => e.PRICE)
                .HasPrecision(14, 2);

            modelBuilder.Entity<TP_ORDER_PRODUCT>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PACKAGE_STATE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PACKAGE_STATE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_PACKAGE_STATE>()
                .HasMany(e => e.TP_EXCHANGE)
                .WithRequired(e => e.TP_PACKAGE_STATE)
                .HasForeignKey(e => e.PACKAGE_STATE);

            modelBuilder.Entity<TP_PACKAGE_STATE>()
                .HasMany(e => e.TP_ORDER)
                .WithRequired(e => e.TP_PACKAGE_STATE)
                .HasForeignKey(e => e.PACKAGE_STATE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PACKAGE_STATE>()
                .HasMany(e => e.TP_RETURN)
                .WithRequired(e => e.TP_PACKAGE_STATE)
                .HasForeignKey(e => e.PACKAGE_STATE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PAYMENT_STATE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PAYMENT_STATE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_PAYMENT_STATE>()
                .HasMany(e => e.TP_ORDER)
                .WithRequired(e => e.TP_PAYMENT_STATE)
                .HasForeignKey(e => e.PAYMENT_STATE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.CATEGORY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.PRICE)
                .HasPrecision(14, 2);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.INVENTORY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.DETAILS)
                .IsUnicode(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .Property(e => e.BRAND_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_ACTIVITY_PRODUCT)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_COMMENT)
                .WithOptional(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_EXCHANGE)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_FAVORITE)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_ORDER_PRODUCT)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_PRODUCT_IMAGE)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_RETURN)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PRODUCT>()
                .HasMany(e => e.TP_SHOPPING_CART)
                .WithRequired(e => e.TP_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_PRODUCT_IMAGE>()
                .Property(e => e.IMAGE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PRODUCT_IMAGE>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_PRODUCT_IMAGE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RECEIVER>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RECEIVER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_RECEIVER>()
                .Property(e => e.PHONE_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<TP_RECEIVER>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RECEIVER>()
                .Property(e => e.LOCATION_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RECEIVER>()
                .Property(e => e.ADDR_DETAIL)
                .IsUnicode(false);

            modelBuilder.Entity<TP_RECEIVER>()
                .HasMany(e => e.TP_ORDER)
                .WithRequired(e => e.TP_RECEIVER)
                .HasForeignKey(e => e.SHIPPING_RECEIVER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_RECEIVER>()
                .HasMany(e => e.TP_USER1)
                .WithOptional(e => e.TP_RECEIVER1)
                .HasForeignKey(e => e.DEFAULT_RECEIVER_ID);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.ORDER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.QUANTITY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.PACKAGE_STATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.EXPRESS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_RETURN>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ROLE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_ROLE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_ROLE>()
                .HasMany(e => e.TP_USER_ROLE)
                .WithRequired(e => e.TP_ROLE)
                .HasForeignKey(e => e.ROLE_ID);

            modelBuilder.Entity<TP_SHOPPING_CART>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_SHOPPING_CART>()
                .Property(e => e.PRODUCT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_SHOPPING_CART>()
                .Property(e => e.QUANTITY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_SHOPPING_CART>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.SALT)
                .IsUnicode(false);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.PHONE_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.ICON_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_USER>()
                .Property(e => e.DEFAULT_RECEIVER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_COMMENT)
                .WithOptional(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_EXCHANGE)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_FAVORITE)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_ORDER)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_RECEIVER)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_RETURN)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_SHOPPING_CART)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID);

            modelBuilder.Entity<TP_USER>()
                .HasMany(e => e.TP_USER_ROLE)
                .WithRequired(e => e.TP_USER)
                .HasForeignKey(e => e.USER_ID);

            modelBuilder.Entity<TP_USER_ROLE>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_USER_ROLE>()
                .Property(e => e.ROLE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TP_USER_ROLE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);
        }
    }
}
