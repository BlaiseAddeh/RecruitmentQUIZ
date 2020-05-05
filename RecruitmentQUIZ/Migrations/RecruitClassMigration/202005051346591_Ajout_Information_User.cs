namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajout_Information_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Phone", c => c.String());
            AddColumn("dbo.Users", "Sexe", c => c.String());
            AddColumn("dbo.Users", "NiveauEtude", c => c.String());
            AddColumn("dbo.Users", "LangueMaternelle", c => c.String());
            AddColumn("dbo.Users", "LangueTravail1", c => c.String());
            AddColumn("dbo.Users", "LangueTravail2", c => c.String());
            AddColumn("dbo.Users", "Nationalite", c => c.String());
            AddColumn("dbo.Users", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Photo");
            DropColumn("dbo.Users", "Nationalite");
            DropColumn("dbo.Users", "LangueTravail2");
            DropColumn("dbo.Users", "LangueTravail1");
            DropColumn("dbo.Users", "LangueMaternelle");
            DropColumn("dbo.Users", "NiveauEtude");
            DropColumn("dbo.Users", "Sexe");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "Email");
        }
    }
}
