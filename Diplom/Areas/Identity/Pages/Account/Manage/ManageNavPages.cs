﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diplom.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Email => "Email";

        public static string ChangePassword => "ChangePassword";

        public static string DownloadPersonalData => "DownloadPersonalData";

        public static string DeletePersonalData => "DeletePersonalData";

        public static string ExternalLogins => "ExternalLogins";

        public static string Profile => "Profile";

        public static string PersonalData => "PersonalData";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string Job => "Job";
        public static string Driver => "Driver";
        public static string Manager => "Manager";
        public static string Cargo => "Cargo";
        public static string CustromerOrders => "CustromerOrders";
        public static string StoreOrders => "StoreOrders";
        public static string ManagerOrders => "ManagerOrders";
        public static string DriverOrders => "DriverOrders";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

        public static string DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string ProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profile);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        public static string JobNavClass(ViewContext viewContext) => PageNavClass(viewContext, Job);
        public static string DriverNavClass(ViewContext viewContext) => PageNavClass(viewContext, Driver);
        public static string ManagerNavClass(ViewContext viewContext) => PageNavClass(viewContext, Manager);
        public static string CargoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Cargo);
        public static string CustoromerOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, CustromerOrders);
        public static string StoreOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, StoreOrders);
        public static string ManagerOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManagerOrders);
        public static string DriverOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, DriverOrders);


        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
