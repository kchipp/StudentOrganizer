namespace Student_Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        eventID = c.String(),
                        title = c.String(),
                        description = c.String(),
                        startDate = c.String(),
                        startTime = c.String(),
                        endDate = c.String(),
                        endTime = c.String(),
                        start = c.DateTime(nullable: false),
                        end = c.DateTime(nullable: false),
                        url = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Faculty_id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Faculty_id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Student_id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Student_id);
            
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
            DropTable("dbo.Students");
            DropTable("dbo.Faculties");
            DropTable("dbo.Events");
        }
    }
}
