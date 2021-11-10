namespace QRAttendanceSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Guid(nullable: false),
                        Firstname = c.String(unicode: false),
                        Lastname = c.String(unicode: false),
                        EmploymentStatus = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.QrCodes",
                c => new
                    {
                        QrCodeId = c.Guid(nullable: false),
                        QrCodeValue = c.String(unicode: false),
                        QrPath = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.QrCodeId);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ProfileId = c.Guid(nullable: false),
                        Username = c.String(unicode: false),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        TimeIn = c.DateTime(precision: 0),
                        TimeOut = c.DateTime(precision: 0),
                        IsLate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserLogs");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.QrCodes");
            DropTable("dbo.Profiles");
        }
    }
}
