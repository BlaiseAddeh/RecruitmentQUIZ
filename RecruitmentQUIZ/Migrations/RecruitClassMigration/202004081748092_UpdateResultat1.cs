namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResultat1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Resultats", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resultats", "UserID", c => c.Int());
        }
    }
}
