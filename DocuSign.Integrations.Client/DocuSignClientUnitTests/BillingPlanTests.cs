﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocuSign.Integrations.Client;
using System.IO;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestClientUnitTests
{
    [TestClass]
    public class BillingPlanTests
    {
        [TestMethod]
        public void GetPlansForDistributor()
        {
            ConfigLoader.LoadConfig();

            Plans actual = null;

            Account acct = new Account();
            //acct.Email = "greg.miskin@docusign.com";
            //acct.Password = "docusign";

            //if (acct.Login() == false)
            //{
            //    return;
            //}

            try
            {
                actual = acct.BillingPlans();
            }
            catch (Exception)
            {
            }

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GetPlanDetails()
        {
            ConfigLoader.LoadConfig();

            Plan actual = null;

            Account acct = new Account();

            try
            {
                actual = acct.BillingPlan("8844269c-be6d-4b33-9d9f-f4895fdbbb16");
            }
            catch (Exception)
            {
            }

            Assert.IsNotNull(actual);
        }
    }
}
