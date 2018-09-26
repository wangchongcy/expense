using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expense.Model.Exceptions;

namespace Expense.Service.Tests
{
    [TestClass]
    public class ExpenseServiceTest
    {
        private IExpenseService service = new ExpenseService(); 

        [TestMethod]
        public void TestExtractBasicExpenseInfo()
        {
            var text = @"Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested…

<expense><cost_centre>DEV002</cost_centre> <total>890.55</total><payment_method>personalcard</payment_method>
</expense>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan";

            var result = service.ExtractExpenseInfoFromText(text); 
            Assert.AreEqual(result.PropertyCount, 6); 
            Assert.AreEqual(result.GetProperty("cost_centre")?.Value, "DEV002");
            Assert.AreEqual(result.GetProperty("total")?.Value, "890.55");
            Assert.AreEqual(result.GetProperty("payment_method")?.Value, "personalcard");
            Assert.AreEqual(result.GetProperty("vendor")?.Value, "Viaduct Steakhouse");
            Assert.AreEqual(result.GetProperty("description")?.Value, "development team’s project end celebration dinner");
            Assert.AreEqual(result.GetProperty("date")?.Value, "Tuesday 27 April 2017");
        }

        [TestMethod]
        public void TestExtractBasicExpenseInfoWithNotClosingTags()
        {
            var text = @"Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested…

<expense><cost_centre>DEV002</cost_centre> <total>890.55</total><payment_method>personalcard</payment_method>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan";

            try
            {
                var result = service.ExtractExpenseInfoFromText(text);
            }
            catch (XmlTagNotMatchingException)
            {
                return; 
            }

            Assert.Fail(); 
        }

        [TestMethod]
        public void TestExtractBasicExpenseInfoWithoutTotal()
        {
            var text = @"Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested…

<expense><cost_centre>DEV002</cost_centre> <payment_method>personalcard</payment_method>
</expense>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan";

            try
            {
                var result = service.ExtractExpenseInfoFromText(text);
            }
            catch (MissingTotalPropertyException)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void TestExtractBasicExpenseInfoWithoutCostCentre()
        {
            var text = @"Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested…

<expense><total>890.55</total><payment_method>personalcard</payment_method>
</expense>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan";

            var result = service.ExtractExpenseInfoFromText(text);
            Assert.AreEqual(result.PropertyCount, 6);
            Assert.AreEqual(result.GetProperty("cost_centre")?.Value, "UNKNOWN");
            Assert.AreEqual(result.GetProperty("total")?.Value, "890.55");
            Assert.AreEqual(result.GetProperty("payment_method")?.Value, "personalcard");
            Assert.AreEqual(result.GetProperty("vendor")?.Value, "Viaduct Steakhouse");
            Assert.AreEqual(result.GetProperty("description")?.Value, "development team’s project end celebration dinner");
            Assert.AreEqual(result.GetProperty("date")?.Value, "Tuesday 27 April 2017");
        }
    }
}
