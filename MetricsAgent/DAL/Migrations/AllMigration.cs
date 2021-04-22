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
            if (!Schema.Table("dotnetmetrics").Exists())
            { 
                Create.Table("dotnetmetrics")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            }

            if (!Schema.Table("hddmetrics").Exists())
            { 
                Create.Table("hddmetrics")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            } 

            if (!Schema.Table("rammetrics").Exists())
            { 
                Create.Table("rammetrics")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            } 
            
            if (!Schema.Table("networkmetrics").Exists())
            { 
                Create.Table("networkmetrics")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Value").AsInt32()
                    .WithColumn("Time").AsInt64();
            }

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
            if (Schema.Table("networkmetrics").Exists()) Delete.Table("networkmetrics");
            if (Schema.Table("hddmetrics").Exists()) Delete.Table("hddmetrics");
            if (Schema.Table("rammetrics").Exists()) Delete.Table("rammetrics");
            if (Schema.Table("dotnetmetrics").Exists()) Delete.Table("dotnetmetrics");
        }

    }
}
