namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relation_User_Projet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProjetID", c => c.Int());
            CreateIndex("dbo.Users", "ProjetID");
            AddForeignKey("dbo.Users", "ProjetID", "dbo.Projets", "ProjectID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ProjetID", "dbo.Projets");
            DropIndex("dbo.Users", new[] { "ProjetID" });
            DropColumn("dbo.Users", "ProjetID");
        }
    }
}
