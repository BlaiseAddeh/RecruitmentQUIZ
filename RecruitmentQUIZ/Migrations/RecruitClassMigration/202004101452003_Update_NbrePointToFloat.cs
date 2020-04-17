namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_NbrePointToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resultats", "TotalPoint", c => c.Single(nullable: false));
            AlterColumn("dbo.Resultats", "TotalObtenu", c => c.Single(nullable: false));
            AlterColumn("dbo.Reponses", "NbrePoints", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reponses", "NbrePoints", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Resultats", "TotalObtenu", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Resultats", "TotalPoint", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
