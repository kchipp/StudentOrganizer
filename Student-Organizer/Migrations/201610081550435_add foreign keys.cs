namespace Student_Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Student_id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Faculty_id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Student_id");
            CreateIndex("dbo.AspNetUsers", "Faculty_id");
            AddForeignKey("dbo.AspNetUsers", "Faculty_id", "dbo.Faculties", "Faculty_id");
            AddForeignKey("dbo.AspNetUsers", "Student_id", "dbo.Students", "Student_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Student_id", "dbo.Students");
            DropForeignKey("dbo.AspNetUsers", "Faculty_id", "dbo.Faculties");
            DropIndex("dbo.AspNetUsers", new[] { "Faculty_id" });
            DropIndex("dbo.AspNetUsers", new[] { "Student_id" });
            DropColumn("dbo.AspNetUsers", "Faculty_id");
            DropColumn("dbo.AspNetUsers", "Student_id");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
