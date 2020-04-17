namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResultat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resultats", "ExamQuestions", c => c.String());
            AddColumn("dbo.Resultats", "ExamReponseOptions", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resultats", "ExamReponseOptions");
            DropColumn("dbo.Resultats", "ExamQuestions");
        }
    }
}
