using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.ViewModels
{
    public class SiteModel
    {
        public String Icon {
            get {
                return SiteModel.getLogoIconFolder()+SiteModel.getLogoIcon();
            }
        }

        public static String getLogoIcon()
        {
            return "site_logo_icon.ico";
        }

        public static String getLogoIconFolder()
        {
            return "/Images/";
        }
    }
}