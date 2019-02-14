<%@ Control AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Containers.Container" %>
<%@ Register TagPrefix="dnn" TagName="TITLE" Src="~/Admin/Containers/Title.ascx" %>
<div class="panel panel-default new-event">
    <div class="panel-heading ">
        <h3 class="panel-title"><dnn:TITLE runat="server" CssClass="font Head" id="dnnTITLE" /></h3>
    </div>
    <div class="panel-body"  id="ContentPane" runat="server">
    </div>
</div>
