<%@ Control Language="C#" Inherits="DotNetNuke.Modules.Admin.Authentication.Login" AutoEventWireup="false" CodeFile="Login.ascx.cs" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<div class="dnnForm dnnLoginService LoginStyle dnnClear">
    <div class="logo">
        <img src="Portals/_default/Skins/QLCC/images/logo1.png"   />
        <h2>Phần mềm Nhắc hợp đồng đến hạn</h2>
        <h4>Công ty điện lực Sóc Trăng</h4>
    </div>
  
    <div class="dnnFormItem">
        <div class="dnnLabel">
            <asp:Label ID="plUsername" AssociatedControlID="txtUsername" runat="server" CssClass="dnnFormLabel " Visible="true" />
        </div>
        <asp:TextBox ID="txtUsername" CssClass="form-control input-sm" runat="server"  />
    </div>

    <div class="dnnFormItem">
        <div class="dnnLabel">
            <asp:Label ID="plPassword" AssociatedControlID="txtPassword" runat="server" resourcekey="Password" CssClass="dnnFormLabel" ViewStateMode="Disabled" Visible="true" />
        </div>
        <asp:TextBox ID="txtPassword" CssClass="form-control input-sm" TextMode="Password" runat="server" />
    </div>
    <div class="dnnFormItem" id="divCaptcha1" runat="server" visible="false">
        <asp:Label ID="plCaptcha" AssociatedControlID="ctlCaptcha" runat="server" resourcekey="Captcha" CssClass="dnnFormLabel" />
    </div>
    <div class="dnnFormItem dnnCaptcha" id="divCaptcha2" runat="server" visible="false">
        <dnn:CaptchaControl ID="ctlCaptcha" CaptchaWidth="130" CaptchaHeight="40" runat="server" ErrorStyle-CssClass="dnnFormMessage dnnFormError dnnCaptcha" ViewStateMode="Disabled" />
    </div>
    <div class="dnnFormItem">
        <div class="dnnLabel">
            <asp:Label ID="lblLogin" runat="server" AssociatedControlID="cmdLogin" CssClass="dnnFormLabel" ViewStateMode="Disabled" />
        </div>
        <asp:LinkButton ID="cmdLogin" resourcekey="cmdLogin" CssClass="btn cam btn-sm waves-effect none-radius btn-primary" Text="Login" runat="server" />
        <asp:LinkButton ID="cmdCancel" runat="server" CssClass="btn btn-default btn-sm  waves-effect" resourcekey="cmdCancel" CausesValidation="false" />

    </div>
    <div class="dnnFormItem">
        <div class="dnnLabel">
            <asp:Label ID="lblLoginRememberMe" runat="server" AssociatedControlID="cmdLogin" CssClass="dnnFormLabel" />
        </div>
        <span class="dnnLoginRememberMe">
            <asp:CheckBox ID="chkCookie" resourcekey="Remember" runat="server" />
        </span>
    </div>
    <div class="dnnFormItem">
        <div class="dnnLabel">
            <label class="dnnFormLabel">
            </label>
        </div>
        <div class="dnnLoginActions">
            <ul class="dnnActions dnnClear">
                <li id="liRegister" runat="server">
                    <asp:HyperLink ID="registerLink" runat="server" CssClass="text-primary" resourcekey="cmdRegister" ViewStateMode="Disabled" /></li>
                <li id="liPassword" runat="server" >
                    <asp:HyperLink ID="passwordLink" runat="server" CssClass="text-primary" resourcekey="cmdPassword" ViewStateMode="Disabled" /></li>
            </ul>
        </div>
    </div>
</div>
<script type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function setUpLogin() {
            var actionLinks = $("a[id$=DNN_cmdLogin]");
            actionLinks.click(function () {
                if ($(this).hasClass("dnnDisabledAction")) {
                    return false;
                }

                actionLinks.addClass("dnnDisabledAction");
            });
        }

        $(document).ready(function () {
            setUpLogin();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setUpLogin();
            });
        });
    }(jQuery, window.Sys));
jQuery("html").css({"background":"#eee"});
</script>
