using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;
using Core;

namespace MetricsAgent.DAL.Migrations
{

    [Migration(3)]
    public class AllMigration :Migration
    {

        public override void Up()
        {
            if (!Schema.Table("cpumetrics").Exists())
            { 
                Create.Table("cpumetrics")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            }
        }

        public override void Down()
        {
            if (Schema.Table("cpumetrics").Exists()) Delete.Table("cpumetrics");
        }

    }
}
