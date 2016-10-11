namespace Student_Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addededitableproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "editable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "editable");
        }
    }
}
