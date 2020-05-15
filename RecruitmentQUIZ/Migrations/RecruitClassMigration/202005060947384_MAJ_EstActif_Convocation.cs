namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJ_EstActif_Convocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EstConvoquePourPratique", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "EstRetenuApresPratique", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "EstActif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EstActif");
            DropColumn("dbo.Users", "EstRetenuApresPratique");
            DropColumn("dbo.Users", "EstConvoquePourPratique");
        }
    }
}
