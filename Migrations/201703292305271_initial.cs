namespace GameAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false),
                        ScoreValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("dbo.Users", t => t.ScoreId)
                .Index(t => t.ScoreId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "ScoreId", "dbo.Users");
            DropIndex("dbo.Scores", new[] { "ScoreId" });
            DropTable("dbo.Users");
            DropTable("dbo.Scores");
        }
    }
}
