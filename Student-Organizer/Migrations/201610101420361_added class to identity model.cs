namespace Student_Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclasstoidentitymodel : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
        }
    }
}
