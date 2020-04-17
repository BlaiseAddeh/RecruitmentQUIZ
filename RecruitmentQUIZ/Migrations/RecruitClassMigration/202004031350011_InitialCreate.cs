namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OptionReponses",
                c => new
                    {
                        OptionReponseID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.OptionReponseID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        EstMultiChoix = c.Boolean(nullable: false),
                        Libelle = c.String(),
                        ProjetID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Projets", t => t.ProjetID)
                .Index(t => t.ProjetID);
            
            CreateTable(
                "dbo.Projets",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.Resultats",
                c => new
                    {
                        ResultatID = c.Int(nullable: false),
                        TotalPoint = c.Int(nullable: false),
                        TotalObtenu = c.Int(nullable: false),
                        ProjetID = c.Int(),
                        Projet_ProjectID = c.Int(),
                    })
                .PrimaryKey(t => t.ResultatID)
                .ForeignKey("dbo.Projets", t => t.Projet_ProjectID)
                .ForeignKey("dbo.Users", t => t.ResultatID)
                .Index(t => t.ResultatID)
                .Index(t => t.Projet_ProjectID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        SecurityLevelID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.SecurityLevels", t => t.SecurityLevelID)
                .Index(t => t.SecurityLevelID);
            
            CreateTable(
                "dbo.SecurityLevels",
                c => new
                    {
                        SecurityLevelID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.SecurityLevelID);
            
            CreateTable(
                "dbo.Reponses",
                c => new
                    {
                        ReponseID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        NbrePoints = c.Int(nullable: false),
                        QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.ReponseID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .Index(t => t.QuestionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reponses", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "ProjetID", "dbo.Projets");
            DropForeignKey("dbo.Resultats", "ResultatID", "dbo.Users");
            DropForeignKey("dbo.Users", "SecurityLevelID", "dbo.SecurityLevels");
            DropForeignKey("dbo.Resultats", "Projet_ProjectID", "dbo.Projets");
            DropForeignKey("dbo.OptionReponses", "QuestionID", "dbo.Questions");
            DropIndex("dbo.Reponses", new[] { "QuestionID" });
            DropIndex("dbo.Users", new[] { "SecurityLevelID" });
            DropIndex("dbo.Resultats", new[] { "Projet_ProjectID" });
            DropIndex("dbo.Resultats", new[] { "ResultatID" });
            DropIndex("dbo.Questions", new[] { "ProjetID" });
            DropIndex("dbo.OptionReponses", new[] { "QuestionID" });
            DropTable("dbo.Reponses");
            DropTable("dbo.SecurityLevels");
            DropTable("dbo.Users");
            DropTable("dbo.Resultats");
            DropTable("dbo.Projets");
            DropTable("dbo.Questions");
            DropTable("dbo.OptionReponses");
        }
    }
}
