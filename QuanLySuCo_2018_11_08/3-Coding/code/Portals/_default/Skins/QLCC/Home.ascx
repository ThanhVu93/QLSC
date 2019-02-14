<!--khai bao file user control-->
<%@ Control Language="c#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="DNNLINK" Src="~/Admin/Skins/DnnLink.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKTOMOBILE" Src="~/Admin/Skins/LinkToMobileSite.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<script runat="server">
	protected override void OnLoad(EventArgs e)
	{
		// Set page title by template [page name] - [site name]
		DotNetNuke.Framework.CDefault tp = ((DotNetNuke.Framework.CDefault)this.Page);
		StringBuilder title = new StringBuilder();
		if (!string.IsNullOrEmpty(PortalSettings.ActiveTab.Title))
		{
			title.Append(PortalSettings.ActiveTab.Title);
		}
		else
		{
			DotNetNuke.Entities.Tabs.TabInfo tab = 
				(DotNetNuke.Entities.Tabs.TabInfo)PortalSettings.ActiveTab.BreadCrumbs[PortalSettings.ActiveTab.BreadCrumbs.Count - 1];
			title.Append(tab.TabName);
		}
		title.Append(string.Concat(" - ", PortalSettings.PortalName));
		tp.Title = title.ToString();
	}
</script>

<dnn:DnnJsInclude ID="bootstrapJS" runat="server" FilePath="bootstrap/js/bootstrap.min.js" PathNameAlias="SkinPath" AddTag="false" />
<meta name="viewport" content="width=device-width, initial-scale=1">
<div class="row">
    <div class="main-header"  style="display:<%#Request.IsAuthenticated?"":"none !important" %>">
        <!-- Logo -->
        <div class="logo hidden-xs">
            <!-- mini logo for sidebar mini 50x50 pixels -->
            <span class="logo-mini">QUẢN LÝ HỢP ĐỒNG</span>
            <span class="logo-lg">QUẢN LÝ HỢP ĐỒNG</span>
        </div>
        <!-- Header Navbar: style can be found in header.less -->
        <nav class="navbar">
            <!-- Sidebar toggle button-->
            <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button" id="sidebar-toggle">
                <span class="sr-only">MENU</span>
            </a>
            <div class="navbar-custom-menu">
                <ul class="nav navbar-nav">
                    <li>
                        <a href="<%#Request.IsAuthenticated?DotNetNuke.Common.Globals.ApplicationPath+"/Default.aspx?tabid=56":DotNetNuke.Common.Globals.ApplicationPath+"/Login.aspx"%>">
                            <span><%#Request.IsAuthenticated?UserController.GetCurrentUserInfo().DisplayName.ToString():"Đăng Nhập" %></span>
                        </a>
                    </li>
                    <li class="treeview"><a href="<%#Request.IsAuthenticated?DotNetNuke.Common.Globals.ApplicationPath+"/logoff.aspx":"#"%>" style="display: <%#Request.IsAuthenticated?"block":"none" %>"><%#Request.IsAuthenticated?"Thoát":"" %> </a>
                    </li>
                </ul>
            </div>
        </nav>
    </div>
    <aside class="main-sidebar" style="width: <%#Request.IsAuthenticated?"":"0px !important" %>" >
        <section class="sidebar">
            <dnn:MENU ID="MENU" MenuStyle="BootstrapMenu" runat="server"></dnn:MENU>
            <ul class="sidebar-menu">
                <li class="treeview text-center">
                    <img src="Portals/_default/Skins/QLCC/images/logo.png" class="pd-30"  />
                </li>
             </ul>
        </section>
        <!-- /.sidebar -->
    </aside>
    <div class="content-wrapper" style="overflow-y:hidden;overflow-x:auto;min-width:100px;margin-left: <%#Request.IsAuthenticated?"":"0px ! important;" %>" >
        <div id="ContentPane" class="contentPane" runat="server"></div>
        <div class="row">
            <div runat="server" class="col-md-6" id="ContentLeft"></div>
            <div runat="server" class="col-md-6" id="ContentRight"></div>
        </div>
    </div>
    <div id="footer" class="main-footer" style="display: <%#Request.IsAuthenticated?"":"none ! important" %>">
        <div id="footer-content">
            <div class="bottom_left" style="">
                <div class="footer-infomation">
                    © Copyright 2018.                 
                </div>
            </div>
        </div>
    </div>
</div>

<dnn:DnnJsInclude ID="appJS" runat="server" FilePath="dist/js/app.min.js" PathNameAlias="SkinPath" AddTag="false" />
<dnn:DnnJsInclude ID="toastrJS" runat="server" FilePath="js/toastr.js" PathNameAlias="SkinPath" AddTag="false" />
<dnn:DnnJsInclude ID="adminJS" runat="server" FilePath="js/admin.js" PathNameAlias="SkinPath" AddTag="false" />
<dnn:DnnJsInclude ID="wavesJS" runat="server" FilePath="js/waves.js" PathNameAlias="SkinPath" AddTag="false" />
<dnn:DnnJsInclude ID="customJS" runat="server" FilePath="js/js.js" PathNameAlias="SkinPath" AddTag="false" />


