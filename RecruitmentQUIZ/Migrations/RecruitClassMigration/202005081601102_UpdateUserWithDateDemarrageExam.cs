namespace RecruitmentQUIZ.Migrations.RecruitClassMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserWithDateDemarrageExam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ADemarrerExame", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DateDemarrageExam", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateDemarrageExam");
            DropColumn("dbo.Users", "ADemarrerExame");
        }
    }
}
