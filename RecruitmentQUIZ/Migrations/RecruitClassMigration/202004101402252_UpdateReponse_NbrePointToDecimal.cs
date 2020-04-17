namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReponse_NbrePointToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resultats", "TotalPoint", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Resultats", "TotalObtenu", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Reponses", "NbrePoints", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reponses", "NbrePoints", c => c.Int(nullable: false));
            AlterColumn("dbo.Resultats", "TotalObtenu", c => c.Int(nullable: false));
            AlterColumn("dbo.Resultats", "TotalPoint", c => c.Int(nullable: false));
        }
    }
}
