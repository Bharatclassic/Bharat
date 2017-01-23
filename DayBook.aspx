<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="DayBook.aspx.cs" Inherits="DayBook" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class = "BigFieldSet">
<legend class = "BigLegend">
<h1 style="position:relative; right:0px;">
DAY BOOK
    </h1>
</legend>
<h3>
    <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
    <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
</h3>
<table id = "table" align="center">
<tr align="center">
<td style="width=33px"><asp:Label ID = "lbldate" Width = "40px" runat="server"
Cssclass="Label" Text="Date:" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtdate" Width="102px" CssClass="TextBox" runat="server"
align="left" height="21px" AutoCompleteType="None" Autopostback="true"
autocomplete="off" TabIndex="1"></asp:TextBox>
<asp:CalendarExtender ID="Calenderext" TargetControlID="txtdate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
</td>
</tr>
</table>
<div align="left" class="SubmitButtons" style="position:relative; left:450px; top:0px;">
<asp:Button ID="btnrpt" runat="server" Text="Report" Width="95px" TabIndex="2"
        CssClass="Buttons embossed-link" onclick="btnrpt_Click" />
<asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" TabIndex="3"
        CssClass="Buttons embossed-link" onclick="btnexit_Click" />
</fieldset>
</asp:Content>

