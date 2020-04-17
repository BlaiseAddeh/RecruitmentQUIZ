namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relation_User_Resultat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resultats", "UserID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resultats", "UserID");
        }
    }
}
