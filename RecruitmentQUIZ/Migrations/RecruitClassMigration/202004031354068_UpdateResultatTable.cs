namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResultatTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Resultats", "ProjetID");
            RenameColumn(table: "dbo.Resultats", name: "Projet_ProjectID", newName: "ProjetID");
            RenameIndex(table: "dbo.Resultats", name: "IX_Projet_ProjectID", newName: "IX_ProjetID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Resultats", name: "IX_ProjetID", newName: "IX_Projet_ProjectID");
            RenameColumn(table: "dbo.Resultats", name: "ProjetID", newName: "Projet_ProjectID");
            AddColumn("dbo.Resultats", "ProjetID", c => c.Int());
        }
    }
}
