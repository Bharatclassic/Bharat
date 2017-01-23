<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="PurchaseReport.aspx.cs" Inherits="PurchaseReport" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
     <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
     <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" /> 
     
    <style type="text/css">
        .style9
        {
            width: 19px;
        }
        .style10
        {
            width: 128px;
        }
        .style11
        {
            width: 1px;
        }
    </style>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
<legend class="BigLegend">
<h1 style="position:relative; right:0px;">
Purchase Report
</h1>
</legend>
<table align="center" cellspacing="6">
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
</h3>
</table>
<table align="center">
<tr>
<td><asp:RadioButton ID="rdDay" runat="server" Text="DayWise" 
        CssClass="RadioButton" AutoPostBack="true" 
        oncheckedchanged="rdDay_CheckedChanged" /></td>
<td><asp:RadioButton ID="rdBtw" runat="server" Text="Between Two days" 
        AutoPostBack="true" CssClass="RadioButton" 
        oncheckedchanged="rdBtw_CheckedChanged" /></td>
<td><asp:RadioButton ID="rdPurch" runat="server" Text="SupplierWise" 
        AutoPostBack="true" CssClass="RadioButton" 
        oncheckedchanged="rdPurch_CheckedChanged" /></td>
</tr>
</table>
<asp:Panel ID="PanelDay" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table cellpadding="10" style=" cellspacing="10"  width="50%" align="center">
<tr>
<td></td>
<td class="style9"><asp:Label ID="lblDay" align="right" runat="server" CssClass="Label" Text="Date"></asp:Label>
</td>
<td class="style10">
<asp:TextBox runat="server" ID="txtDay" CssClass="TextBox" TabIndex="2" 
        Width="100px" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off" 
        ontextchanged="txtDay_TextChanged"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDay" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
</td><td></td>
</tr>
<tr>
<td></td>
<td><asp:Label ID="lblGrp" runat="server" Text="Group Name" CssClass="Label"></asp:Label>
</td>
<td><asp:DropDownList ID="ddlGrp" runat="server" CssClass="DropDown" 
        onselectedindexchanged="ddlGrp_SelectedIndexChanged" TabIndex="2" AutoPostBack="true"></asp:DropDownList>
</td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="PanelBtw" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="tblBtw" align="center" cellpadding="10" style="width: 39%"10">
<tr align='center'>
<td style="width:33px"><asp:Label ID="lblBtwDate1" Width="75px" runat="server" 
        CssClass="Label" Text="From Date" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtbtwDate1" Width="102px" cssclass="TextBox" runat="server" 
        align="left" Height="21px" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" TabIndex="1" ontextchanged="txtbtwDate1_TextChanged"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtbtwDate1" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td class="style10">
<asp:Label ID="lblBtwDate2" Width="60px" runat="server" CssClass="Label" 
        Text="To Date" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtbtwDate2" cssclass="TextBox" runat="server" TabIndex="2" 
Width="100px" align="left" AutoCompleteType="None" AutoPostBack="true" 
        AutoComplete="off" ontextchanged="txtbtwDate2_TextChanged"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtbtwDate2" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
</td>
</tr>
<table align="center" cellpadding="10">
<tr>
<td></td>
<td><asp:Label ID="lblGrp1" runat="server" Text="Group Name" CssClass="Label"></asp:Label>
</td>
<td><asp:DropDownList ID="ddlgrp1" runat="server" CssClass="DropDown" 
        onselectedindexchanged="ddlgrp1_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
</td>
</tr>
</table>
    </asp:Panel>
<asp:Panel ID="PanelSupp" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="Table2" align="center" cellpadding="10" style="width: 39%"10">
<tr align='center'>
<td><asp:Label ID="lblSuppCode" Width="99px" runat="server" 
        CssClass="Label" Text="Supplier Code" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtSuppCode" Width="102px" cssclass="TextBox" runat="server" 
        align="left" Height="21px" AutoCompleteType="None" AutoPostBack="true" TabIndex="1" 
        AutoComplete="off" ontextchanged="txtSuppCode_TextChanged"></asp:TextBox>
<asp:AutoCompleteExtender ID="txtSuppCode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Suppliercode" ServicePath="" TargetControlID="txtSuppCode" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
<td class="style10">
<asp:Label ID="lblSuppName" Width="108px" runat="server" CssClass="Label" 
        Text="Supplier Name" align="right"></asp:Label>
</td>
<td> 
<asp:TextBox ID="txtSuppName" cssclass="TextBox" runat="server" Width="200px" TabIndex="2"
        align="left" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off" 
        ontextchanged="txtSuppName_TextChanged"></asp:TextBox>
        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Suppliername" ServicePath="" TargetControlID="txtSuppName" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
</tr>
</table>
<table align="center">
<tr><td>
<asp:RadioButton ID="RadioButton1" runat="server" Text="All" 
        CssClass="RadioButton" AutoPostBack="true" 
        oncheckedchanged="RadioButton1_CheckedChanged"/></td>
<td><asp:RadioButton ID="RadioButton2" runat="server" Text="Invoiceno" 
        AutoPostBack="true" CssClass="RadioButton" 
        oncheckedchanged="RadioButton2_CheckedChanged"  /></td></tr>
 

<tr>
 
     <td colspan="6" align="center" class="style9">
      <asp:Label ID="lblchkgrp" runat="server" Text="Invoice No" CssClass="Label"></asp:Label>
   <asp:DropDownList ID="ddinvoiveno" runat="server" Width="150px"  CssClass="DropDown" AutoPostBack="true"></asp:DropDownList>
</td></tr>

</table>

</asp:Panel>
<div align="center" class="SubmitButtons" style="position:relative; left: 250px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Generate"  Width="95px" 
                 TabIndex="3" CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
             <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" 
                 CssClass="Buttons embossed-link" TabIndex="4" onclick="btnExit_Click"/>  
</div>
</fieldset>
</asp:Content>

