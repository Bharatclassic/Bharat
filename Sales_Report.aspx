<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Sales_Report.aspx.cs" Inherits="Sales_Report" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
     <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
     <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" /> 
     
<script type="text/javascript">
    function HideLabel() {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
</script>

     <style type="text/css">
         .style9
         {
             width: 16px;
         }
         .style10
         {
             width: 30px;
         }
         .style11
         {
             width: 133px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
<legend class="BigLegend">
<h1 style="position:relative; right:0px;">
Sales Report
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
<td>
<asp:RadioButton ID="rdDay" runat="server" Text="DayWise" CssClass="RadioButton" AutoPostBack="true" oncheckedchanged="rdDay_CheckedChanged" />
<asp:RadioButton ID="rdBtw" runat="server" Text="Between Two days" AutoPostBack="true" 
        CssClass="RadioButton" oncheckedchanged="rdBtw_CheckedChanged" />
<asp:RadioButton ID="rdCust" runat="server" Text="Customer" AutoPostBack="true"
        CssClass="RadioButton" oncheckedchanged="rdCust_CheckedChanged" />
        <asp:RadioButton ID="rdCust1" runat="server" CssClass="RadioButton" 
        AutoPostBack="true" Text="CustomerWise" 
        oncheckedchanged="rdCust1_CheckedChanged" />
        
</td>
</tr>
</table>
<asp:Panel ID="PanelDay" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table cellpadding="10" style=" cellspacing="10"  width="50%" align="center">
<tr>
<td></td>
<td class="style9"><asp:Label ID="lblDay" runat="server" CssClass="Label" Text="Date"></asp:Label>
</td>
<td style="width:165px">
<asp:TextBox runat="server" ID="txtDay" CssClass="TextBox" TabIndex="2" 
        Width="100px" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off" 
        ontextchanged="txtDay_TextChanged"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDay" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
</td><td></td>
</tr>
<tr>
<td></td>
<td><asp:Label runat="server" ID="lblGrp" Text="Group" CssClass="Label"></asp:Label>
</td>
<td><asp:DropDownList ID="ddlGrp" runat="server" CssClass="DropDown" TabIndex="2" 
        onselectedindexchanged="ddlGrp_SelectedIndexChanged"></asp:DropDownList>
</td>
<td></td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="PanelBtw" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="tblBtw" align="center" cellpadding="10" style="width: 39%"10">
<tr align='center'>
<td class="style11"><asp:Label ID="lblBtwDate1" Width="75px" runat="server" 
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
        ontextchanged="txtbtwDate2_TextChanged" Width="100px" align="left" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtbtwDate2" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
</td>
</tr>
</table>
<table align="center" cellpadding="10" style="width: 39%"10">
<tr>
<td class="style11"></td>
<td style="float:right"><asp:Label ID="lblGrp1" runat="server" CssClass="Label" Text="Group"></asp:Label></td>
<td><asp:DropDownList runat="server" ID="ddlGrp1" CssClass="DropDown" 
        onselectedindexchanged="ddlGrp1_SelectedIndexChanged" TabIndex="2"></asp:DropDownList></td>
<td></td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="PanelCust" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="Table1" align="center" cellpadding="10" style=" cellspacing="10"  style="width: 39%"10">
<tr align="center">
<td></td>
<td>
<asp:CheckBox ID="chkCredCust" runat="server" AutoPostBack="true" CssClass="CheckBox" Text="Credit" 
        oncheckedchanged="chkCredCust_CheckedChanged" />
</td>
<td>
</td>
<td>
<asp:CheckBox ID="chkAdvCust" runat="server" CssClass="CheckBox" Text="Advanced" AutoPostBack="true"
        oncheckedchanged="chkAdvCust_CheckedChanged" />
</td>
</tr>
<tr align='center'>
<td><asp:Label ID="lblCust1" Width="75px" runat="server" CssClass="Label" 
        Text="From Date" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtCust1" cssclass="TextBox" runat="server" Width="100px" 
        align="left" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off" 
        ontextchanged="txtCust1_TextChanged" TabIndex="1"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtCust1" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
</td>
<td>
<asp:Label ID="lblCust2" Width="56px" runat="server" CssClass="Label" 
        Text="To Date" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtCust2" cssclass="TextBox" runat="server" TabIndex="2" 
        ontextchanged="txtCust2_TextChanged" Width="100px" align="left" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
<asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtCust2" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
</td>
</tr>
</table>
</asp:Panel>
<asp:Panel ID="PanelCust1" runat="server" position="relative" BorderStyle="Groove" BorderColor="DarkGreen">
<table id="Table2" align="center" cellpadding="10" style="width: 39%"10">
<tr align='center'>
<td style="width:33px">
    <asp:Label ID="lblCustCode" Width="114px" runat="server" 
        CssClass="Label" Text="Customer Code" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtCustCode" Width="102px" cssclass="TextBox" runat="server" 
        align="left" Height="21px" AutoCompleteType="None" AutoPostBack="true" TabIndex="1" 
        AutoComplete="off" ontextchanged="txtCustCode_TextChanged"></asp:TextBox>
<asp:AutoCompleteExtender ID="txtCustCode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Customercode" ServicePath="" TargetControlID="txtCustCode" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
<td class="style10">
<asp:Label ID="lblCustName" Width="115px" runat="server" CssClass="Label" 
        Text="Customer Name" align="right"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtCustName" TabIndex="2" cssclass="TextBox" runat="server" Width="120px" align="left" AutoCompleteType="None" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
<asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
               Enabled="True" ServiceMethod="Customername" ServicePath="" TargetControlID="txtCustName" CompletionListItemCssClass="OtherCompletionItemCssClass"
               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass" CompletionInterval="10">
              </asp:AutoCompleteExtender>
</td>
</tr>
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