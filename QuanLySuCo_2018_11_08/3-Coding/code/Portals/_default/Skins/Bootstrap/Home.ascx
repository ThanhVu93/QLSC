<%@ Control Language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
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
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<meta name="viewport" content="width=1455px">
<dnn:DnnJsInclude ID="bootstrapJS" runat="server" FilePath="lib/bootstrap/js/bootstrap.js"
    PathNameAlias="SkinPath" AddTag="false" />
<dnn:DnnJsInclude ID="customJS" runat="server" FilePath="js/scripts.js" PathNameAlias="SkinPath"
    AddTag="false" />
<dnn:DnnJsInclude ID="WOWJs" runat="server" FilePath="lib/WOW/dist/wow.js" PathNameAlias="SkinPath"
    AddTag="false" />
<div id="siteWrapper" style="width: 100%; max-width: 1455px; margin: 0px auto; min-width: 1255px">
    <%--<div id="userControls">
        <div class="row-fluid">
           <div class="span2 language pull-left">
                <dnn:LANGUAGE runat="server" ID="LANGUAGE1" ShowMenu="False" ShowLinks="True" />
            </div>
            <div id="search" class="span3 pull-right">
               <dnn:SEARCH ID="dnnSearch" runat="server" ShowSite="false" ShowWeb="false" EnableTheming="true"
                    Submit="Search" CssClass="SearchButton" />
            </div>
            <div id="login" class="span5 pull-right">
                <dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false" />
                <dnn:USER ID="dnnUser" runat="server" LegacyMode="false" ShowUnreadMessages="false" ShowAvatar="false" />
            </div>
        </div>
    </div>--%>
    <div id="siteHeadBanner">
        <div id="siteHeadBannerinner">
            <div id="dnn_banner">
            </div>
        </div>
    </div>
    <div id="siteHeadouter">
        <div id="siteHeadinner" class="skin-bg-corlor">
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-7 pd-0">
                    <div id="top_menu" class="top_menu">
                    <dnn:MENU ID="MENU2" MenuStyle="BootstrapMenu" runat="server"></dnn:MENU>
                         
                        </div>
                </div>
                <div class="col-md-3 login-menu-item pd-0">
                    <dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false" />
                                    <dnn:USER ID="dnnUser" runat="server" LegacyMode="false" ShowUnreadMessages="false" ShowAvatar="false" />
                    </div>
                <div class="col-md-1">
                </div>
            </div>
             <%--<div id="dnn_headbox" class="container">
                <div class="js-clingify-placeholder" style="height: 0px;">
                    <div class="js-clingify-wrapper">
                        <div id="roll_nav">
                            <div class="head_mid clearfix dnn_layout ">
                                <div id="top_menu2" class="top_menu">
                                    <dnn:MENU ID="MENU1" MenuStyle="BootstrapMenu" runat="server"></dnn:MENU>
                                   
                                </div>
                               <div class="mobile_icons visible-xs ">
                                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-9" aria-expanded="false">
                                    <img src="Portals/_default/Skins/Bootstrap/Images/menu_icon.png" />
                                </button>                             
                            <nav class="navbar navbar-inverse">
                            </nav>
                        </div>
                         <div class="header_shadow hidden-xs "></div>
                    </div>
                    <div class="menu_phone visible-xs">

                        <div class="navbar-collapse collapse" id="bs-example-navbar-collapse-9" aria-expanded="false" style="height: 0.909090876579285px;">
                            <dnn:MENU ID="MENU2" MenuStyle="BootstrapMenu" runat="server"></dnn:MENU>
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <div id="contentWrapper">
        <div class="row">
            <div class="col-md-1">
            </div>
            <div class="col-md-10">
                <div class="col-md-12" runat="server" id="ContentPane">
                </div>
                <div class="row ContentTwoCollumsLeft">
                    <div class="col-md-8" runat="server" id="ContentPaneLeft">
                    </div>
                    <div class="col-md-4" runat="server" id="ContentPaneRight">
                    </div>
                </div>
            </div>
            <div class="col-md-1">
            </div>
            <div class="col-md-12" runat="server" id="FullContentPane">
            </div>
        </div>
    </div>
</div>
<footer id="footerWrapper" class="footer" style="width: 100%; max-width: 1455px; margin: 0px auto; min-width: 1255px">
    <div class="row footerinter">
        <%--<div id="footercontent" class="container pd-b10 pd-t10">
            <div class="footer-infomation">
                <b class="skin-text-corlor">Heifer International Vietnam</b><br>
                Địa chỉ: 272B Đường 30/4 P.Hưng Lợi,Quận Ninh Kiều, TP. Cần Thơ<br>
                Điện thoại: (84-710)3 838 143&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax: (84-710)3 830 657<br>
                Email: info@heifervietnam.org
            </div>
        </div>--%>
        <div id="copyright" class="skin-bg-corlor copyright">
            <div class="copyright-infomation container fz13">
                © Copyright 2015 - Heifer International Vietnam.
            </div>
            <div id="to_top" style="display: block;">
                <div class="to_topimg">
                </div>
            </div>
        </div>
    </div>
</footer>
<dnn:DnnJsInclude ID="dttg" runat="server" FilePath="js/doubletaptogo.min.js" PathNameAlias="SkinPath"
    AddTag="false" />
<script type="text/javascript">
    $(function () {
        $('#navdttg li:has(ul)').doubleTapToGo();
    });
</script>
