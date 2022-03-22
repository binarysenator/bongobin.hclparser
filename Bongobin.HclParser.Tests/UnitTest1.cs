using System;
using System.Linq;
using Bongobin.HclParser.Nodes;
using NUnit.Framework;

namespace Bongobin.HclParser.Tests
{
    public class Tests
    {

        [Test]
        public void DataSource_NoVariables()
        {
            var doc = HclDocument.Parse("data \"aaa\" \"bbb\" {\r\n}");
            Assert.AreEqual(9, doc.Parts.Count());
            Assert.AreEqual("data \"aaa\" \"bbb\" {\r\n}", doc.Text);
            var node = doc.Root.Children.First();
            var dataNode = node as DataSourceNode;
            Assert.IsNotNull(dataNode);
            Assert.AreEqual("aaa", dataNode.ResourceType);
            Assert.AreEqual("bbb", dataNode.ResourceName);
            Assert.AreEqual(0, dataNode.Variables.Count);

        }

        [Test]
        public void Test_ResourceWithSquareBrackets()
        {
            var resource = "resource \"azurerm_virtual_network\" \"vpn\" {\r\n  name                = join(\"-\", [\"vnet\", var.env_short, var.resource_prefix])\r\n  location            = azurerm_resource_group.main.location\r\n  resource_group_name = azurerm_resource_group.main.name\r\n\r\n  address_space       = [var.vnet_cidr]\r\n\r\n  tags = var.tags\r\n}";
            var doc = HclDocument.Parse(resource);
            Assert.AreEqual(resource, doc.Text);
            return;
        }

        [Test]
        public void Test_ResourceWithVariables()
        {
            var resource =
                "resource \"azurerm_servicebus_namespace\" \"servicebus_namespace\" {\r\n  name                = \"synergix-${environment_suffix}\"\r\n  location            = data.azurerm_resource_group.resource_group.location\r\n  resource_group_name = data.azurerm_resource_group.resource_group.name\r\n  sku                 = var.service_bus_version\r\n  zone_redundant      = true\r\n  capacity = 1\r\n}";
            var doc = HclDocument.Parse(resource);
            Assert.AreEqual(resource, doc.Text);
            return;
        }

        [Test]
        public void Test_VirtualNetworkWithComment()
        {
            var tf = "# Shared Virtual Network\r\ndata \"azurerm_virtual_network\" \"dcaVnet\" {\r\n  name                = \"dcaVNet\"\r\n  resource_group_name = \"dcaCore\"\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);
            Assert.AreEqual(1, doc.Root.DataSourceNodes.Count());
        }

        [Test]
        public void Test_ResourceGroup_OddSpacing()
        {
            var tf = "resource \"azurerm_resource_group\" \"main\" {\r\n  location = var.location\r\n  name     = join(\"-\", [\"rg\", var.env_short, var.resource_prefix])\r\n\r\n  tags = var.tags\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);
            Assert.AreEqual(1, doc.Root.ResourceNodes.Count());
            Assert.AreEqual(3, doc.Root.ResourceNodes.First().VariableAssignments.Count());
            var assignment1 = doc.Root.Resources["main"];
            Assert.IsNotNull(assignment1);
            Assert.AreEqual("main", assignment1.ResourceName);
            Assert.AreEqual("azurerm_resource_group", assignment1.ResourceType);

            var location = assignment1.Variables["location"];
            Assert.IsNotNull(location);
            Assert.AreEqual("location", location.Name);
            Assert.AreEqual("var.location", location.Value);

            var name = assignment1.Variables["name"];
            Assert.IsNotNull(name);
            Assert.AreEqual("name", name.Name);
            Assert.AreEqual("join(\"-\", [\"rg\", var.env_short, var.resource_prefix])", name.Value);

            var tags = assignment1.Variables["tags"];
            Assert.IsNotNull(name);
            Assert.AreEqual("tags", tags.Name);
            Assert.AreEqual("var.tags", tags.Value);
        }

        [Test]
        public void Test_SimpleVariable()
        {
            var tf = "variable \"vpn_aad_tenant\" {\r\n  type = string\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);
            Assert.AreEqual(1, doc.Root.VariableNodes.Count());
            Assert.AreEqual("string", doc.Root.VariableNodes.First().Type);
        }

        [Test]
        public void Test_FunctionAppBlocks()
        {
            var tf = "resource \"azurerm_function_app\" \"diag_result_case_publisher\" {\r\n  name                       = join(\"-\", [var.diagnostic_result_case_publisher_config.fa_name, \"function\"])\r\n  location                   = var.diagnostic_result_case_publisher_config.location\r\n  resource_group_name        = var.diagnostic_result_case_publisher_config.rg_name\r\n  app_service_plan_id        = azurerm_app_service_plan.diag_result_case_publisher.id\r\n  storage_account_name       = azurerm_storage_account.diag_order_provider.name\r\n  storage_account_access_key = azurerm_storage_account.diag_order_provider.primary_access_key\r\n  version                    = \"~3\"\r\n  https_only                 = true\r\n\r\n  site_config {\r\n    min_tls_version = \"1.2\"\r\n  }\r\n\r\n  identity {\r\n    type = \"SystemAssigned\"\r\n  }\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);

            var resource = doc.Root.Resources["diag_result_case_publisher"];
            var variables = resource.Variables;
            Assert.AreEqual(8, variables.Count);

            var blocks = resource.Blocks;
            Assert.AreEqual(2, blocks.Count);

            var siteconfig = blocks["site_config"];
            Assert.IsNotNull(siteconfig);
        }

        [Test]
        public void Test_ServiceBusTopic()
        {
            var tf = "# Service Bus Topics\r\nresource \"azurerm_servicebus_topic\" \"crm-accountplan_accountplanbenefits_topic\" {\r\n  name                = \"crm-accountplan-accountplanbenefits\"\r\n  resource_group_name = data.azurerm_resource_group.resource_group.name\r\n  namespace_name      = azurerm_servicebus_namespace.servicebus_namespace.name\r\n  enable_batched_operations               = true\r\n  support_ordering                        = true\r\n  enable_partitioning = false\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);
        }

        [Test]
        public void Test_MultipleVariables()
        {
            var tf = "variable \"resource_group\" {\r\n  description = \"The name of the resource group in which to create resources\"\r\n}\r\n\r\nvariable \"location\" {\r\n  description = \"Region where the resource group is created\"\r\n}\r\n\r\nvariable \"environment_suffix\" {\r\n  description = \"The suffix of the environment (dev, test, uat, pro, testing e.t.c)\"\r\n}\r\n\r\nvariable \"service_bus_version\" {\r\n  description = \"The version of the service bus instance\"\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);

            Assert.AreEqual(4, doc.Root.VariableNodes.Count());
        }

        [Test]
        public void Test_TopLevelBlock()
        {
            var tf = "resource_groups = {\r\n    primary_rg = {\r\n        name   = \"dsb\"\r\n        region = \"region1\"\r\n        tags = {           \r\n        }\r\n    }\r\n}";
            var doc = HclDocument.Parse(tf);
            Assert.AreEqual(tf, doc.Text);
        }
    }
}