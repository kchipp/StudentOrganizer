namespace Student_Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedeventColortobackgroundColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "backgroundColor", c => c.String());
            DropColumn("dbo.Events", "eventColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "eventColor", c => c.String());
            DropColumn("dbo.Events", "backgroundColor");
        }
    }
}
